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
        public int space;

        public Inventory(Sword aSword, int aSpace)
        {
            sword = aSword;
            space = aSpace;
        }
    }
}
