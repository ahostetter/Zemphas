using Spectre.Console;

namespace Zemphas
{
    // Room events that are not fights. Each returns false if the Hero died.
    internal static class Events
    {
        public static bool Fountain(Hero hero)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("You wonder if you should take a drink from it?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                    .AddChoices(new[] { "Drink", "Leave" }));

            if (choice == "Drink")
            {
                Console.WriteLine("You take a drink");

                if (HeroManagement.HeroLuckCheck(hero))
                {
                    hero.strength = hero.strength + Modifiers.fountainStrength();
                    HeroManagement.HeroDamageCheck(hero);
                    Console.WriteLine("You feel stronger!!!");
                }
                else
                {
                    hero.health = hero.health - Modifiers.fountainDamage();
                    Console.WriteLine($"You take {Modifiers.fountainDamage()} damage!!!");

                    // The fountain can kill outright, so alive state is updated here too
                    if (hero.health <= 0)
                    {
                        hero.health = 0;
                        hero.alive = false;
                        Console.WriteLine("The water burns through you. You perished...");
                        Console.WriteLine();
                        return false;
                    }
                }
            }
            else
            {
                Console.WriteLine("You leave too worried that the water will hurt you...");
            }

            Console.WriteLine();
            return true;
        }
    }
}
