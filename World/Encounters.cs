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
        public static EncounterOutcome randomEcounter(Hero encounterHero)
        {
            Enemy enemy = randomEnemies[Random.Shared.Next(randomEnemies.Length)]();
            return Encounter(encounterHero, enemy);
        }

        // Level 2 opens with a specific Ogre rather than a random enemy
        public static EncounterOutcome OgreEncounter(Hero hero)
        {
            return Encounter(hero, new Ogre());
        }

        // One combat loop for every enemy. All the differences between enemies live
        // on the Enemy itself, so this method never branches on which one it is.
        // offerBoons is false for the final fight, where the run ends immediately
        // afterwards and a growth prompt would be a choice with nothing to spend it on.
        public static EncounterOutcome Encounter(Hero hero, Enemy enemy, bool offerBoons = true)
        {
            AnsiConsole.Write(new Markup($"[blue]{Markup.Escape(enemy.introText)}[/]"));
            Console.WriteLine();
            Console.WriteLine();

            double enemyHealth = enemy.health;
            bool escape = false;

            // Carried by Defend into the following round
            double braceBonus = 0;

            while (enemyHealth > 0 && escape == false && hero.health > 0)
            {
                // Swing options first, then the utility choices
                List<string> options = new List<string>();
                foreach (AttackProfile p in Combat.attackProfiles)
                {
                    options.Add(p.name);
                }
                options.Add("Try to Escape");
                options.Add("Use Item");
                options.Add("Check Hero Stats");

                if (braceBonus > 0)
                {
                    AnsiConsole.Write(new Markup($"[green]You are braced: your next attack deals +{braceBonus * 100:F0}% damage.[/]"));
                    Console.WriteLine();
                }

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .PageSize(10)
                        .MoreChoicesText("[blue](Move up and down to reveal more choices)[/]")
                        .AddChoices(options));

                AttackProfile? chosenProfile = null;
                foreach (AttackProfile p in Combat.attackProfiles)
                {
                    if (p.name == choice)
                    {
                        chosenProfile = p;
                    }
                }

                if (chosenProfile != null)
                {
                    // Recalculated every round so a Strength Potion used mid-fight takes effect immediately
                    HeroManagement.HeroDamageCheck(hero);

                    HeroAttackResult attack = Combat.ResolveHeroAttack(Random.Shared, hero, enemy, chosenProfile, braceBonus);
                    braceBonus = chosenProfile.braceBonus;

                    if (!chosenProfile.dealsDamage)
                    {
                        AnsiConsole.Write(new Markup("[green]You raise your guard and brace for the blow.[/]"));
                        Console.WriteLine();
                    }
                    else if (attack.landed)
                    {
                        AnsiConsole.Write(
                            new FigletText(attack.critical ? "CRITICAL!!" : "CHARGE!!")
                            .LeftAligned()
                            .Color(Color.Red));

                        Console.WriteLine();
                        enemyHealth = enemyHealth - attack.damage;
                        Console.WriteLine($"Hero attacks the {enemy.name} ({attack.damage:F0} HIT POINTS!)");

                        if (attack.critical)
                        {
                            Console.WriteLine("IT IS A CRITICAL HIT!!!!");
                        }

                        if (attack.elementalMultiplier > 1.0)
                        {
                            AnsiConsole.Write(new Markup($"[orange1]The {Markup.Escape(hero.inventory.sword.element)} blade sears the {Markup.Escape(enemy.name)} — it is weak to it![/]"));
                            Console.WriteLine();
                        }
                        else if (attack.elementalMultiplier < 1.0)
                        {
                            AnsiConsole.Write(new Markup($"[grey]The {Markup.Escape(enemy.name)} shrugs off your {Markup.Escape(hero.inventory.sword.element)} blade.[/]"));
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        AnsiConsole.Write(new Markup($"[yellow]Your {Markup.Escape(chosenProfile.name)} goes wide and misses![/]"));
                        Console.WriteLine();
                    }

                    if (enemyHealth > 0)
                    {
                        if (chosenProfile.dealsDamage && attack.landed)
                        {
                            Console.WriteLine($"The {enemy.name} has {enemyHealth:F0} health now");
                        }
                        Console.WriteLine();

                        SpecialAbilityResult special = enemy.UseSpecial(Random.Shared, hero, enemyHealth / enemy.health);

                        if (special.triggered)
                        {
                            AnsiConsole.Write(
                                new FigletText(special.name)
                                .LeftAligned()
                                .Color(Color.Purple));

                            AnsiConsole.Write(new Markup($"[purple]{Markup.Escape(special.description)}[/]"));
                            Console.WriteLine();

                            // A signature move cannot be dodged, but guarding still blunts it,
                            // which is what makes Defend worth reaching for.
                            double specialDamage = special.damageToHero * chosenProfile.incomingMultiplier;
                            hero.health = hero.health - specialDamage;
                            Console.WriteLine($"You take {specialDamage:F0} damage which leaves you with {hero.health:F0} health");

                            if (special.healToEnemy > 0)
                            {
                                enemyHealth = enemyHealth + special.healToEnemy;
                                Console.WriteLine($"The {enemy.name} heals itself to {enemyHealth:F0} health!");
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine($"The {enemy.name} {enemy.attackText}");

                            EnemyAttackResult counter = Combat.ResolveEnemyAttack(Random.Shared, hero, enemy, chosenProfile);

                            if (counter.landed)
                            {
                                hero.health = hero.health - counter.damage;
                                Console.WriteLine($"You take {counter.damage:F0} damage which leaves you with {hero.health:F0} health");
                                Console.WriteLine();
                            }
                            else if (counter.dodged)
                            {
                                Console.WriteLine("You slip aside and dodge the attack!");
                            }
                            else
                            {
                                Console.WriteLine($"The {enemy.name} misses!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"The {enemy.name} collapsed!");
                    }
                }
                else if (choice == "Try to Escape")
                {
                    if (Combat.Roll(Random.Shared, hero.evasiveness))
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
                return EncounterOutcome.Escaped;
            }

            if (hero.health <= 0)
            {
                Console.WriteLine("You perished");
                hero.alive = false;
                return EncounterOutcome.Defeated;
            }

            Console.WriteLine($"You defeated the {enemy.name}!!!");
            HeroManagement.HeroLevelCheck(hero, enemy.experience, offerBoons);
            HeroManagement.HeroDamageCheck(hero);
            HeroManagement.HeroPickupItem(hero);
            Console.WriteLine();
            return EncounterOutcome.Victory;
        }
    }
}
