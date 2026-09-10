using Spectre.Console;

namespace Zemphas
{
    internal class HeroManagement
    {
        // Hero Alive Check
        public static bool HeroAliveCheck(Hero hero)
        {
            if (hero.alive == false)
            {
                Console.WriteLine("YOU LOST...");
                Console.WriteLine();
                return false;
            }
            return true;
        }

        // Hero evasion/sneak check
        public static bool HeroEvasionCheck(Hero hero)
        {
            int chanceScale = 10;
            Random random = new Random();

            int userEscapeChance = random.Next(1, chanceScale);

            if ((chanceScale - chanceScale * hero.evasiveness) < userEscapeChance)
            {
                return true;
            }
            return false;
        }

        // Hero luck check
        public static bool HeroLuckCheck(Hero hero)
        {
            int chanceScale = 10;
            Random random = new Random();

            int userLuckChance = random.Next(1, chanceScale);

            if ((chanceScale - chanceScale * hero.luck) < userLuckChance)
            {
                return true;
            }
            return false;
        }

        // Adds experience and, for every level gained, lets the player pick a boon.
        public static void HeroLevelCheck(Hero hero, int xp)
        {
            int levelsGained = Combat.ApplyExperience(hero, xp);

            for (int i = 0; i < levelsGained; i++)
            {
                AnsiConsole.Write(
                    new FigletText("LEVEL UP!")
                    .LeftAligned()
                    .Color(Color.Yellow));

                AnsiConsole.Write(new Markup($"[yellow]You are now level {hero.level}.[/]"));
                Console.WriteLine();
                Console.WriteLine();

                HeroChooseBoon(hero);
            }
        }

        // Two runs should not look the same. Every level is a fork in the build.
        public static void HeroChooseBoon(Hero hero)
        {
            string vitality = $"Vitality  (+{Modifiers.boonMaxHealth()} max health, and heal that much)";
            string might = $"Might     (+{Modifiers.boonStrength()} strength)";
            string precision = $"Precision (+{Modifiers.boonCritChance() * 100:F0}% critical chance)";

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose how you grow:")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                    .AddChoices(new[] { vitality, might, precision }));

            if (choice == vitality)
            {
                // Heals by what it grants rather than to full: a full heal every level
                // made careful play and button-mashing score almost the same.
                hero.maxHealth = hero.maxHealth + Modifiers.boonMaxHealth();
                hero.health = Math.Min(hero.maxHealth, hero.health + Modifiers.boonMaxHealth());
                Console.WriteLine($"You feel hardier. Max health is now {hero.maxHealth}, and you recover {Modifiers.boonMaxHealth()} health.");
            }
            else if (choice == might)
            {
                hero.strength = hero.strength + Modifiers.boonStrength();
                Console.WriteLine($"Your grip tightens. Strength is now {hero.strength}.");
            }
            else
            {
                hero.criticalChance = hero.criticalChance + Modifiers.boonCritChance();
                Console.WriteLine($"Your eye sharpens. Critical chance is now {hero.criticalChance * 100:F0}%.");
            }

            HeroDamageCheck(hero);
            Console.WriteLine();
        }

        //Calculates the Hero's damage based on basedamage, Hero level, Sword damage, and Hero strength
        public static void HeroDamageCheck(Hero hero)
        {
            // Combat owns the formula so the balance simulator and the game can never disagree
            hero.currentDamage = Combat.CalculateHeroDamage(hero);
        }

        //If the hero has space in their inventory then randomly select an item
        public static void HeroPickupItem(Hero hero)
        {
            Console.WriteLine();
            Console.WriteLine("You find something.");

            if (Inventory.inventorySpaceCheck(hero.inventory))
            {
                Random rand = new Random();

                int randomItem = rand.Next(0, 2);

                if (randomItem == 0)
                {
                    Console.WriteLine("It's a Health potion");
                    hero.inventory.healthPotion = hero.inventory.healthPotion + 1;
                    Console.WriteLine("You now have " + hero.inventory.healthPotion + " Health Potions in your Inventory.");
                }
                else if (randomItem == 1)
                {
                    Console.WriteLine("It's a Strength potion");
                    hero.inventory.strengthPotion = hero.inventory.strengthPotion + 1;
                    Console.WriteLine("You now have " + hero.inventory.strengthPotion + " Strength Potions in your Inventory.");
                }
            }
        }

        // Gives the Hero a menu system to pick a item to use. If the Hero has none of the item then they can't use it
        public static void HeroUseItem(Hero hero)
        {
            int i = 1000;

            while (i != 0)
            {
                // User Options
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                        .AddChoices(new[] {
                        "Health Potion", "Strength Potion", "Exit Inventory",
                        }));

                if (choice == "Health Potion")
                {
                    if (hero.inventory.healthPotion <= 0)
                    {
                        Console.WriteLine("You can't use a Health Potion because you don't have any!!");
                    }
                    else
                    {
                        // Heal up to maxHealth, but never past it
                        if ((hero.health + Modifiers.healthPotionStrength()) > hero.maxHealth)
                            hero.health = hero.maxHealth;
                        else
                        {
                            hero.health = hero.health + Modifiers.healthPotionStrength();
                        }
                        Console.WriteLine("You now have " + hero.health + " health");
                        hero.inventory.healthPotion = hero.inventory.healthPotion - 1;
                        Console.WriteLine("You now have " + hero.inventory.healthPotion + " Health Potions in your Inventory.");
                    }
                }
                else if (choice == "Strength Potion")
                {
                    if (hero.inventory.strengthPotion <= 0)
                    {
                        Console.WriteLine("You can't use a Strength Potion because you don't have any!!");
                    }
                    else
                    {
                        hero.strength = hero.strength + 10;
                        HeroDamageCheck(hero);
                        Console.WriteLine("You now have " + hero.strength + " strength");
                        hero.inventory.strengthPotion = hero.inventory.strengthPotion - 1;
                        Console.WriteLine("You now have " + hero.inventory.strengthPotion + " Strength Potions in your Inventory.");
                    }
                }
                else
                {
                    i = 0;
                }
            }
        }

        // The opening chest: one Fire blade and one Ice blade. The two enemies you
        // will meet most invert each other's weakness, so neither is the safe pick.
        public static void HeroChooseStartingSword(Hero hero)
        {
            Sword[] swords =
            {
                new Sword("Excalibar", Random.Shared.Next(Modifiers.startingSwordLow(), Modifiers.startingSwordHigh()), "claymore", "Fire"),
                new Sword("Scorn", Random.Shared.Next(Modifiers.startingSwordLow(), Modifiers.startingSwordHigh()), "rapier", "Ice"),
            };

            var table = new Table();
            table.AddColumn("[red]Blade[/]");
            table.AddColumn("[red]Damage[/]");
            table.AddColumn("[red]Element[/]");
            foreach (Sword s in swords)
            {
                table.AddRow(s.name, s.damage.ToString(), s.element);
            }
            AnsiConsole.Write(table);

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What sword do you choose?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                    .AddChoices(new[] { swords[0].name, swords[1].name }));

            Sword picked = choice == swords[0].name ? swords[0] : swords[1];
            hero.inventory.sword = picked;

            AnsiConsole.Write(new Markup($"[blue]You chose a blade with {Markup.Escape(picked.element)} and a damage output of {picked.damage}[/]"));
            Console.WriteLine();

            HeroDamageCheck(hero);
            Console.WriteLine();
        }

        // The treasure at the end of the shimmering path: a blade of the opposite
        // element to the one the Hero is carrying. Taking the risky path is what
        // buys the chance to pivot a build that is badly matched to what is ahead.
        public static void HeroFindSword(Hero hero)
        {
            string currentElement = hero.inventory.sword.element;
            string newElement = currentElement == "Fire" ? "Ice" : "Fire";
            string newName = newElement == "Fire" ? "Emberfang" : "Rimewake";

            Sword found = new Sword(newName, Random.Shared.Next(Modifiers.treasureSwordLow(), Modifiers.treasureSwordHigh()),
                hero.inventory.sword.type, newElement);

            Console.WriteLine();
            AnsiConsole.Write(new Markup($"[yellow]Set into the cave wall is a second blade: {Markup.Escape(found.name)}.[/]"));
            Console.WriteLine();

            var table = new Table();
            table.AddColumn("[red]Blade[/]");
            table.AddColumn("[red]Damage[/]");
            table.AddColumn("[red]Element[/]");
            table.AddRow($"{hero.inventory.sword.name} (carried)", hero.inventory.sword.damage.ToString(), hero.inventory.sword.element);
            table.AddRow($"{found.name} (found)", found.damage.ToString(), found.element);
            AnsiConsole.Write(table);

            string take = $"Take {found.name}";
            string keep = $"Keep {hero.inventory.sword.name}";

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Which blade do you carry out of the cave?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                    .AddChoices(new[] { take, keep }));

            if (choice == take)
            {
                hero.inventory.sword = found;
                Console.WriteLine($"You leave the old blade behind and take {found.name}.");
            }
            else
            {
                Console.WriteLine($"You leave {found.name} where it rests.");
            }

            HeroDamageCheck(hero);
            Console.WriteLine();
        }

        // Displays the Hero stats at any given time
        public static void HeroStats(Hero hero)
        {
            //Bar Chart used as a Health bar. Blacked out maxHealth in order to compare the 2 so Health would go down after damage
            AnsiConsole.Write(new BarChart()
                .Width(70)
                .Label("[red bold underline]Hero Stats[/]")
                .CenterLabel()
                .AddItem("Max Health", hero.maxHealth, Color.Black)
                .AddItem("Health", hero.health, Color.Red));

            //// Create a table for Stats
            var table = new Table();
            var table2 = new Table();

            // Add some columns
            table.AddColumn("[red]Hero Name[/]");
            table.AddColumn("[red]Health[/]");
            table.AddColumn("[red]Strength[/]");
            table.AddColumn("[red]Damage[/]");
            table.AddColumn("[red]Level[/]");
            table.AddColumn("[red]XP[/]");
            table.AddColumn("[red]Crit Chance[/]");

            table2.AddColumn("[red]Crit Damage[/]");
            table2.AddColumn("[red]Evasiveness[/]");
            table2.AddColumn("[red]Health Potions[/]");
            table2.AddColumn("[red]Strength Potions[/]");

            // Add some rows
            table.AddRow(hero.name, hero.health.ToString(), hero.strength.ToString(), hero.currentDamage.ToString(), hero.level.ToString(), hero.xp.ToString(), (hero.criticalChance * 100) + "%");
            table2.AddRow((hero.criticalDamage * 100) + "%", (hero.evasiveness * 100) + "%", hero.inventory.healthPotion.ToString(), hero.inventory.strengthPotion.ToString());

            // Render the table to the console
            AnsiConsole.Write(table);
            AnsiConsole.Write(table2);
        }
    }
}
