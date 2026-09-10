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
            "You stand before a hulking giant of an Ogre",
            "swings his club at you",
            "swings his club at you")
        {
        }
    }
}
