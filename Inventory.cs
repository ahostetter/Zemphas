using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zemphas
{
    internal class Inventory
    {
        public Sword sword;
        public int healthPotion;
        public int space;

        public Inventory(Sword aSword, int aHealthPotion, int aSpace)
        {
            sword = aSword;
            healthPotion = aHealthPotion;
            space = aSpace;
        }
    }
}
