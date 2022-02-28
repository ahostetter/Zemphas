using Spectre.Console;

namespace Zemphas
{
    internal class Level
    {
        public static bool complete;

        public static Hero Level1(Hero hero)
        {
            complete = false;

            Random rnd = new Random();
            Sword[] swords = { new Sword("Excalibar", rnd.Next(300, 500), "claymore", "Fire"), new Sword("Scorn", rnd.Next(300, 500), "rapier", "Ice") };

            HeroManagement.HeroStats(hero);

            Console.WriteLine();
            AnsiConsole.Write(new Markup("[blue]You wake up in a dark cave and have no idea how you got there.[/]"));
            Console.WriteLine();
            AnsiConsole.Write(new Markup("[blue]The only light you can see is in the distance from the mouth of the cave[/]"));
            Console.WriteLine();
            AnsiConsole.Write(new Markup("[blue]You see a treasure chest close by and you walk up to it and open it.[/]"));
            Console.WriteLine();
            AnsiConsole.Write(new Markup("[blue]Inside you find swords that seemed to be enchanted![/]"));
            Console.WriteLine();
            Console.WriteLine();

            while (complete == false)
            {
                // Ask user what sword the Hero wants
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What sword do you choose?")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                        .AddChoices(new[] {
                        swords[0].name, swords[1].name,
                        }));

                if (choice == swords[0].name)
                {
                    AnsiConsole.Write(new Markup($"[blue]You chose a blade with {swords[0].element} and a damage output of {swords[0].damage}[/]"));
                    hero.inventory.sword = swords[0];
                    Console.WriteLine();
                }
                else if (choice == swords[1].name)
                {
                    Console.WriteLine($"You chose a blade with {swords[1].element} and a damage output of {swords[1].damage}");
                    hero.inventory.sword = swords[1];
                    Console.WriteLine();
                }

                AnsiConsole.Write(new Markup("[blue]You keep walking but you notice something is near you.[/]"));
                Console.WriteLine();

                Encounters.randomEcounter(hero);

                if (hero.alive == false)
                {
                    Console.WriteLine("You lost");
                    Console.WriteLine();
                    break;
                }

                AnsiConsole.Write(new Markup("[blue]You see that there are two paths you can take.[/]"));
                Console.WriteLine();
                AnsiConsole.Write(new Markup("[blue]One way leads down a path with shimmering light.[/]"));
                Console.WriteLine();
                AnsiConsole.Write(new Markup("[blue]The other is the way out of the cave.[/]"));
                Console.WriteLine();
                Console.WriteLine();

                // Ask user what sword the Hero wants
                choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Which path do you choose?")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                        .AddChoices(new[] {
                        "Shimmery Light", "Exit the Cave",
                        }));

                if (choice == "Shimmery Light")
                {
                    Console.WriteLine("You head down the path of shimmering light.");
                    Encounters.randomEcounter(hero);
                    if (hero.alive == true)
                    {
                        HeroManagement.HeroPickupItem(hero);
                        Console.WriteLine("You exit the cave into a forest.");
                    }
                    else
                    {
                        Console.WriteLine("You lost...");
                        Console.WriteLine();
                        break;
                    }
                }
                else if (choice == "Exit the Cave")
                {
                    Console.WriteLine("You exit the cave into a forest.");
                }
                else
                {
                    Console.WriteLine("You did not put in a correct choice");
                }
                complete = true;
            }
            return hero;
        }

        public static Hero Level2(Hero hero)
        {
            HeroManagement.HeroStats(hero);
            Console.WriteLine("Loading 2nd Level");
            return hero;
        }
    }
}
