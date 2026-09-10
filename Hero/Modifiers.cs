namespace Zemphas
{
    internal class Modifiers
    {
        public static string heroName()
        {
            string name = "Zemphas";
            return name;
        }

        public static double maxHeroHealth()
        {
            double health = 1500;
            return health;
        }

        public static double heroHealth()
        {
            double health = maxHeroHealth();
            return health;
        }

        public static double heroStrength()
        {
            double strength = 10;
            return strength;
        }

        public static double heroCurrentDamage()
        {
            double currentDamage = 0;
            return currentDamage;
        }

        public static double heroBaseDamage()
        {
            double baseDamage = 100;
            return baseDamage;
        }

        public static double heroStartingLevel()
        {
            double startLevel = 1;
            return startLevel;
        }

        public static double heroXP()
        {
            double heroXP = 0;
            return heroXP;
        }

        public static double heroCritChance()
        {
            double critChance = .2;
            return critChance;
        }

        public static double heroCritDamage()
        {
            double critDamage = 1.0;
            return critDamage;
        }

        public static double heroEvasiveness()
        {
            double evasiveness = .5;
            return evasiveness;
        }

        public static double heroLuck()
        {
            double luck = .5;
            return luck;
        }

        public static bool heroAlive()
        {
            bool alive = true;
            return alive;
        }

        public static double scaleStrength()
        {
            double strengthScale = 3;
            return strengthScale;
        }

        public static double scaleLevel()
        {
            double levelScale = .1;
            return levelScale;
        }

        // Enemy stat ranges live here so difficulty can be tuned in one place
        public static int ogreHealthLow() { return 3000; }
        public static int ogreHealthHigh() { return 4000; }
        public static int ogreDamageLow() { return 210; }
        public static int ogreDamageHigh() { return 300; }

        public static int warlockHealthLow() { return 1700; }
        public static int warlockHealthHigh() { return 2200; }
        public static int warlockDamageLow() { return 265; }
        public static int warlockDamageHigh() { return 395; }

        public static int ogreExperience()
        {
            int ogreExperience = 75;
            return ogreExperience;
        }

        public static double ogreAccuracy()
        {
            double ogreAccuracy = .5;
            return ogreAccuracy;
        }

        public static int warlockExperience()
        {
            int warlockExperience = 100;
            return warlockExperience;
        }

        public static double warlockAccuracy()
        {
            double warlockAccuracy = .8;
            return warlockAccuracy;
        }
        // Fraction of the Hero's evasiveness that applies to dodging blows in combat.
        // Evasiveness is also used at full strength for escaping, so this keeps the
        // in-combat dodge from making the Hero untouchable.
        public static double dodgeScale()
        {
            double dodgeScale = .5;
            return dodgeScale;
        }

        // --- Elements -------------------------------------------------------
        // The Level 1 swords are Fire and Ice, and the two enemies invert each
        // other's weakness, so neither blade is the safe pick.
        public static double elementalWeaknessMultiplier() { return 1.45; }
        public static double elementalResistanceMultiplier() { return 0.82; }

        // --- Enemy signature moves -------------------------------------------
        public static double ogreEnrageThreshold() { return .35; }  // fires below this share of health
        public static double ogreEnrageChance() { return .30; }
        public static double ogreEnrageDamage() { return 1.55; }     // multiple of the Ogre's normal hit

        public static double warlockDrainChance() { return .20; }
        public static double warlockDrainDamage() { return .65; }   // also heals the Warlock this much

        // --- The Warlord (final boss) -----------------------------------------
        public static int warlordHealthLow() { return 3600; }
        public static int warlordHealthHigh() { return 4400; }
        public static int warlordDamageLow() { return 235; }
        public static int warlordDamageHigh() { return 315; }
        public static double warlordAccuracy() { return .60; }
        public static int warlordExperience() { return 250; }
        public static double warlordNovaChance() { return .16; }
        public static double warlordNovaDamage() { return 1.45; }

        // Damage range for the blade hidden down the shimmering path
        public static int treasureSwordLow() { return 380; }
        public static int treasureSwordHigh() { return 560; }

        // --- Level up boons ---------------------------------------------------
        public static double boonMaxHealth() { return 320; }
        public static double boonStrength() { return 9; }
        public static double boonCritChance() { return .07; }

        public static int healthPotionStrength()
        {
            int healthPotionStrength = 850;
            return healthPotionStrength;
        }
    }
}
