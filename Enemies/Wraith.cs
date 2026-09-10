namespace Zemphas.Enemies
{
    // Highland horror. Thin on health but hits harder than anything short of the
    // Warlord, and its accuracy makes guarding the only reliable answer.
    internal class Wraith : Enemy
    {
        public Wraith() : base(
            "Wraith",
            Random.Shared.Next(Modifiers.wraithHealthLow(), Modifiers.wraithHealthHigh()),
            Random.Shared.Next(Modifiers.wraithDamageLow(), Modifiers.wraithDamageHigh()),
            Modifiers.wraithAccuracy(),
            Modifiers.wraithExperience(),
            weakness: "Ice",
            resistance: "Fire",
            "The cold arrives before the shape does. A Wraith pours out of the dark",
            "reaches through your guard with one grey hand",
            "howls, and the sound goes straight through you")
        {
        }

        public override SpecialAbilityResult UseSpecial(Random rnd, Hero hero, double healthFraction)
        {
            if (!Combat.Roll(rnd, Modifiers.wraithChillChance()))
            {
                return SpecialAbilityResult.None;
            }

            return new SpecialAbilityResult(
                true,
                "SOUL CHILL",
                "The Wraith closes its hand around something inside your chest!",
                damage * Modifiers.wraithChillDamage(),
                0);
        }
    }
}
