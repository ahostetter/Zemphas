using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zemphas.Enemies;

namespace Zemphas
{
    internal class Encounters
    {
        public static void randomEcounter(Hero encounterHero)
        {
            Random random = new Random();
            int userRoll = random.Next(1, 10);

            if (userRoll <= 3)
            {
                OgreEncounter(encounterHero);
            }
            else if (userRoll > 3 && userRoll < 7)
            {
                WarlockEncounter(encounterHero);
            }
            else
            {
                Console.WriteLine("Another Encounter goes here 3");
                OgreEncounter(encounterHero);
            }

        }

        public static void OgreEncounter(Hero hero)
        {
            Random rnd = new Random();
            Ogre ogre = new Ogre(rnd.Next(1500, 2000), rnd.Next(200, 300));
            int userChoice = 0;
            int chanceScale = 10;
            int critDamage = (int)(hero.damage * hero.criticalDamage);

            Console.WriteLine("You stand before a hulking giant of a Ogre");
            Console.WriteLine();

            int orgeHealth = ogre.health;
            int heroHealth = hero.health;
            int heroDamage = hero.inventory.sword.damage + hero.damage;
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
                        if ((chanceScale - chanceScale * hero.criticalChance) < userCritChance)
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
                            heroHealth = heroHealth - ogre.damage;
                            Console.WriteLine("You take " + ogre.damage + " damage which leaves you with " + heroHealth + " health");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("The Ogre collapsed!");
                        }
                    }
                    else if (userChoice == 1)
                    {
                        int userEscapeChance = rnd.Next(1, chanceScale);
                        //Console.WriteLine(userEscapeChance.ToString());
                        if ((chanceScale - chanceScale * hero.evasiveness) < userEscapeChance)
                        {
                            escape = true;
                        }
                        else
                        {
                            Console.WriteLine("You were not able to escape!");
                            Console.WriteLine();
                            Console.WriteLine("The Ogre swings his club at you");
                            heroHealth = heroHealth - ogre.damage;
                            Console.WriteLine("You take " + ogre.damage + " damage");
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
                Console.WriteLine();
            }
        }

        public static void WarlockEncounter(Hero hero)
        {
            Random rnd = new Random();
            Warlock warlock = new Warlock(rnd.Next(800, 1000), rnd.Next(500, 800));
            int userChoice = 0;
            int chanceScale = 10;
            int critDamage = (int)(hero.damage * hero.criticalDamage);

            Console.WriteLine("You see the red crazy eyes of Warlock as it conjures a spell meant for you");
            Console.WriteLine();

            int warlockHealth = warlock.health;
            int heroHealth = hero.health;
            int heroDamage = hero.inventory.sword.damage + hero.damage;
            bool escape = false;

            while (warlockHealth > 0 && escape == false && heroHealth > 0)
            {

                Console.WriteLine("What will you do? Attack with your Sword [0] or attempt to escape? [1]");
                userChoice = Convert.ToInt32(Console.ReadLine());

                if (heroHealth > 0)
                {
                    if (userChoice == 0)
                    {
                        int userCritChance = rnd.Next(1, chanceScale);
                        //Console.WriteLine(userCritChance.ToString());
                        if ((chanceScale - chanceScale * hero.criticalChance) < userCritChance)
                        {
                            warlockHealth = (int)(warlockHealth - (critDamage + heroDamage));
                            Console.WriteLine("Hero attacks the warlock (" + (heroDamage + critDamage) + " HIT POINTS!)");
                            Console.WriteLine("IT IS A CRITICAL HIT!!!!");
                        }
                        else
                        {
                            warlockHealth = warlockHealth - heroDamage;
                            Console.WriteLine("Hero attacks the warlock (" + heroDamage + " HIT POINTS!)");
                        }
                        if (warlockHealth > 0)
                        {
                            Console.WriteLine("The warlock has " + warlockHealth + " health now");
                            Console.WriteLine();
                            Console.WriteLine("The warlock sends a bolt of lightning your way");
                            heroHealth = heroHealth - warlock.damage;
                            Console.WriteLine("You take " + warlock.damage + " damage which leaves you with " + heroHealth + " health");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("The warlock collapsed!");
                        }
                    }
                    else if (userChoice == 1)
                    {
                        int userEscapeChance = rnd.Next(1, chanceScale);
                        //Console.WriteLine(userEscapeChance.ToString());
                        if ((chanceScale - chanceScale * hero.evasiveness) < userEscapeChance)
                        {
                            escape = true;
                        }
                        else
                        {
                            Console.WriteLine("You were not able to escape!");
                            Console.WriteLine();
                            Console.WriteLine("The warlock sends a cone of frozen air your way");
                            heroHealth = heroHealth - warlock.damage;
                            Console.WriteLine("You take " + warlock.damage + " damage");
                            if (heroHealth <= 0)
                            {
                                Console.WriteLine("The warlock was too much for you.");
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
                Console.WriteLine("You defeated the Warlock!!!");
                heroHealth = heroHealth + 500;
                Console.WriteLine("It drops a health potion which brings your health up to " + heroHealth);
                Console.WriteLine();
            }
        }
    }
}
