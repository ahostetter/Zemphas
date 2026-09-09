using Zemphas.Enemies;
using Spectre.Console;

namespace Zemphas
{
    internal class Encounters
    {
        // Every enemy the Hero can run into by chance. Add a new Enemy subclass
        // here and it joins the rotation with no other changes.
        private static readonly Func<Enemy>[] randomEnemies =
        {
            () => new Ogre(),
            () => new Warlock(),
        };

        // Picks one of the available enemies at random and fights it
        public static void randomEcounter(Hero encounterHero)
        {
            Enemy enemy = randomEnemies[Random.Shared.Next(randomEnemies.Length)]();
            Encounter(encounterHero, enemy);
        }

        // Level 2 opens with a specific Ogre rather than a random enemy
        public static void OgreEncounter(Hero hero)
        {
            Encounter(hero, new Ogre());
        }

        // One combat loop for every enemy. All the differences between enemies live
        // on the Enemy itself, so this method never branches on which one it is.
        public static void Encounter(Hero hero, Enemy enemy)
        {
            int chanceScale = 10;

            AnsiConsole.Write(new Markup($"[blue]{Markup.Escape(enemy.introText)}[/]"));
            Console.WriteLine();
            Console.WriteLine();

            double enemyHealth = enemy.health;
            bool escape = false;

            while (enemyHealth > 0 && escape == false && hero.health > 0)
            {
                // User Options
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .PageSize(10)
                        .MoreChoicesText("[blue](Move up and down to reveal more choices)[/]")
                        .AddChoices(new[] {
                        "Attack", "Try to Escape", "Use Item", "Check Hero Stats",
                        }));

                if (choice == "Attack")
                {
                    // Recalculated every round so a Strength Potion used mid-fight takes effect immediately
                    HeroManagement.HeroDamageCheck(hero);
                    double heroDamage = hero.currentDamage;
                    double critDamage = hero.currentDamage * hero.criticalDamage;

                    int critRoll = Random.Shared.Next(1, chanceScale + 1);

                    if ((chanceScale - chanceScale * hero.criticalChance) < critRoll)
                    {
                        AnsiConsole.Write(
                            new FigletText("CRITICAL!!")
                            .LeftAligned()
                            .Color(Color.Red));

                        Console.WriteLine();
                        enemyHealth = enemyHealth - (critDamage + heroDamage);
                        Console.WriteLine($"Hero attacks the {enemy.name} ({heroDamage + critDamage} HIT POINTS!)");
                        Console.WriteLine("IT IS A CRITICAL HIT!!!!");
                    }
                    else
                    {
                        AnsiConsole.Write(
                            new FigletText("CHARGE!!")
                            .LeftAligned()
                            .Color(Color.Red));

                        enemyHealth = enemyHealth - heroDamage;
                        Console.WriteLine($"Hero attacks the {enemy.name} ({heroDamage} HIT POINTS!)");
                    }

                    if (enemyHealth > 0)
                    {
                        Console.WriteLine($"The {enemy.name} has {enemyHealth} health now");
                        Console.WriteLine();
                        Console.WriteLine($"The {enemy.name} {enemy.attackText}");

                        // Rolled separately from the Hero's crit roll so the two outcomes are independent
                        int enemyAccuracyRoll = Random.Shared.Next(1, chanceScale + 1);

                        if ((chanceScale - chanceScale * enemy.accuracy) < enemyAccuracyRoll)
                        {
                            hero.health = hero.health - enemy.damage;
                            Console.WriteLine($"You take {enemy.damage} damage which leaves you with {hero.health} health");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("You dodged the attack!");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"The {enemy.name} collapsed!");
                    }
                }
                else if (choice == "Try to Escape")
                {
                    int userEscapeChance = Random.Shared.Next(1, chanceScale + 1);

                    if ((chanceScale - chanceScale * hero.evasiveness) < userEscapeChance)
                    {
                        escape = true;
                    }
                    else
                    {
                        Console.WriteLine("You were not able to escape!");
                        Console.WriteLine();
                        Console.WriteLine($"The {enemy.name} {enemy.escapeAttackText}");
                        hero.health = hero.health - enemy.damage;
                        Console.WriteLine($"You take {enemy.damage} damage");

                        if (hero.health <= 0)
                        {
                            Console.WriteLine($"The {enemy.name} was too much for you.");
                        }
                        else
                        {
                            Console.WriteLine($"You have {hero.health} left");
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
                Console.WriteLine($"You defeated the {enemy.name}!!!");
                HeroManagement.HeroLevelCheck(hero, enemy.experience);
                HeroManagement.HeroDamageCheck(hero);
                HeroManagement.HeroPickupItem(hero);
                Console.WriteLine();
            }
        }
    }
}
