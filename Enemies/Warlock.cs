namespace Zemphas.Enemies
{
    internal class Warlock : Enemy
    {
        public Warlock() : base(
            "Warlock",
            Random.Shared.Next(800, 1000),
            Random.Shared.Next(500, 800),
            Modifiers.warlockAccuracy(),
            Modifiers.warlockExperience(),
            "You see the red crazy eyes of a Warlock as it conjures a spell meant for you",
            "sends a bolt of lightning your way",
            "sends a cone of frozen air your way")
        {
        }
    }
}
