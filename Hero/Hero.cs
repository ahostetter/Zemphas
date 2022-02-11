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
        public double xp;
        public double criticalChance;
        public double criticalDamage;
        public double evasiveness;
        public Inventory inventory;
        public bool alive;

        public Hero(string aName, double aHealth, double aStrength, double aCurrentDamage, double aBaseDamage, double aLevel, double aXP, double aCriticalChance, double aCriticalDamage, double aEvasiveness, Inventory aInventory, bool aAlive)
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
            alive = aAlive;
        }
    }
}
