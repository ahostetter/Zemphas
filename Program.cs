using Zemphas;
using System;

Console.WindowWidth = 75;
Console.WindowHeight = 70;

bool wantToPlay = true;

while (wantToPlay)
{
    int userChoice = 1000;
    int i = 0;

    while (i == 0)
    {
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&#&@@@@@@#&@@@@@@@@@@@@@@@@@@@@@");
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#G5YB@@@@#GYY@@@@@@@@@&#GPP&&#@@@@");
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#GG5Y5?B@@&G5Y!P@@@@@&#BPYJJYJYYY#@@@");
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@#&@&&&@@@&G5GGPP55Y?G&BPPJ~?&@#BBGPP5YY555YY5#@@@@");
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@BPYGBGGPPPB5JPBBGPGPJY5PGG5JYBBGP5Y5555PPP55P#@@@@@@");
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@&BBPPGG5PP?JPY5YGGGBGGGPPYPB#BBGG5PPPPPPGGP5PP5P@@@@@@@");
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@BPGGPGPJ!Y5Y5PYPPPGG5JPPYPPB#BBGGBGGPGPPPPPPB#BP#@@@@@@@");
        Console.WriteLine("@@@@@@@@@@@@@@@@@&BGB5YJYP5J555555GGGP555GYPGBB#B#GGP5PY5BB#BB#BPB&@@@@@@@@");
        Console.WriteLine("@@@@@@@@@@@@@@@@B5PGYJJ??5P55PP5PP555PBGGGB##GB#BG5YPG####BBBBPYP@@@@@@@@@@");
        Console.WriteLine("@@@@@@@@@@@@@@@@BP5J55Y5P5YPP5J5PYJYPPGGBBGB#GGPJJY5BBBBBBBGBG5PB@@@@@@@@@@");
        Console.WriteLine("@@@@@@@@@@@@@@@@&YJ5GY5PP555YJYPP5GB###BGGPPPPY7JG##GGPP5PGBBG@@@@@@@@@@@@@");
        Console.WriteLine("@@@@@@@@@@@@@@@&#BPGBGPGGGPGB###&&&&#PPBP55P55JYG#####BP55YPPYB@@@@@@@@@@@@");
        Console.WriteLine("@@@@@@@@@@&#PGGGPPPPGGGB&@&&&#BBP5GB55Y55YPGPPGPGBBBGGBBGPP55555#@@@@@@@@@@");
        Console.WriteLine("@@@@@@&PJGG555YYYY55YY55PGBG55YYJ?55?YY5Y5GBB###G5GGPPGP55PPGG5?Y5G@@@@@@@@");
        Console.WriteLine("@@@@@Y??JP5YPPYY5PP5555555555YYJJJJJYPG5Y5G5GBPGG5J5P5YJ?JY5PYJYGGG&@@@@@@@");
        Console.WriteLine("@@@@#7J75B5JPY5PP5PYJYGG5P5YYJYYY55PGGPJYPBYPBP555YJYY5J5PP5PY5Y5@@@@@@@@@@");
        Console.WriteLine("@@@@PJYJ5YJ5JYPGPJJJJJJ5555YJJJYY55Y5GP5GBGYYPP555YJ77YGPGY5YYYJJ@@@@@@@@@@");
        Console.WriteLine("@@@@5?Y5J?YY5BBGY?77?J?JYJJY5PGG#&PGPGBB#&Y?J5Y5GGGPYJ?BBPPJYJ??7J&@@@@@@@@");
        Console.WriteLine("@@@@BJJYJ5BBB&B#BB##BB##B##&&&#BGB555#&&#P57?YG#BBBGGPPPGPG5YJJ5#PYG@@@@@@@");
        Console.WriteLine("@@@@@B5YB@@@&@@&B&&&BBB#GGGPPPGB##&&&@@@PYYJ5B&##BBGGPPPPPPPY5J5B##5P@@@@@@");
        Console.WriteLine("@@@@@@&&@@@@@&@&###G55G55Y5G#&@@@@@@@@@BJ5G&&#&##BBGGPPPPG5GY5?P#&&&B&@@@@@");
        Console.WriteLine("@@@@@@@@@@@@@B5#P5GGPP5JP#@@@@@@@@@@@@BY5GBGB###BGGP5YYP5G55YJJB@@@@@@@@@@@");
        Console.WriteLine("@@@@@@@@@@@@#GP55PP5Y?P&@@@@@@@@@@@@@G75BB####BBGPP5Y?YP5PPY5JY#@@@@@@@@@@@");
        Console.WriteLine("@@@@@@@@@@@#JJ5PGPJYY5G@@@@@@@@@@@@@57JG#B#BBGPP55Y55PY55555YJJ#@@@@@@@@@@@");
        Console.WriteLine("@@@@@@@@@@@&BPPGPGGGGGG#@@@@@@@@@@&5?YGG5GBP55YYYY?JYYJJYYYYJYPP@@@@@@@@@@@");
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@&@@@@@@@@@@@@@#?!P#BGGGP5YYYYYYJYJYYJYYY5?GBG&@@@@@@@@@@");
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@P77J5BBGP5Y55YYYYJYJJYY?JYYYYBBBG@@@@@@@@@@");

        Console.WriteLine("Play Game [0]");
        Console.WriteLine("Exit [1]");

        try
        {
            userChoice = Convert.ToInt32(Console.ReadLine());
        }
        catch (Exception)
        {
            Console.WriteLine("You did not put in a number");
            userChoice = 1000;
        }

        if (userChoice == 0)
        {
            Console.WriteLine("Starting Game");
            Console.WriteLine();
            i = 1;
        }
        else if (userChoice == 1)
        {
            Console.WriteLine("Exiting Game");
            i = 1;
            wantToPlay = false;
        }
        else
        {
            Console.WriteLine("You did not put in a correct choice");
        }
    }

    if (wantToPlay == false)
    {
        break;
    }

    bool heroAlive = true;
    bool heroWin = false;

    while (heroAlive == true && heroWin == false)
    {
        // Initiates the starting inventory
        Inventory zemphasInventory = new Inventory(new Sword("Dull Blade", 20, "dagger", "None"), 0, 0, 3);

        // Loads the Hero with all of the base stats, and the hero stores the inventory
        Hero zemphas = new Hero(Modifiers.heroName(), Modifiers.heroHealth(), Modifiers.heroStrength(), Modifiers.heroCurrentDamage(),
            Modifiers.heroBaseDamage(), Modifiers.heroStartingLevel(), Modifiers.heroXP(), Modifiers.heroCritChance(), Modifiers.heroCritDamage(),
            Modifiers.heroEvasiveness(), zemphasInventory, Modifiers.heroAlive());

        // Load state of Hero after first level
        zemphas = Level.Level1(zemphas);

        // Checks to make sure Hero is still alive and if he is then load state of Hero into Second Level
        if (zemphas.alive)
        {
            Level.Level2(zemphas);
        }

        if (!zemphas.alive)
        {
            heroAlive = false;
        }

        if (zemphas.alive)
        {
            heroWin = true;
        }

        if (heroWin)
        {
            Console.WriteLine("You beat the Game!!!");
        }
    }
}






