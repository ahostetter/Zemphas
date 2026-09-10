namespace Zemphas.Enemies
{
    // The castle's master, and the only thing standing between the Hero and the
    // end of the game. Deliberately weak to Ice and resistant to Fire: the rest of
    // the run leans Ogre-heavy, which favours a Fire blade, so the final fight
    // pulls the other way and keeps the Level 1 sword choice honest.
    internal class Warlord : Enemy
    {
        public Warlord() : base(
            "Warlord",
            Random.Shared.Next(Modifiers.warlordHealthLow(), Modifiers.warlordHealthHigh()),
            Random.Shared.Next(Modifiers.warlordDamageLow(), Modifiers.warlordDamageHigh()),
            Modifiers.warlordAccuracy(),
            Modifiers.warlordExperience(),
            weakness: "Ice",
            resistance: "Fire",
            "The Warlord rises from a throne of blackened iron, and the hall goes cold",
            "brings a greatsword down on you",
            "hurls you back with a gauntleted fist")
        {
        }

        // Shadow Nova: a wave of darkness that cannot be dodged, only weathered.
        // Unlike the Ogre's Enrage this can fire at any point, so the fight never
        // settles into a rhythm.
        public override SpecialAbilityResult UseSpecial(Random rnd, Hero hero, double healthFraction)
        {
            if (!Combat.Roll(rnd, Modifiers.warlordNovaChance()))
            {
                return SpecialAbilityResult.None;
            }

            return new SpecialAbilityResult(
                true,
                "SHADOW NOVA",
                "The Warlord tears open the dark and lets it wash over the hall!",
                damage * Modifiers.warlordNovaDamage(),
                0);
        }
    }
}
