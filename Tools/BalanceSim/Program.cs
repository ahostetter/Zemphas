using Zemphas;
using Zemphas.Enemies;

// Headless balance simulator. Drives the real Combat resolver from the game, so
// the numbers reported here are the numbers the game actually plays with.
//
//   dotnet run --project Tools/BalanceSim
//   dotnet run --project Tools/BalanceSim -- 50000

int trials = args.Length > 0 && int.TryParse(args[0], out int parsed) ? parsed : 20000;
Random rnd = new Random(20260909);

Console.WriteLine($"Zemphas balance simulation — {trials:N0} trials per scenario");
Console.WriteLine();

Hero NewHero(double swordDamage, string element = "None")
{
    Inventory inventory = new Inventory(new Sword("Sim Blade", swordDamage, "claymore", element), 0, 0, 6);
    return new Hero(Modifiers.heroName(), Modifiers.maxHeroHealth(), Modifiers.heroHealth(),
        Modifiers.heroStrength(), Modifiers.heroCurrentDamage(), Modifiers.heroBaseDamage(),
        Modifiers.heroStartingLevel(), Modifiers.heroXP(), Modifiers.heroCritChance(),
        Modifiers.heroCritDamage(), Modifiers.heroEvasiveness(), Modifiers.heroLuck(),
        inventory, Modifiers.heroAlive());
}

// Mirrors the round structure of Encounters.Encounter, minus the console output
(bool won, int rounds, double healthLeft) Fight(Hero hero, Enemy enemy, Func<double, double, bool, AttackProfile> pickAttack, bool usePotions = false)
{
    double enemyHealth = enemy.health;
    double braceBonus = 0;
    int rounds = 0;

    while (enemyHealth > 0 && hero.health > 0 && rounds < 500)
    {
        rounds++;

        // Mirrors HeroManagement.HeroUseItem: a careful player drinks when badly hurt
        if (usePotions && hero.health / hero.maxHealth < 0.35 && hero.inventory.healthPotion > 0)
        {
            hero.health = (hero.health + Modifiers.healthPotionStrength()) > hero.maxHealth
                ? hero.maxHealth
                : hero.health + Modifiers.healthPotionStrength();
            hero.inventory.healthPotion--;
            continue;
        }
        AttackProfile profile = pickAttack(hero.health / hero.maxHealth, enemyHealth / enemy.health, braceBonus > 0);

        HeroAttackResult attack = Combat.ResolveHeroAttack(rnd, hero, enemy, profile, braceBonus);
        braceBonus = profile.braceBonus;

        if (attack.landed)
        {
            enemyHealth -= attack.damage;
        }

        if (enemyHealth > 0)
        {
            SpecialAbilityResult special = enemy.UseSpecial(rnd, hero, enemyHealth / enemy.health);

            if (special.triggered)
            {
                hero.health -= special.damageToHero * profile.incomingMultiplier;
                enemyHealth += special.healToEnemy;
            }
            else
            {
                EnemyAttackResult counter = Combat.ResolveEnemyAttack(rnd, hero, enemy, profile);
                if (counter.landed)
                {
                    hero.health -= counter.damage;
                }
            }
        }
    }

    return (hero.health > 0, rounds, hero.health);
}

// ---- strategies a player might actually use -------------------------------
AttackProfile Named(string name) => Combat.attackProfiles.First(p => p.name == name);

AttackProfile AlwaysAttack(double h, double e, bool braced) => Named("Attack");
AttackProfile AlwaysHeavy(double h, double e, bool braced) => Named("Heavy Swing");
AttackProfile AlwaysQuick(double h, double e, bool braced) => Named("Quick Strike");

// Defend when badly hurt, swing heavy when healthy, otherwise a measured attack
AttackProfile Considered(double healthFraction, double enemyFraction, bool braced)
{
    // Cash in a brace with the biggest swing available
    if (braced) return Named("Heavy Swing");
    // Badly hurt: soak the next hit and set up a counter
    if (healthFraction < 0.35) return Named("Defend");
    // Enemy nearly down: take the guaranteed hit rather than risk a miss
    if (enemyFraction < 0.20) return Named("Quick Strike");
    // Healthy: gamble on the big swing
    if (healthFraction > 0.65) return Named("Heavy Swing");
    return Named("Attack");
}

void Scenario(string label, Func<Enemy> makeEnemy, double swordDamage, Func<double, double, bool, AttackProfile> strategy, string element = "None")
{
    int wins = 0;
    double totalRounds = 0;
    double totalHealth = 0;

    for (int i = 0; i < trials; i++)
    {
        Hero hero = NewHero(swordDamage, element);
        var (won, rounds, healthLeft) = Fight(hero, makeEnemy(), strategy);
        totalRounds += rounds;
        if (won)
        {
            wins++;
            totalHealth += healthLeft;
        }
    }

    double winRate = wins * 100.0 / trials;
    Console.WriteLine($"  {label,-42} win {winRate,5:F1}%   rounds {totalRounds / trials,4:F1}   HP left {(wins > 0 ? totalHealth / wins : 0),6:F0}");
}

Console.WriteLine("=== element matrix: considered play, sword ~400 ===");
foreach (string element in new[] { "None", "Fire", "Ice" })
{
    Scenario($"Ogre    vs {element,-5} blade", () => new Ogre(), 400, Considered, element);
}
Console.WriteLine();
foreach (string element in new[] { "None", "Fire", "Ice" })
{
    Scenario($"Warlock vs {element,-5} blade", () => new Warlock(), 400, Considered, element);
}
Console.WriteLine();

Console.WriteLine("=== full run through the expanded world ===");

// Mirrors Content/world.json. A step is an encounter table, a named enemy, or "rest".
string[] longPath  = { "cave", "deepcave", "deepcave", "Ogre", "forest", "rest", "highlands", "highlands", "rest", "castle", "castle", "Warlord" };
string[] shortPath = { "cave", "rest", "rest", "highlands", "rest", "castle", "castle", "Warlord" };

Dictionary<string, string[]> tables = new Dictionary<string, string[]>
{
    ["cave"] = new[] { "Goblin", "GiantSpider", "Goblin" },
    ["deepcave"] = new[] { "GiantSpider", "Ogre", "Goblin" },
    ["forest"] = new[] { "Ogre", "Warlock" },
    ["highlands"] = new[] { "Troll", "Wraith", "Troll", "Warlock" },
    ["castle"] = new[] { "Wraith", "Troll" },
};

Enemy Spawn(string name)
{
    switch (name)
    {
        case "Goblin": return new Goblin();
        case "GiantSpider": return new GiantSpider();
        case "Ogre": return new Ogre();
        case "Warlock": return new Warlock();
        case "Troll": return new Troll();
        case "Wraith": return new Wraith();
        case "Warlord": return new Warlord();
        default: throw new Exception("unknown enemy " + name);
    }
}

void FullRun(string label, Func<double, double, bool, AttackProfile> strategy, bool usePotions,
    string element, string[] path)
{
    int survived = 0;
    double totalRounds = 0;
    double totalFights = 0;

    for (int i = 0; i < trials; i++)
    {
        Hero hero = NewHero(rnd.Next(300, 500), element);
        bool alive = true;

        foreach (string step in path)
        {
            if (!alive) break;

            if (step == "rest")
            {
                hero.health = Math.Min(hero.maxHealth, hero.health + hero.maxHealth * Modifiers.restHealAmount());
                continue;
            }

            string enemyName = tables.ContainsKey(step)
                ? tables[step][rnd.Next(tables[step].Length)]
                : step;

            Enemy enemy = Spawn(enemyName);
            var (won, rounds, _) = Fight(hero, enemy, strategy, usePotions);
            totalRounds += rounds;
            totalFights++;
            alive = won;

            if (!alive) break;

            // Levelling through the real code path; the boss grants no usable boon
            int levels = Combat.ApplyExperience(hero, enemy.experience);
            for (int l = 0; l < levels; l++)
            {
                if (hero.health / hero.maxHealth < 0.55)
                {
                    hero.maxHealth += Modifiers.boonMaxHealth();
                    hero.health = Math.Min(hero.maxHealth, hero.health + Modifiers.boonMaxHealth());
                }
                else
                {
                    hero.strength += Modifiers.boonStrength();
                }
            }

            // Mirrors HeroManagement.HeroPickupItem
            if (hero.inventory.healthPotion + hero.inventory.strengthPotion < hero.inventory.space)
            {
                if (rnd.Next(0, 2) == 0) hero.inventory.healthPotion++;
                else { hero.inventory.strengthPotion++; hero.strength += 10; }
            }
        }

        if (alive) survived++;
    }

    Console.WriteLine($"  {label,-44} survive {survived * 100.0 / trials,5:F1}%   fights {totalFights / trials,4:F1}   rounds {totalRounds / trials,5:F1}");
}

FullRun("long path, considered + potions, Fire", Considered, true, "Fire", longPath);
FullRun("long path, considered + potions, Ice", Considered, true, "Ice", longPath);
FullRun("long path, always Attack + potions", AlwaysAttack, true, "Fire", longPath);
FullRun("long path, always Heavy Swing + potions", AlwaysHeavy, true, "Fire", longPath);
FullRun("long path, always Quick Strike + potions", AlwaysQuick, true, "Fire", longPath);
FullRun("long path, considered, no potions", Considered, false, "Fire", longPath);
FullRun("long path, always Attack, no potions", AlwaysAttack, false, "Fire", longPath);
Console.WriteLine();
FullRun("short path, considered + potions, Fire", Considered, true, "Fire", shortPath);
FullRun("short path, considered + potions, Ice", Considered, true, "Ice", shortPath);
