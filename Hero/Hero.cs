namespace Zemphas
{
    internal class Hero
    {
        public string name;
        public double maxHealth;
        public double health;
        public double strength;
        public double currentDamage;
        public double baseDamage;
        public double level;
        public double xp;
        public double criticalChance;
        public double criticalDamage;
        public double evasiveness;
        public double luck;
        public Inventory inventory;
        public bool alive;

        public Hero(string aName, double aMaxHealth, double aHealth, double aStrength, double aCurrentDamage, double aBaseDamage, double aLevel, double aXP, double aCriticalChance, double aCriticalDamage, double aEvasiveness, double aLuck, Inventory aInventory, bool aAlive)
        {
            name = aName;
            maxHealth = aMaxHealth;
            health = aHealth;
            strength = aStrength;
            currentDamage = aCurrentDamage;
            baseDamage = aBaseDamage;
            level = aLevel;
            xp = aXP;
            criticalChance = aCriticalChance;
            criticalDamage = aCriticalDamage;
            evasiveness = aEvasiveness;
            luck = aLuck;
            inventory = aInventory;
            alive = aAlive;
        }
    }
}
