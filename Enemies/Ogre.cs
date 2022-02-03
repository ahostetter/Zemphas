using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zemphas.Enemies
{
    internal class Ogre : Enemy
    {

        public Ogre(double aHealth, double aDamage) : base(aHealth, aDamage)
        {
            health = aHealth;
            damage = aDamage;
        }

    }
}
