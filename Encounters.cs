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
            HeroManagement.HeroDamageCheck(hero);
            int userChoice = 0;
            int chanceScale = 10;
            double critDamage = hero.currentDamage * hero.criticalDamage;

            //HeroManagement.HeroStats(hero);

            Console.WriteLine("You stand before a hulking giant of a Ogre");
            Console.WriteLine();

            double orgeHealth = ogre.health;
            double heroDamage = hero.currentDamage;
            bool escape = false;

            while (orgeHealth > 0 && escape == false && hero.health > 0)
            {

                Console.WriteLine("What will you do?");
                Console.WriteLine();
                Console.WriteLine("Attack with your Sword [0]");
                Console.WriteLine("Try to Escape [1]");
                Console.WriteLine("Use health potion [2]");
                Console.WriteLine("Check Hero Stats [3]");

                try
                {
                    userChoice = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("You did not put in a number");
                    userChoice = 1000;
                }

                if (hero.health > 0)
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
                            hero.health = hero.health - ogre.damage;
                            Console.WriteLine("You take " + ogre.damage + " damage which leaves you with " + hero.health + " health");
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
                            hero.health = hero.health - ogre.damage;
                            Console.WriteLine("You take " + ogre.damage + " damage");
                            if (hero.health <= 0)
                            {
                                Console.WriteLine("The Ogre is too much for you.");
                            }
                            else
                            {
                                Console.WriteLine("You have " + hero.health + " left");
                            }
                            Console.WriteLine();
                        }
                    }
                    else if (userChoice == 2)
                    {
                        HeroManagement.HeroUseHealth(hero);
                    }
                    else if (userChoice == 3)
                    {
                        HeroManagement.HeroStats(hero);
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
            else if (hero.health <= 0)
            {
                Console.WriteLine("You perished");
            }
            else
            {
                Console.WriteLine("You defeated the Ogre!!!");
                HeroManagement.HeroLevelCheck(hero, 50);
                Console.WriteLine(hero.level);
                Console.WriteLine(hero.xp);
                HeroManagement.HeroDamageCheck(hero);
                HeroManagement.HeroPickupItem(hero);
                Console.WriteLine();
            }
        }

        public static void WarlockEncounter(Hero hero)
        {
            Random rnd = new Random();
            Warlock warlock = new Warlock(rnd.Next(800, 1000), rnd.Next(500, 800));
            HeroManagement.HeroDamageCheck(hero);
            int userChoice = 0;
            int chanceScale = 10;
            double critDamage = hero.currentDamage * hero.criticalDamage;

            //HeroManagement.HeroStats(hero);

            Console.WriteLine("You see the red crazy eyes of Warlock as it conjures a spell meant for you");
            Console.WriteLine();

            double warlockHealth = warlock.health;
            double heroDamage = hero.currentDamage;
            bool escape = false;

            while (warlockHealth > 0 && escape == false && hero.health > 0)
            {

                Console.WriteLine("What will you do?");
                Console.WriteLine();
                Console.WriteLine("Attack with your Sword [0]");
                Console.WriteLine("Try to Escape [1]");
                Console.WriteLine("Use health potion [2]");
                Console.WriteLine("Check Hero Stats [3]");

                try
                {
                    userChoice = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("You did not put in a number");
                    userChoice = 1000;
                }

                if (hero.health > 0)
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
                            hero.health = hero.health - warlock.damage;
                            Console.WriteLine("You take " + warlock.damage + " damage which leaves you with " + hero.health + " health");
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
                            hero.health = hero.health - warlock.damage;
                            Console.WriteLine("You take " + warlock.damage + " damage");
                            if (hero.health <= 0)
                            {
                                Console.WriteLine("The warlock was too much for you.");
                            }
                            else
                            {
                                Console.WriteLine("You have " + hero.health + " left");
                            }
                            Console.WriteLine();
                        }
                    }
                    else if (userChoice == 2)
                    {
                        HeroManagement.HeroUseHealth(hero);
                    }
                    else if (userChoice == 3)
                    {
                        HeroManagement.HeroStats(hero);
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
            else if (hero.health <= 0)
            {
                Console.WriteLine("You perished");
            }
            else
            {
                Console.WriteLine("You defeated the Warlock!!!");
                HeroManagement.HeroLevelCheck(hero, 75);
                Console.WriteLine(hero.level);
                Console.WriteLine(hero.xp);
                HeroManagement.HeroDamageCheck(hero);
                HeroManagement.HeroPickupItem(hero);
                Console.WriteLine();
            }
        }
    }
}
