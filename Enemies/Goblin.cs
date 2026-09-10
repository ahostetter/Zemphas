namespace Zemphas.Enemies
{
    // Cave vermin. Weak, quick, and mostly there to teach the combat menu before
    // anything dangerous turns up.
    internal class Goblin : Enemy
    {
        public Goblin() : base(
            "Goblin",
            Random.Shared.Next(Modifiers.goblinHealthLow(), Modifiers.goblinHealthHigh()),
            Random.Shared.Next(Modifiers.goblinDamageLow(), Modifiers.goblinDamageHigh()),
            Modifiers.goblinAccuracy(),
            Modifiers.goblinExperience(),
            weakness: "Ice",
            resistance: "None",
            "A Goblin drops from a ledge, all teeth and bad intentions",
            "jabs at you with a rusted spear",
            "flings a rock at your back")
        {
        }
    }
}
