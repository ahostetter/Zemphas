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
        //public static Hero zemphas = new Hero("Zemphas", 2000, 200, .8, .5, .4);
        //public static Inventory zemphasInventory = new Inventory(new Sword("Dull Blade", 20, "dagger", "None"), 3);

        public static void Level1(Inventory heroInventory, Hero hero)
        {
            complete = false;

            Random rnd = new Random();

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

                    int userChoice = System.Convert.ToInt32(Console.ReadLine());

                    if (userChoice == 0)
                    {
                        Console.WriteLine("You chose a blade with " + swords[0].element + " and a damage output of " + swords[0].damage);
                        heroInventory.sword = swords[0];
                        i = 1;
                    }
                    else if (userChoice == 1)
                    {
                        Console.WriteLine("You chose a blade with " + swords[1].element + " and a damage output of " + swords[1].damage);
                        heroInventory.sword = swords[1];
                        i = 1;
                    }
                    else
                    {
                        Console.WriteLine("You did not put in a correct choice");
                    }
                }

                Console.WriteLine("You keep walking but you notice something is near you.");

                Encounters.randomEcounter(heroInventory, hero);
                complete = true;
            }
        }

        public static void Level2(Inventory heroInventory, Hero hero)
        {
            Console.WriteLine(heroInventory.sword.name);
        }
    }
}
