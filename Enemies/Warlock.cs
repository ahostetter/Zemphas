using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zemphas.Enemies
{
    internal class Warlock : Enemy
    {
        public Warlock(double aHealth, double aDamage) : base(aHealth, aDamage)
        {
            health = aHealth;
            damage = aDamage;
        }
    }
}
