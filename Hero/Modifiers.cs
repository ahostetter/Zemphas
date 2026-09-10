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
        public static int ogreDamageLow() { return 240; }
        public static int ogreDamageHigh() { return 340; }

        public static int warlockHealthLow() { return 1700; }
        public static int warlockHealthHigh() { return 2200; }
        public static int warlockDamageLow() { return 300; }
        public static int warlockDamageHigh() { return 460; }

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

        public static int healthPotionStrength()
        {
            int healthPotionStrength = 700;
            return healthPotionStrength;
        }
    }
}
