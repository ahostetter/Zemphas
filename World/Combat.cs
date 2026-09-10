using Zemphas.Enemies;

namespace Zemphas
{
    // How a fight ended. The game could not previously tell a victory from a
    // flight, which is why running from everything still counted as winning.
    internal enum EncounterOutcome
    {
        Victory,
        Escaped,
        Defeated,
    }

    // How the Hero chose to swing this round. Each option trades damage against
    // how exposed it leaves you, so "Attack" is no longer always the right answer.
    internal sealed class AttackProfile
    {
        public string name;
        public string description;
        public double damageMultiplier;   // scales the Hero's calculated damage
        public double landChance;         // chance the blow connects at all
        public double incomingMultiplier; // scales the counterattack you take this round
        public double braceBonus;         // damage bonus handed to your NEXT attack
        public bool dealsDamage;

        // Parameters are named without the usual "a" prefix so the profile table below
        // can use named arguments and stay readable at seven arguments wide.
        public AttackProfile(string name, string description, double damageMultiplier,
            double landChance, double incomingMultiplier, double braceBonus, bool dealsDamage)
        {
            this.name = name;
            this.description = description;
            this.damageMultiplier = damageMultiplier;
            this.landChance = landChance;
            this.incomingMultiplier = incomingMultiplier;
            this.braceBonus = braceBonus;
            this.dealsDamage = dealsDamage;
        }
    }

    internal readonly struct HeroAttackResult
    {
        public readonly bool landed;
        public readonly bool critical;
        public readonly double damage;
        public readonly double elementalMultiplier; // 1.0 when the element had no bearing

        public HeroAttackResult(bool aLanded, bool aCritical, double aDamage, double aElementalMultiplier)
        {
            landed = aLanded;
            critical = aCritical;
            damage = aDamage;
            elementalMultiplier = aElementalMultiplier;
        }
    }

    // What an enemy's signature move did this round. Returned rather than printed
    // so the balance simulator can run abilities with no console attached.
    internal readonly struct SpecialAbilityResult
    {
        public readonly bool triggered;
        public readonly string name;
        public readonly string description;
        public readonly double damageToHero;
        public readonly double healToEnemy;

        public static readonly SpecialAbilityResult None = new SpecialAbilityResult(false, "", "", 0, 0);

        public SpecialAbilityResult(bool aTriggered, string aName, string aDescription,
            double aDamageToHero, double aHealToEnemy)
        {
            triggered = aTriggered;
            name = aName;
            description = aDescription;
            damageToHero = aDamageToHero;
            healToEnemy = aHealToEnemy;
        }
    }

    internal readonly struct EnemyAttackResult
    {
        public readonly bool landed;   // the enemy's blow connected
        public readonly bool dodged;   // the Hero evaded a blow that would have connected
        public readonly double damage;

        public EnemyAttackResult(bool aLanded, bool aDodged, double aDamage)
        {
            landed = aLanded;
            dodged = aDodged;
            damage = aDamage;
        }
    }

    // Pure combat maths with no console output, so the balance simulator in
    // Tools/BalanceSim can drive exactly the same code the real game runs.
    internal static class Combat
    {
        public const int chanceScale = 10;

        // The Hero's swing options, in the order they appear in the combat menu
        public static readonly AttackProfile[] attackProfiles =
        {
            new AttackProfile("Attack", "A measured swing. Reliable, no surprises.",
                damageMultiplier: 1.0, landChance: 0.90, incomingMultiplier: 1.0, braceBonus: 0.0, dealsDamage: true),

            new AttackProfile("Quick Strike", "Always connects, but it is a glancing blow.",
                damageMultiplier: 0.55, landChance: 1.00, incomingMultiplier: 0.80, braceBonus: 0.0, dealsDamage: true),

            new AttackProfile("Heavy Swing", "Devastating when it lands. It often does not.",
                damageMultiplier: 2.10, landChance: 0.55, incomingMultiplier: 1.30, braceBonus: 0.0, dealsDamage: true),

            new AttackProfile("Defend", "Absorb the blow and put your weight into the next one.",
                damageMultiplier: 0.0, landChance: 0.0, incomingMultiplier: 0.20, braceBonus: 1.00, dealsDamage: false),
        };

        // A roll of 1..chanceScale succeeds with probability `chance`.
        // Written this way to match the threshold comparison the game has always used.
        public static bool Roll(Random rnd, double chance)
        {
            return (chanceScale - chanceScale * chance) < rnd.Next(1, chanceScale + 1);
        }

        // Single source of truth for Hero damage; HeroManagement.HeroDamageCheck defers to this.
        public static double CalculateHeroDamage(Hero hero)
        {
            return (hero.baseDamage + (hero.level * Modifiers.scaleLevel()) * hero.baseDamage)
                + hero.inventory.sword.damage
                + (hero.strength * Modifiers.scaleStrength());
        }

        // How well the Hero's blade suits this enemy. The Level 1 swords are Fire and
        // Ice, and the two enemies invert each other, so neither sword is the safe pick.
        public static double ElementalMultiplier(string swordElement, Enemy enemy)
        {
            if (string.IsNullOrEmpty(swordElement) || swordElement == "None")
            {
                return 1.0;
            }

            if (swordElement == enemy.weakness)
            {
                return Modifiers.elementalWeaknessMultiplier();
            }

            if (swordElement == enemy.resistance)
            {
                return Modifiers.elementalResistanceMultiplier();
            }

            return 1.0;
        }

        public static HeroAttackResult ResolveHeroAttack(Random rnd, Hero hero, Enemy enemy, AttackProfile profile, double braceBonus)
        {
            if (!profile.dealsDamage)
            {
                return new HeroAttackResult(false, false, 0, 1.0);
            }

            if (!Roll(rnd, profile.landChance))
            {
                return new HeroAttackResult(false, false, 0, 1.0);
            }

            double elemental = ElementalMultiplier(hero.inventory.sword.element, enemy);
            double damage = CalculateHeroDamage(hero) * profile.damageMultiplier * (1.0 + braceBonus) * elemental;
            bool critical = Roll(rnd, hero.criticalChance);

            if (critical)
            {
                damage = damage + damage * hero.criticalDamage;
            }

            return new HeroAttackResult(true, critical, damage, elemental);
        }

        // Adds experience and reports how many levels it bought. Loops, because a
        // single large award used to grant only one level no matter its size.
        public static int ApplyExperience(Hero hero, int xp)
        {
            hero.xp = hero.xp + xp;
            int levelsGained = 0;

            while (hero.xp >= 100)
            {
                hero.level = hero.level + 1;
                hero.xp = hero.xp - 100;
                levelsGained++;
            }

            return levelsGained;
        }

        public static EnemyAttackResult ResolveEnemyAttack(Random rnd, Hero hero, Enemy enemy, AttackProfile profile)
        {
            // Rolled independently of the Hero's attack roll so the outcomes stay uncorrelated
            if (!Roll(rnd, enemy.accuracy))
            {
                return new EnemyAttackResult(false, false, 0);
            }

            // The Hero's evasiveness now matters inside a fight, not only when escaping
            if (Roll(rnd, hero.evasiveness * Modifiers.dodgeScale()))
            {
                return new EnemyAttackResult(false, true, 0);
            }

            return new EnemyAttackResult(true, false, enemy.damage * profile.incomingMultiplier);
        }
    }
}
