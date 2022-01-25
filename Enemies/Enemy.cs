using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zemphas.Enemies
{
    internal class Enemy
    {
        public int health;
        public int damage;

        public Enemy(int aHealth, int aDamage)
        {
            health = aHealth;
            damage = aDamage;
        }
    }
}
