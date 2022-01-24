using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zemphas
{
    internal class Warlock : Enemy
    {

        Random random = new Random();

        public Warlock(int aHealth, int aDamage) : base(aHealth, aDamage)
        {
            health = aHealth;
            damage = aDamage;
        }
    }
}
