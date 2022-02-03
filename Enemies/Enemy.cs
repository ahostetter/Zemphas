using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zemphas.Enemies
{
    internal class Enemy
    {
        public double health;
        public double damage;

        public Enemy(double aHealth, double aDamage)
        {
            health = aHealth;
            damage = aDamage;
        }
    }
}
