using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zemphas
{
    internal class Modifiers
    {
        public static string heroName()
        {
            string name = "Zemphas";
            return name;
        }

        public static double heroHealth()
        {
            double health = 2000;
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
            double baseDamage = 200;
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
            double critChance = 1;
            return critChance;
        }

        public static double heroCritDamage()
        {
            double critDamage = .5;
            return critDamage;
        }

        public static double heroEvasiveness()
        {
            double evasiveness = .5;
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
    }
}
