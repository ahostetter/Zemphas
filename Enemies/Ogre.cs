namespace Zemphas.Enemies
{
    internal class Ogre : Enemy
    {
        public Ogre() : base(
            "Ogre",
            Random.Shared.Next(Modifiers.ogreHealthLow(), Modifiers.ogreHealthHigh()),
            Random.Shared.Next(Modifiers.ogreDamageLow(), Modifiers.ogreDamageHigh()),
            Modifiers.ogreAccuracy(),
            Modifiers.ogreExperience(),
            weakness: "Fire",
            resistance: "Ice",
            "You stand before a hulking giant of an Ogre",
            "swings his club at you",
            "swings his club at you")
        {
        }

        // Enrage: once badly wounded the Ogre stops defending itself and swings wild.
        // Punishes a slow grind, and rewards finishing the fight quickly.
        public override SpecialAbilityResult UseSpecial(Random rnd, Hero hero, double healthFraction)
        {
            if (healthFraction >= Modifiers.ogreEnrageThreshold())
            {
                return SpecialAbilityResult.None;
            }

            if (!Combat.Roll(rnd, Modifiers.ogreEnrageChance()))
            {
                return SpecialAbilityResult.None;
            }

            return new SpecialAbilityResult(
                true,
                "ENRAGE",
                "The Ogre roars and swings with everything it has left!",
                damage * Modifiers.ogreEnrageDamage(),
                0);
        }
    }
}
