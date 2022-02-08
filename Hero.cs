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
        public double health;
        public double strength;
        public double currentDamage;
        public double baseDamage;
        public double level;
        public int xp;
        public double criticalChance;
        public double criticalDamage;
        public double evasiveness;
        public Inventory inventory;

        public Hero(string aName, double aHealth, double aStrength, double aCurrentDamage, double aBaseDamage, double aLevel, int aXP, double aCriticalChance, double aCriticalDamage, double aEvasiveness, Inventory aInventory)
        {
            name = aName;
            health = aHealth;
            strength = aStrength;
            currentDamage = aCurrentDamage;
            baseDamage = aBaseDamage;
            level = aLevel;
            xp = aXP;
            criticalChance = aCriticalChance;
            criticalDamage = aCriticalDamage;
            evasiveness = aEvasiveness;
            inventory = aInventory;
        }
    }
}
