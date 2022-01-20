using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zemphas
{
    internal class Encounters
    {
        public static void OgreEncounter(Inventory heroInventory, Hero zemphas)
        {
            Random rnd = new Random();
            Enemy Ogre = new Enemy(rnd.Next(1000, 1500), rnd.Next(200, 300));
            int userChoice = 0;
            int chanceScale = 10;

            Console.WriteLine("You stand before a hulking giant of a Ogre");

            int orgeHealth = Ogre.health;
            int heroHealth = zemphas.health;
            int heroDamage = heroInventory.sword.damage + zemphas.damage;
            bool escape = false;

            while (orgeHealth > 0 && escape == false && heroHealth > 0)
            {

                Console.WriteLine("What will you do? Attack with your Sword [0] or attempt to escape? [1]");
                userChoice = Convert.ToInt32(Console.ReadLine());

                if (heroHealth > 0)
                {
                    if (userChoice == 0)
                    {
                        orgeHealth = orgeHealth - heroDamage;
                        Console.WriteLine("Hero attacks the Ogre (" + heroDamage + " HIT POINTS!)");

                        if (orgeHealth > 0)
                        {
                            Console.WriteLine("The Ogre has " + orgeHealth + " health now");
                            Console.WriteLine("The Ogre swings his club at you");
                            heroHealth = heroHealth - Ogre.damage;
                            Console.WriteLine("You take " + Ogre.damage + " damage which leaves you with " + heroHealth + " health");
                        }
                        else
                        {
                            Console.WriteLine("The Ogre collapsed");
                        }
                    }
                    else if (userChoice == 1)
                    {

                        int userEscapeChance = rnd.Next(1, chanceScale);
                        Console.WriteLine(userEscapeChance.ToString());
                        if ((chanceScale - chanceScale * zemphas.evasiveness) < userEscapeChance)
                        {
                            escape = true;
                        }
                        else
                        {
                            Console.WriteLine("You were not able to escape!");
                            Console.WriteLine("The Ogre swings his club at you");
                            heroHealth = heroHealth - Ogre.damage;
                            Console.WriteLine("You take " + Ogre.damage + " damage");
                            if (heroHealth <= 0)
                            {
                                Console.WriteLine("The Ogre is too much for you.");
                            }
                            else
                            {
                                Console.WriteLine("You have " + heroHealth + " left");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid option");
                    }
                }
            }

            if (escape)
            {
                Console.WriteLine("You successfully escaped!");
            }
            else if (heroHealth <= 0)
            {
                Console.WriteLine("You perished");
            }
            else
            {
                Console.WriteLine("You defeated the Ogre!!!");
            }
        }
    }
}
