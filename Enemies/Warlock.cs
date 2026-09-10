namespace Zemphas.Enemies
{
    internal class Warlock : Enemy
    {
        public Warlock() : base(
            "Warlock",
            Random.Shared.Next(Modifiers.warlockHealthLow(), Modifiers.warlockHealthHigh()),
            Random.Shared.Next(Modifiers.warlockDamageLow(), Modifiers.warlockDamageHigh()),
            Modifiers.warlockAccuracy(),
            Modifiers.warlockExperience(),
            weakness: "Ice",
            resistance: "Fire",
            "You see the red crazy eyes of a Warlock as it conjures a spell meant for you",
            "sends a bolt of lightning your way",
            "sends a cone of frozen air your way")
        {
        }

        // Life Drain: siphons the Hero's health into its own. Turns a long fight
        // against a Warlock into a losing race, so it wants closing down fast.
        public override SpecialAbilityResult UseSpecial(Random rnd, Hero hero, double healthFraction)
        {
            if (!Combat.Roll(rnd, Modifiers.warlockDrainChance()))
            {
                return SpecialAbilityResult.None;
            }

            double drained = damage * Modifiers.warlockDrainDamage();

            return new SpecialAbilityResult(
                true,
                "LIFE DRAIN",
                "The Warlock rips the life out of you and pulls it into itself!",
                drained,
                drained);
        }
    }
}
