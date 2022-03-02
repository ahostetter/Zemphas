using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zemphas.Enemies
{
    internal class Ogre : Enemy
    {

        public Ogre(double aHealth, double aDamage, double aAccuracy) : base(aHealth, aDamage, aAccuracy)
        {
            health = aHealth;
            damage = aDamage;
            accuracy = aAccuracy;
        }

    }
}
