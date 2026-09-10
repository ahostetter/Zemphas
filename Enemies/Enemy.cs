namespace Zemphas.Enemies
{
    internal class Enemy
    {
        public string name;
        public double health;
        public double damage;
        public double accuracy;
        public int experience;

        // Elements the Hero's blade can exploit or waste itself against.
        // "None" on either side means the strike lands for normal damage.
        public string weakness;
        public string resistance;

        // Flavour text. Every enemy supplies its own, so the encounter loop never
        // has to know which enemy it is fighting.
        public string introText;          // shown once when the fight starts
        public string attackText;         // follows "The <name> " when it counterattacks
        public string escapeAttackText;   // follows "The <name> " when a escape attempt fails

        // Parameters are named without the usual "a" prefix so subclasses can use
        // named arguments and stay readable at ten arguments wide.
        public Enemy(string name, double health, double damage, double accuracy, int experience,
            string weakness, string resistance,
            string introText, string attackText, string escapeAttackText)
        {
            this.name = name;
            this.health = health;
            this.damage = damage;
            this.accuracy = accuracy;
            this.experience = experience;
            this.weakness = weakness;
            this.resistance = resistance;
            this.introText = introText;
            this.attackText = attackText;
            this.escapeAttackText = escapeAttackText;
        }

        // Each enemy gets one signature move. The base enemy has none; subclasses
        // override this. It returns a result rather than printing, so the balance
        // simulator can run abilities headlessly.
        //
        // healthFraction lets an ability care about how hurt the enemy is.
        public virtual SpecialAbilityResult UseSpecial(Random rnd, Hero hero, double healthFraction)
        {
            return SpecialAbilityResult.None;
        }
    }
}
