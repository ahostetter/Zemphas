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
            "You see the red crazy eyes of a Warlock as it conjures a spell meant for you",
            "sends a bolt of lightning your way",
            "sends a cone of frozen air your way")
        {
        }
    }
}
