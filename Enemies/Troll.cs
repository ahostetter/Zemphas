namespace Zemphas.Enemies
{
    // Highland bruiser. Enormous, slow, and it knits itself back together, so a
    // cautious grind loses to it outright.
    internal class Troll : Enemy
    {
        public Troll() : base(
            "Troll",
            Random.Shared.Next(Modifiers.trollHealthLow(), Modifiers.trollHealthHigh()),
            Random.Shared.Next(Modifiers.trollDamageLow(), Modifiers.trollDamageHigh()),
            Modifiers.trollAccuracy(),
            Modifiers.trollExperience(),
            weakness: "Fire",
            resistance: "Ice",
            "A Troll hauls itself upright, and the wounds you have not given it yet are already closing",
            "brings both fists down on you",
            "swipes at you as you back away")
        {
        }

        // Regenerate: heals itself and does no damage. Fighting defensively against
        // a Troll gives it the time it needs, so this one wants ending fast.
        public override SpecialAbilityResult UseSpecial(Random rnd, Hero hero, double healthFraction)
        {
            if (!Combat.Roll(rnd, Modifiers.trollRegenerateChance()))
            {
                return SpecialAbilityResult.None;
            }

            return new SpecialAbilityResult(
                true,
                "REGENERATE",
                "The Troll's wounds close over as you watch.",
                0,
                health * Modifiers.trollRegenerateAmount());
        }
    }
}
