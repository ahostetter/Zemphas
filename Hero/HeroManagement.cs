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
            int userChoice = 1000;

            while (userChoice != 0)
            {
                userChoice = 1000;

                Console.WriteLine("What item do you want to use?");
                Console.WriteLine();
                Console.WriteLine("Health Potion [1]");
                Console.WriteLine("Strength Potion [2]");
                Console.WriteLine("Exit Inventory [0]");

                try
                {
                    userChoice = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("You did not put in a number");
                }

                if (userChoice == 1)
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
                else if (userChoice == 2)
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

            //// Create a table
            var table = new Table();

            // Add some columns
            table.AddColumn("[red]Hero Name[/]");
            table.AddColumn("[red]Health[/]");
            table.AddColumn("[red]Strength[/]");
            table.AddColumn("[red]Damage[/]");
            table.AddColumn("[red]Level[/]");
            table.AddColumn("[red]XP[/]");
            table.AddColumn("[red]Crit Chance[/]");

            // Add some rows
            table.AddRow(hero.name, hero.health.ToString(), hero.strength.ToString(), hero.currentDamage.ToString(), hero.level.ToString(), hero.xp.ToString(), (hero.criticalDamage * 100) + "%");
            //table.AddRow(new Markup("[blue]Corgi[/]"), new Panel("Waldo"));

            // Render the table to the console
            AnsiConsole.Write(table);
        }
    }
}
