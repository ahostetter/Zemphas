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
            Enemy Ogre = new Enemy(rnd.Next(1500, 2000), rnd.Next(200, 300));
            int userChoice = 0;
            int chanceScale = 10;
            int critDamage = (int)(zemphas.damage * zemphas.criticalDamage);

            Console.WriteLine("You stand before a hulking giant of a Ogre");
            Console.WriteLine();

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
                        int userCritChance = rnd.Next(1, chanceScale);
                        //Console.WriteLine(userCritChance.ToString());
                        if ((chanceScale - chanceScale * zemphas.criticalChance) < userCritChance)
                        {
                            orgeHealth = (int)(orgeHealth - (critDamage + heroDamage));
                            Console.WriteLine("Hero attacks the Ogre (" + (heroDamage + critDamage) + " HIT POINTS!)");
                            Console.WriteLine("IT IS A CRITICAL HIT!!!!");
                        }
                        else
                        {
                            orgeHealth = orgeHealth - heroDamage;
                            Console.WriteLine("Hero attacks the Ogre (" + heroDamage + " HIT POINTS!)");
                        }
                        if (orgeHealth > 0)
                        {
                            Console.WriteLine("The Ogre has " + orgeHealth + " health now");
                            Console.WriteLine();
                            Console.WriteLine("The Ogre swings his club at you");
                            heroHealth = heroHealth - Ogre.damage;
                            Console.WriteLine("You take " + Ogre.damage + " damage which leaves you with " + heroHealth + " health");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("The Ogre collapsed");
                        }
                    }
                    else if (userChoice == 1)
                    {
                        int userEscapeChance = rnd.Next(1, chanceScale);
                        //Console.WriteLine(userEscapeChance.ToString());
                        if ((chanceScale - chanceScale * zemphas.evasiveness) < userEscapeChance)
                        {
                            escape = true;
                        }
                        else
                        {
                            Console.WriteLine("You were not able to escape!");
                            Console.WriteLine();
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
                            Console.WriteLine();
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
                heroHealth = heroHealth + 300;
                Console.WriteLine("It drops a health potion which brings your health up to " + heroHealth);
            }
        }
    }
}
