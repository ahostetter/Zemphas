namespace Zemphas.Enemies
{
    internal class Enemy
    {
        public string name;
        public double health;
        public double damage;
        public double accuracy;
        public int experience;

        // Flavour text. Every enemy supplies its own, so the encounter loop never
        // has to know which enemy it is fighting.
        public string introText;          // shown once when the fight starts
        public string attackText;         // follows "The <name> " when it counterattacks
        public string escapeAttackText;   // follows "The <name> " when a escape attempt fails

        public Enemy(string aName, double aHealth, double aDamage, double aAccuracy, int aExperience,
            string aIntroText, string aAttackText, string aEscapeAttackText)
        {
            name = aName;
            health = aHealth;
            damage = aDamage;
            accuracy = aAccuracy;
            experience = aExperience;
            introText = aIntroText;
            attackText = aAttackText;
            escapeAttackText = aEscapeAttackText;
        }
    }
}
