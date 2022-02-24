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
            double critChance = .5;
            return critChance;
        }

        public static double heroCritDamage()
        {
            double critDamage = .2;
            return critDamage;
        }

        public static double heroEvasiveness()
        {
            double evasiveness = .0;
            return evasiveness;
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

        public static int OgreExperience()
        {
            int ogreExperience = 75;
            return ogreExperience;
        }

        public static int WarlockExperience()
        {
            int warlockExperience = 100;
            return warlockExperience;
        }
    }
}
