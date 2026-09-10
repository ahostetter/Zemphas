namespace Zemphas.Enemies
{
    // Cave ambusher. Hits more reliably than a Goblin but is just as brittle.
    internal class GiantSpider : Enemy
    {
        public GiantSpider() : base(
            "Giant Spider",
            Random.Shared.Next(Modifiers.spiderHealthLow(), Modifiers.spiderHealthHigh()),
            Random.Shared.Next(Modifiers.spiderDamageLow(), Modifiers.spiderDamageHigh()),
            Modifiers.spiderAccuracy(),
            Modifiers.spiderExperience(),
            weakness: "Fire",
            resistance: "None",
            "The ceiling moves. A Giant Spider unfolds itself above you",
            "drives its fangs at your shoulder",
            "lunges as you turn to run")
        {
        }
    }
}
