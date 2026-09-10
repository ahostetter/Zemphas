using Zemphas;
using Spectre.Console;


// Console resizing is Windows-only, and even there the new Windows Terminal
// refuses it. Guarded so the game runs on macOS and Linux instead of throwing
// on the first line, and so a refusal is shrugged off rather than fatal.
if (OperatingSystem.IsWindows())
{
    try
    {
        Console.WindowWidth = 75;
        Console.WindowHeight = 50;
    }
    catch (Exception)
    {
        // Terminal would not be resized; the game plays fine at whatever size it is.
    }
}

// Loaded and validated once at startup so a broken world file fails immediately
// with a useful message, rather than stranding the player mid-run.
WorldData world;
try
{
    world = Adventure.Load();
}
catch (Exception ex)
{
    AnsiConsole.Write(new Markup($"[red]Could not load the world file:[/] {Markup.Escape(ex.Message)}"));
    Console.WriteLine();
    return;
}

// Content check for CI or a quick sanity pass after editing world.json
if (args.Contains("--validate"))
{
    Console.WriteLine("World file loaded and validated.");
    Console.Write(Adventure.Describe(world));
    return;
}

bool wantToPlay = true;

while (wantToPlay)
{
    AnsiConsole.Write(
        new FigletText("    Zemphas")
        .LeftAligned()
        .Color(Color.Red));
    
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
    Console.WriteLine();
    Console.WriteLine();
    
    // User Options
    var choice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
            .AddChoices(new[] {
        "Start Game", "Exit",
            }));
    
    if (choice == "Start Game")
    {
        // Asynchronous
        await AnsiConsole.Progress()
            .AutoClear(true)
            .StartAsync(async ctx =>
            {
                // Define tasks
                var task1 = ctx.AddTask("[red]Loading Game[/]");
    
                while (!ctx.IsFinished)
                {
                    // Delay load
                    await Task.Delay(10);
    
                    // Increment
                    task1.Increment(2);
                }
            });
    }
    else if (choice == "Exit")
    {
        Console.WriteLine("Exiting Game");
        wantToPlay = false;
    }
    else
    {
        Console.WriteLine("You did not put in a correct choice");
    }

    if (wantToPlay == false)
    {
        break;
    }

    bool warlordDefeated = false;

    {
        // Initiates the starting inventory
        Inventory zemphasInventory = new Inventory(new Sword("Dull Blade", 20, "dagger", "None"), 0, 0, 3);

        // Loads the Hero with all of the base stats, and the hero stores the inventory
        Hero zemphas = new Hero(Modifiers.heroName(), Modifiers.maxHeroHealth(), Modifiers.heroHealth(), Modifiers.heroStrength(), Modifiers.heroCurrentDamage(),
            Modifiers.heroBaseDamage(), Modifiers.heroStartingLevel(), Modifiers.heroXP(), Modifiers.heroCritChance(), Modifiers.heroCritDamage(),
            Modifiers.heroEvasiveness(), Modifiers.heroLuck(), zemphasInventory, Modifiers.heroAlive());

        // Without this the stats panel greets the player with a Damage of 0,
        // because currentDamage was only ever calculated once a fight began.
        HeroManagement.HeroDamageCheck(zemphas);

        HeroManagement.HeroStats(zemphas);

        // Walks the room graph from Content/world.json instead of two hardcoded levels
        RunOutcome outcome = Adventure.Run(zemphas, world);
        warlordDefeated = outcome == RunOutcome.Victory;

        Console.WriteLine();

        if (outcome == RunOutcome.Died)
        {
            AnsiConsole.Write(
                new FigletText("YOU DIED")
                .LeftAligned()
                .Color(Color.Red));
            AnsiConsole.Write(new Markup("[red]Zemphas falls, and the cave keeps its secret.[/]"));
        }
        else if (warlordDefeated)
        {
            AnsiConsole.Write(
                new FigletText("VICTORY")
                .LeftAligned()
                .Color(Color.Green));
            AnsiConsole.Write(new Markup("[green]The Warlord is dead. You beat the Game!!![/]"));
        }
        else
        {
            // Alive, but the Warlord still sits his throne. Surviving is not winning.
            AnsiConsole.Write(
                new FigletText("YOU FLED")
                .LeftAligned()
                .Color(Color.Yellow));
            AnsiConsole.Write(new Markup("[yellow]You live, but the Warlord still holds the castle.[/]"));
        }

        Console.WriteLine();
        Console.WriteLine();
    }
}






