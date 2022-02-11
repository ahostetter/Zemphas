namespace Zemphas
{
    internal class Sword
    {
        public string name;
        public double damage;
        public string type;
        public string element;

        public Sword(string aName, double aDamage, string aType, string aElement)
        {
            name = aName;
            damage = aDamage;
            type = aType;
            element = aElement;
        }
    }
}
