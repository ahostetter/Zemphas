using Spectre.Console;

namespace Zemphas
{
    // Room events that are not fights. Each returns false if the Hero died.
    internal static class Events
    {
        // A safe place to stop. Without these a longer journey is pure attrition,
        // since the only other healing is whatever potions happen to drop.
        public static void Rest(Hero hero)
        {
            double before = hero.health;
            double healed = hero.maxHealth * Modifiers.restHealAmount();
            hero.health = Math.Min(hero.maxHealth, hero.health + healed);

            AnsiConsole.Write(new Markup($"[green]You stop, and for a while nothing is trying to kill you.[/]"));
            Console.WriteLine();
            AnsiConsole.Write(new Markup($"[green]You recover {hero.health - before:F0} health, and stand at {hero.health:F0}.[/]"));
            Console.WriteLine();
            Console.WriteLine();
        }

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
