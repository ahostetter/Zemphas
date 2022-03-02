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
        public double accuracy;

        public Enemy(double aHealth, double aDamage, double aAccuracy)
        {
            health = aHealth;
            damage = aDamage;
            accuracy = aAccuracy;
        }
    }
}
