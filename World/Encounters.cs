using Zemphas.Enemies;

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
            Ogre ogre = new Ogre(rnd.Next(1500, 2000), rnd.Next(200, 300));
            HeroManagement.HeroDamageCheck(hero);
            int userChoice = 0;
            int chanceScale = 10;
            double critDamage = hero.currentDamage * hero.criticalDamage;

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
                Console.WriteLine("Use Item [2]");
                Console.WriteLine("Check Hero Stats [3]");

                try
                {
                    userChoice = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("You did not put in a number");
                    userChoice = 1000;
                }

                if (hero.health > 0)
                {
                    if (userChoice == 0)
                    {
                        int userCritChance = rnd.Next(1, chanceScale);

                        if ((chanceScale - chanceScale * hero.criticalChance) < userCritChance)
                        {
                            Console.WriteLine();
                            orgeHealth = orgeHealth - (critDamage + heroDamage);
                            Console.WriteLine("Hero attacks the Ogre (" + (heroDamage + critDamage) + " HIT POINTS!)");
                            Console.WriteLine("IT IS A CRITICAL HIT!!!!");
                        }
                        else
                        {
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&5G#&&GBB&@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&!BBG&@&#&@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@P@J5#?&@&#@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@P@J@#PG7@@&&@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@5B&PY@&@G@Y#7@@B@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&^.5#@@@@@@&:5@BP@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&B??##5J?YG#&#J@BY@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&75G#J^.?@@@BG&B@5B@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@BP~~?~77P&@@@BBY@P@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@P##G:@@5JG#@@P?&G&@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&&YB@@@@PYGB@@!&G#@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@^7#@@@@G555@@&BY@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&@@@@@@#!J&@@@B5Y7@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#?@@&&&G#G~5#@@@#PP7&@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@B.PB7B@B.^YJ?&@@@@#P~J&@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@G7?:Y5B&PJ .#@@@@@@&?:7B@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@Y~.JG. ^PJP#@@@@@5.7B#P&@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@&&@@@BPY5G##BY. J.PPG~~P@@5^:7B&J@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@&?!JYG5. !@&5PBG5YGJ!?57:..PBP  ^^~YB@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@&BGJ7P?:7. &@  .G&@@@@@YJJ5PB5YYB!^B@@75@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@5J##@@.5B&BP@P #@@@@@@@   ^~&PGB@@&&&G#!@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@&Y55YPJ J@P@@@@5&@##&&#7:JYJY!.:7?~!~^~?^&@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@#Y:!B. :@&Y?7~^:!YYJ7~:^^~~::^^:.   5G.~?&@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@GY!5&5 ^J@@@@B^  :..7YJ.   ~~..   ?@@@5 :~Y&@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@PG@@@# ?#B&&@@& :75@@@&P  P@@@@@@@@@@@@5?!..@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@: Y@@@&&&.~7@@&@&^ #@@@@@@@@@@@@@@BY!#@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@B:!G#BGGGPGGP!?Y? ~@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@PJPPPGBB##&#P^@#? ~@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@?JYYJJY555555?Y?  J@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@&&BP5J7~:.:   ^7?~:~^^..    .^JG#&@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@#5YYYY55YJ~..5P^?GGJ::5&B#: ?:P:  ~:.^!?JYY5PB#&@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@&GBP@B7!!~^:57&GJBJ.  7@@@5#  PB~#G^~B&#PYYPGB5YYJ?77JG&@@@@@@@@");
                            Console.WriteLine("@@BG@@@@&&#GBB5:.^~7~.5P&@&? .. 5@@@@5&   Y#?5&BJ?P#@@@&#GPBBP7:.^!YB@@@@@@");
                            Console.WriteLine("#??Y?YY!~^^~!^ :#@#7^5B&@@. G@ !@@B&@G@!   ^&&GP#&#P5P#@@@@@&##G5J!~:.!@@@@");
                            Console.WriteLine("@@@@@@#Y~Y&&PYPG^ :?B&@@@~.@@B @@@!@@&G@7    ~B@@&##&@@@@@BBB7.    ^~  ?@@@");
                            Console.WriteLine("@@@@BJPGB@@#7&@#.Y@&GG@@@.&G@P!@@#.@@@G!&#^     ~5&@&B5P5Y&?.P.~ ?YP#  ?@@@");
                            Console.WriteLine("@@@@J&@@@@@&B@P.^5PB&@@@P~&5@B5@@# 5@@@5 ^5P!      .?#@?^#: J5Y@PY#@G  &@@@");
                            Console.WriteLine("@@@@@@@@@@@@5^7#&&#&@@@@~:?Y@@G@@#  5@@&:^?B&Y.       :##. .7B@@@GP@. B@@@@");
                            Console.WriteLine("@@@@@@@@@@5.~PP?!?PJ J@Y..Y!@@B@@#   7B#BY~.                :B@@@#:.^&@@@@@");
                            Console.WriteLine("@@@@@@@@&^.7P5?YGP#@YBJ?.B77@@&B@Y  JBJ:.:^!JY5PP5J?777YG&P.  ^J#@!~@@@@@@@");
                            Console.WriteLine("@@@@@@@@! :.  ^..?#5&& P~@7^@@@&&:7G!.!G##BBBBBBBBBBGPP5Y5!  :!. #@7&@@@@@@");
                            Console.WriteLine("@@@@@@@@.^7:Y7JP! :&#! Y#&G &@&BPB7^YB&&&&@&&&##BBBBBGGG5J!!P&~.  P@YB@@@@@");
                            Console.WriteLine("@@@@@@@G77~&@@&P!Y~ Y&BP&@&:J###5~Y#BGPPPGG#&@@@@@@@@@@@@@@@@@#   :@@BP#@@@");
                            Console.WriteLine("@@@@@@@Y7^@@@@@&B?    :7J555J!:  !?JYPB#&@@@@@@@@@@@@@@@@@@@@@@^   !P@@P!#@");
                            Console.WriteLine("@@@@@@PJ.@@&GBBB&@&BY!^.......^7PB&@@@@@@@@@@@@@@@@@@@@@@@@@@@@&J?^..7#&#BP");
                            Console.WriteLine("@@@@@G? J@J.J#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&BGP5PG#&");
                            Console.WriteLine("@@@@B? .@7 #@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@G~ :#@@BJB@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@BP  YPPG##PP#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@BG5JJYYYY55JJ@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine();

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
                        HeroManagement.HeroUseItem(hero);
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
                hero.alive = false;
            }
            else
            {
                Console.WriteLine("You defeated the Ogre!!!");
                HeroManagement.HeroLevelCheck(hero, Modifiers.OgreExperience());
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
                Console.WriteLine("Use Item [2]");
                Console.WriteLine("Check Hero Stats [3]");

                try
                {
                    userChoice = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
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
                            Console.WriteLine();
                            warlockHealth = warlockHealth - (critDamage + heroDamage);
                            Console.WriteLine("Hero attacks the warlock (" + (heroDamage + critDamage) + " HIT POINTS!)");
                            Console.WriteLine("IT IS A CRITICAL HIT!!!!");
                        }
                        else
                        {
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&5G#&&GBB&@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&!BBG&@&#&@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@P@J5#?&@&#@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@P@J@#PG7@@&&@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@5B&PY@&@G@Y#7@@B@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&^.5#@@@@@@&:5@BP@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&B??##5J?YG#&#J@BY@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&75G#J^.?@@@BG&B@5B@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@BP~~?~77P&@@@BBY@P@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@P##G:@@5JG#@@P?&G&@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&&YB@@@@PYGB@@!&G#@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@^7#@@@@G555@@&BY@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&@@@@@@#!J&@@@B5Y7@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#?@@&&&G#G~5#@@@#PP7&@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@B.PB7B@B.^YJ?&@@@@#P~J&@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@G7?:Y5B&PJ .#@@@@@@&?:7B@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@Y~.JG. ^PJP#@@@@@5.7B#P&@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@&&@@@BPY5G##BY. J.PPG~~P@@5^:7B&J@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@&?!JYG5. !@&5PBG5YGJ!?57:..PBP  ^^~YB@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@&BGJ7P?:7. &@  .G&@@@@@YJJ5PB5YYB!^B@@75@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@5J##@@.5B&BP@P #@@@@@@@   ^~&PGB@@&&&G#!@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@&Y55YPJ J@P@@@@5&@##&&#7:JYJY!.:7?~!~^~?^&@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@#Y:!B. :@&Y?7~^:!YYJ7~:^^~~::^^:.   5G.~?&@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@GY!5&5 ^J@@@@B^  :..7YJ.   ~~..   ?@@@5 :~Y&@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@PG@@@# ?#B&&@@& :75@@@&P  P@@@@@@@@@@@@5?!..@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@: Y@@@&&&.~7@@&@&^ #@@@@@@@@@@@@@@BY!#@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@B:!G#BGGGPGGP!?Y? ~@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@PJPPPGBB##&#P^@#? ~@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@?JYYJJY555555?Y?  J@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@&&BP5J7~:.:   ^7?~:~^^..    .^JG#&@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@@@#5YYYY55YJ~..5P^?GGJ::5&B#: ?:P:  ~:.^!?JYY5PB#&@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@@@@@@@@@&GBP@B7!!~^:57&GJBJ.  7@@@5#  PB~#G^~B&#PYYPGB5YYJ?77JG&@@@@@@@@");
                            Console.WriteLine("@@BG@@@@&&#GBB5:.^~7~.5P&@&? .. 5@@@@5&   Y#?5&BJ?P#@@@&#GPBBP7:.^!YB@@@@@@");
                            Console.WriteLine("#??Y?YY!~^^~!^ :#@#7^5B&@@. G@ !@@B&@G@!   ^&&GP#&#P5P#@@@@@&##G5J!~:.!@@@@");
                            Console.WriteLine("@@@@@@#Y~Y&&PYPG^ :?B&@@@~.@@B @@@!@@&G@7    ~B@@&##&@@@@@BBB7.    ^~  ?@@@");
                            Console.WriteLine("@@@@BJPGB@@#7&@#.Y@&GG@@@.&G@P!@@#.@@@G!&#^     ~5&@&B5P5Y&?.P.~ ?YP#  ?@@@");
                            Console.WriteLine("@@@@J&@@@@@&B@P.^5PB&@@@P~&5@B5@@# 5@@@5 ^5P!      .?#@?^#: J5Y@PY#@G  &@@@");
                            Console.WriteLine("@@@@@@@@@@@@5^7#&&#&@@@@~:?Y@@G@@#  5@@&:^?B&Y.       :##. .7B@@@GP@. B@@@@");
                            Console.WriteLine("@@@@@@@@@@5.~PP?!?PJ J@Y..Y!@@B@@#   7B#BY~.                :B@@@#:.^&@@@@@");
                            Console.WriteLine("@@@@@@@@&^.7P5?YGP#@YBJ?.B77@@&B@Y  JBJ:.:^!JY5PP5J?777YG&P.  ^J#@!~@@@@@@@");
                            Console.WriteLine("@@@@@@@@! :.  ^..?#5&& P~@7^@@@&&:7G!.!G##BBBBBBBBBBGPP5Y5!  :!. #@7&@@@@@@");
                            Console.WriteLine("@@@@@@@@.^7:Y7JP! :&#! Y#&G &@&BPB7^YB&&&&@&&&##BBBBBGGG5J!!P&~.  P@YB@@@@@");
                            Console.WriteLine("@@@@@@@G77~&@@&P!Y~ Y&BP&@&:J###5~Y#BGPPPGG#&@@@@@@@@@@@@@@@@@#   :@@BP#@@@");
                            Console.WriteLine("@@@@@@@Y7^@@@@@&B?    :7J555J!:  !?JYPB#&@@@@@@@@@@@@@@@@@@@@@@^   !P@@P!#@");
                            Console.WriteLine("@@@@@@PJ.@@&GBBB&@&BY!^.......^7PB&@@@@@@@@@@@@@@@@@@@@@@@@@@@@&J?^..7#&#BP");
                            Console.WriteLine("@@@@@G? J@J.J#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&BGP5PG#&");
                            Console.WriteLine("@@@@B? .@7 #@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@@G~ :#@@BJB@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@BP  YPPG##PP#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine("@@BG5JJYYYY55JJ@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                            Console.WriteLine();

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
                        HeroManagement.HeroUseItem(hero);
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
                hero.alive = false;
            }
            else
            {
                Console.WriteLine("You defeated the Warlock!!!");
                HeroManagement.HeroLevelCheck(hero, Modifiers.WarlockExperience());
                Console.WriteLine(hero.level);
                Console.WriteLine(hero.xp);
                HeroManagement.HeroDamageCheck(hero);
                HeroManagement.HeroPickupItem(hero);
                Console.WriteLine();
            }
        }
    }
}
