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

Hero NewHero(double swordDamage)
{
    Inventory inventory = new Inventory(new Sword("Sim Blade", swordDamage, "claymore", "None"), 0, 0, 3);
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

        HeroAttackResult attack = Combat.ResolveHeroAttack(rnd, hero, profile, braceBonus);
        braceBonus = profile.braceBonus;

        if (attack.landed)
        {
            enemyHealth -= attack.damage;
        }

        if (enemyHealth > 0)
        {
            EnemyAttackResult counter = Combat.ResolveEnemyAttack(rnd, hero, enemy, profile);
            if (counter.landed)
            {
                hero.health -= counter.damage;
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

void Scenario(string label, Func<Enemy> makeEnemy, double swordDamage, Func<double, double, bool, AttackProfile> strategy)
{
    int wins = 0;
    double totalRounds = 0;
    double totalHealth = 0;

    for (int i = 0; i < trials; i++)
    {
        Hero hero = NewHero(swordDamage);
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

Console.WriteLine("=== single fight, full health, sword ~400 ===");
Scenario("Ogre    / always Attack", () => new Ogre(), 400, AlwaysAttack);
Scenario("Ogre    / always Heavy Swing", () => new Ogre(), 400, AlwaysHeavy);
Scenario("Ogre    / always Quick Strike", () => new Ogre(), 400, AlwaysQuick);
Scenario("Ogre    / considered play", () => new Ogre(), 400, Considered);
Console.WriteLine();
Scenario("Warlock / always Attack", () => new Warlock(), 400, AlwaysAttack);
Scenario("Warlock / always Heavy Swing", () => new Warlock(), 400, AlwaysHeavy);
Scenario("Warlock / always Quick Strike", () => new Warlock(), 400, AlwaysQuick);
Scenario("Warlock / considered play", () => new Warlock(), 400, Considered);
Console.WriteLine();

Console.WriteLine("=== full run: three encounters, health carries over ===");
void FullRun(string label, Func<double, double, bool, AttackProfile> strategy, bool usePotions)
{
    int survived = 0;
    double totalRounds = 0;

    for (int i = 0; i < trials; i++)
    {
        Hero hero = NewHero(rnd.Next(300, 500));
        bool alive = true;

        for (int encounter = 0; encounter < 3 && alive; encounter++)
        {
            Enemy enemy = (encounter == 2 || rnd.Next(0, 2) == 0) ? new Ogre() : new Warlock();
            var (won, rounds, _) = Fight(hero, enemy, strategy, usePotions);
            totalRounds += rounds;
            alive = won;

            // Mirrors HeroManagement.HeroPickupItem after a victory
            if (alive && hero.inventory.healthPotion + hero.inventory.strengthPotion < hero.inventory.space)
            {
                if (rnd.Next(0, 2) == 0) hero.inventory.healthPotion++;
                else
                {
                    hero.inventory.strengthPotion++;
                    hero.strength += 10;
                }
            }
        }

        if (alive) survived++;
    }

    Console.WriteLine($"  {label,-42} survive {survived * 100.0 / trials,5:F1}%   total rounds {totalRounds / trials,4:F1}");
}

FullRun("always Attack, no potions", AlwaysAttack, false);
FullRun("always Heavy Swing, no potions", AlwaysHeavy, false);
FullRun("always Quick Strike, no potions", AlwaysQuick, false);
FullRun("considered play, no potions", Considered, false);
Console.WriteLine();
FullRun("always Attack + potions", AlwaysAttack, true);
FullRun("considered play + potions  <-- TARGET", Considered, true);

Console.WriteLine();
Console.WriteLine("Target: considered play should clear a run noticeably more often than");
Console.WriteLine("any single-button strategy. If they match, the choices are not yet real.");
