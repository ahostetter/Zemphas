using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zemphas.Enemies
{
    internal class Ogre : Enemy
    {

        public Ogre(int aHealth, int aDamage) : base(aHealth, aDamage)
        {
            health = aHealth;
            damage = aDamage;
        }

    }
}
