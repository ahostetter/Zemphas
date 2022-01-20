using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zemphas
{
    internal class Sword
    {
        public string name;
        public int damage;
        public string type;
        public string element;

        public Sword(string aName, int aDamage, string aType, string aElement)
        {
            name = aName;
            damage = aDamage;
            type = aType;
            element = aElement;
        }
    }
}
