namespace Zemphas
{
    internal class Inventory
    {
        public Sword sword;
        public int healthPotion;
        public int strengthPotion;
        public int space;

        public Inventory(Sword aSword, int aHealthPotion, int aStrengthPotion, int aSpace)
        {
            sword = aSword;
            healthPotion = aHealthPotion;
            strengthPotion = aStrengthPotion;
            space = aSpace;
        }

        public static bool inventorySpaceCheck(Inventory heroInventory)
        {
            int usedSpace = heroInventory.healthPotion + heroInventory.strengthPotion;
            bool doYouHaveSpace;

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
