namespace Zemphas.Enemies
{
    internal class Ogre : Enemy
    {
        public Ogre() : base(
            "Ogre",
            Random.Shared.Next(1500, 2000),
            Random.Shared.Next(200, 300),
            Modifiers.ogreAccuracy(),
            Modifiers.ogreExperience(),
            "You stand before a hulking giant of an Ogre",
            "swings his club at you",
            "swings his club at you")
        {
        }
    }
}
