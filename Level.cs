using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zemphas
{
    internal class Level
    {
        public static bool complete;

        public static void Level1(Hero hero)
        {
            complete = false;

            Random rnd = new Random();
            int userChoice = 0;
            Sword[] swords = { new Sword("Excalibar", rnd.Next(300, 500), "claymore", "Fire"), new Sword("Scorn", rnd.Next(300, 500), "rapier", "Ice") };

            Console.WriteLine("You wake up in a dark cave and have no idea how you got there.");
            Console.WriteLine("The only light you can see is in the distance from the mouth of the cave");
            Console.WriteLine("You see a treasure chest close by and you walk up to it and open it.");
            Console.WriteLine("Inside you find swords that seemed to be enchanted!");
            Console.WriteLine();

            while (complete == false)
            {
                int i = 0;

                while (i == 0)
                {
                    Console.WriteLine("What weapon do you choose? " + swords[0].name + "[0] or " + swords[1].name + "[1]?");

                    try
                    {
                        userChoice = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("You did not put in a number");
                        userChoice = 1000;
                    }

                    if (userChoice == 0)
                    {
                        Console.WriteLine("You chose a blade with " + swords[0].element + " and a damage output of " + swords[0].damage);
                        hero.inventory.sword = swords[0];
                        i = 1;
                    }
                    else if (userChoice == 1)
                    {
                        Console.WriteLine("You chose a blade with " + swords[1].element + " and a damage output of " + swords[1].damage);
                        hero.inventory.sword = swords[1];
                        i = 1;
                    }
                    else
                    {
                        Console.WriteLine("You did not put in a correct choice");
                    }
                }

                Console.WriteLine("You keep walking but you notice something is near you.");

                Encounters.randomEcounter(hero);

                Console.WriteLine("You see that there are two paths you can take.");
                Console.WriteLine("One way leads down a path with shimmering light that you are sure has some sort of treasure.");
                Console.WriteLine("The other is the way out of the cave. Which way will you go?");
                Console.WriteLine();

                i = 0;

                while (i == 0)
                {

                    try
                    {
                        Console.WriteLine("Path of shimmering light [0] or exit the cave? [1]");

                        userChoice = System.Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("You did not put in a number");
                        userChoice = 1000;
                    }

                    if (userChoice == 0)
                    {
                        Console.WriteLine("You head down the path of shimmering light.");
                        Encounters.randomEcounter(hero);
                        i = 1;
                    }
                    else if (userChoice == 1)
                    {
                        Console.WriteLine("You exit the cave into a forest.");
                        i = 1;
                    }
                    else
                    {
                        Console.WriteLine("You did not put in a correct choice");
                    }
                }

                complete = true;
            }
        }

        public static void Level2(Inventory heroInventory, Hero hero)
        {
            Console.WriteLine(heroInventory.sword.name);
        }
    }
}
