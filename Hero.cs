using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zemphas
{
    internal class Hero
    {
        public string name;
        public int health;
        public int damage;
        public double criticalChance;
        public double criticalDamage;
        public double evasiveness;

        public Hero(string aName, int aHealth, int aDamage, double aCriticalChance, double aCriticalDamage, double aEvasiveness)
        {
            name = aName;
            health = aHealth;
            damage = aDamage;
            criticalChance = aCriticalChance;
            criticalDamage = aCriticalDamage;
            evasiveness = aEvasiveness;
        }
    }
}
