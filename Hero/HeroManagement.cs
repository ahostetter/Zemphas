using Spectre.Console;

namespace Zemphas
{
    internal class HeroManagement
    {
        // If hero xp is over 100 then add 1 to Hero level.
        public static void HeroLevelCheck(Hero hero, int xp)
        {
            hero.xp = hero.xp + xp;

            if (hero.xp >= 100)
            {
                hero.level = hero.level + 1;
                hero.xp = hero.xp - 100;
            }
        }

        //Calculates the Hero's damage based on basedamage, Hero level, Sword damage, and Hero strength
        public static void HeroDamageCheck(Hero hero)
        {
            hero.currentDamage = (hero.baseDamage + (hero.level * Modifiers.scaleLevel()) * hero.baseDamage) + hero.inventory.sword.damage + (hero.strength * Modifiers.scaleStrength());
        }

        //If the hero has space in their inventory then randomly select an item
        public static void HeroPickupItem(Hero hero)
        {
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
                        hero.health = hero.health + 300;
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

        // Displays the Hero stats at any given time
        public static void HeroStats(Hero hero)
        {
            Console.WriteLine();
            Console.WriteLine("|Name:" + hero.name + "|Health:" + hero.health + "|Strength:" + hero.strength + "|Damage:" + hero.currentDamage + "|");
            Console.WriteLine("|Level:" + hero.level + "|XP:" + hero.xp + "|CritChance:" + (hero.criticalChance * 100) + "%" + "|CritDamage:" + (hero.criticalDamage * 100) + "%|");
            Console.WriteLine("|Evasiveness:" + (hero.evasiveness * 100) + "%" + "|Health Potions:" + hero.inventory.healthPotion + "|Strength Potions:" + hero.inventory.strengthPotion + "|");
            Console.WriteLine();

            //Bar Chart used as a Health bar. Blacked out maxHealth in order to compare the 2 so Health would go down after damage
            AnsiConsole.Write(new BarChart()
                .Width(70)
                .Label("[red bold underline]Hero Stats[/]")
                .CenterLabel()
                .AddItem("", hero.maxHealth, Color.Black)
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
