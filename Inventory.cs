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

        public static bool inventorySpaceCheck(Inventory heroInventory)
        {
            int usedSpace = heroInventory.healthPotion;
            bool doYouHaveSpace = true;

            if (heroInventory.space > usedSpace)
            {
                doYouHaveSpace = true;
            }
            else
            {
                Console.WriteLine("You don't have enough room in your inventory. You have to leave the item behind.");
                doYouHaveSpace = false;
            }
            return doYouHaveSpace;
        }
    }
}
