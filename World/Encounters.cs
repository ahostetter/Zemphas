using Zemphas.Enemies;
using Spectre.Console;

namespace Zemphas
{
    internal class Encounters
    {
        public static bool randomEcounter(Hero encounterHero)
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
            return false;
        }

        public static void OgreEncounter(Hero hero)
        {
            Random rnd = new Random();
            Ogre ogre = new Ogre(rnd.Next(1500, 2000), rnd.Next(200, 300), Modifiers.ogreAccuracy());
            HeroManagement.HeroDamageCheck(hero);
            int chanceScale = 10;
            double critDamage = hero.currentDamage * hero.criticalDamage;

            AnsiConsole.Write(new Markup("[blue]You stand before a hulking giant of a Ogre[/]"));
            Console.WriteLine();
            Console.WriteLine();

            double orgeHealth = ogre.health;
            double heroDamage = hero.currentDamage;
            bool escape = false;

            while (orgeHealth > 0 && escape == false && hero.health > 0)
            {
                // User Options
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .PageSize(10)
                        .MoreChoicesText("[blue](Move up and down to reveal more choices)[/]")
                        .AddChoices(new[] {
                        "Attack", "Try to Escape", "Use Item", "Check Hero Stats",
                        }));

                if (hero.health > 0)
                {
                    if (choice == "Attack")
                    {
                        int diceRoll = rnd.Next(1, chanceScale);

                        if ((chanceScale - chanceScale * hero.criticalChance) < diceRoll)
                        {
                            AnsiConsole.Write(
                            new FigletText("CRITICAL!!")
                            .LeftAligned()
                            .Color(Color.Red));
                            //Thread.Sleep(500);

                            Console.WriteLine();
                            orgeHealth = orgeHealth - (critDamage + heroDamage);
                            Console.WriteLine("Hero attacks the Ogre (" + (heroDamage + critDamage) + " HIT POINTS!)");
                            Console.WriteLine("IT IS A CRITICAL HIT!!!!");
                        }
                        else
                        {
                            AnsiConsole.Write(
                            new FigletText("CHARGE!!")
                            .LeftAligned()
                            .Color(Color.Red));
                            //Thread.Sleep(500);

                            orgeHealth = orgeHealth - heroDamage;
                            Console.WriteLine("Hero attacks the Ogre (" + heroDamage + " HIT POINTS!)");
                        }
                        if (orgeHealth > 0)
                        {
                            Console.WriteLine("The Ogre has " + orgeHealth + " health now");
                            Console.WriteLine();
                            Console.WriteLine("The Ogre swings his club at you");

                            if ((chanceScale - chanceScale * ogre.accuracy) < diceRoll)
                            {
                                hero.health = hero.health - ogre.damage;
                                Console.WriteLine("You take " + ogre.damage + " damage which leaves you with " + hero.health + " health");
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("You dodged the attack");
                            }
                        }
                        else
                        {
                            Console.WriteLine("The Ogre collapsed!");
                        }
                    }
                    else if (choice == "Try to Escape")
                    {
                        int userEscapeChance = rnd.Next(1, chanceScale);

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
                    else if (choice == "Use Item")
                    {
                        HeroManagement.HeroUseItem(hero);
                    }
                    else if (choice == "Check Hero Stats")
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
                hero.alive = false;
            }
            else
            {
                Console.WriteLine("You defeated the Ogre!!!");
                HeroManagement.HeroLevelCheck(hero, Modifiers.ogreExperience());
                HeroManagement.HeroDamageCheck(hero);
                HeroManagement.HeroPickupItem(hero);
                Console.WriteLine();
            }
        }

        public static void WarlockEncounter(Hero hero)
        {
            Random rnd = new Random();
            Warlock warlock = new Warlock(rnd.Next(800, 1000), rnd.Next(500, 800), Modifiers.warlockAccuracy());
            HeroManagement.HeroDamageCheck(hero);
            int chanceScale = 10;
            double critDamage = hero.currentDamage * hero.criticalDamage;

            Console.WriteLine("You see the red crazy eyes of Warlock as it conjures a spell meant for you");
            Console.WriteLine();
            Console.WriteLine();

            double warlockHealth = warlock.health;
            double heroDamage = hero.currentDamage;
            bool escape = false;

            while (warlockHealth > 0 && escape == false && hero.health > 0)
            {
                // User Options
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                        .AddChoices(new[] {
                        "Attack", "Try to Escape", "Use Item", "Check Hero Stats",
                        }));

                if (hero.health > 0)
                {
                    if (choice == "Attack")
                    {
                        int diceRoll = rnd.Next(1, chanceScale);

                        if ((chanceScale - chanceScale * hero.criticalChance) < diceRoll)
                        {
                            AnsiConsole.Write(
                            new FigletText("CRITICAL!!")
                            .LeftAligned()
                            .Color(Color.Red));
                            //Thread.Sleep(500);

                            Console.WriteLine();
                            warlockHealth = warlockHealth - (critDamage + heroDamage);
                            Console.WriteLine("Hero attacks the warlock (" + (heroDamage + critDamage) + " HIT POINTS!)");
                            Console.WriteLine("IT IS A CRITICAL HIT!!!!");
                        }
                        else
                        {
                            AnsiConsole.Write(
                            new FigletText("CHARGE!!")
                            .LeftAligned()
                            .Color(Color.Red));
                            //Thread.Sleep(500);

                            warlockHealth = warlockHealth - heroDamage;
                            Console.WriteLine("Hero attacks the warlock (" + heroDamage + " HIT POINTS!)");
                        }
                        if (warlockHealth > 0)
                        {
                            Console.WriteLine("The warlock has " + warlockHealth + " health now");
                            Console.WriteLine();
                            Console.WriteLine("The warlock sends a bolt of lightning your way");

                            if ((chanceScale - chanceScale * warlock.accuracy) < diceRoll)
                            {
                                hero.health = hero.health - warlock.damage;
                                Console.WriteLine("You take " + warlock.damage + " damage which leaves you with " + hero.health + " health");
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("You dodged the attack!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("The warlock collapsed!");
                        }
                    }
                    else if (choice == "Try to Escape")
                    {
                        int userEscapeChance = rnd.Next(1, chanceScale);

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
                    else if (choice == "Use Item")
                    {
                        HeroManagement.HeroUseItem(hero);
                    }
                    else if (choice == "Check Hero Stats")
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
                hero.alive = false;
            }
            else
            {
                Console.WriteLine("You defeated the Warlock!!!");
                HeroManagement.HeroLevelCheck(hero, Modifiers.warlockExperience());
                HeroManagement.HeroDamageCheck(hero);
                HeroManagement.HeroPickupItem(hero);
                Console.WriteLine();
            }
        }
    }
}
