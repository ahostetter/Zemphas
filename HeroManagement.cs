using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zemphas
{
    internal class HeroManagement
    {
        public static void HeroLevelCheck(Hero hero, int xp)
        {
            hero.xp = hero.xp + xp;

            if (hero.xp >= 100)
            {
                hero.level = hero.level + 1;
                hero.xp = hero.xp - 100;
            }
        }

        public static void HeroDamageCheck(Hero hero)
        {
            hero.currentDamage = (hero.baseDamage + (hero.level * .1) * hero.baseDamage) + hero.inventory.sword.damage;
        }

        public static void HeroPickupItem(Hero hero)
        {
            Console.WriteLine("You find something.");

            if (Inventory.inventorySpaceCheck(hero.inventory))
            {
                Console.WriteLine("It's a health potion");
                hero.inventory.healthPotion = hero.inventory.healthPotion + 1;
                Console.WriteLine("You now have " + hero.inventory.healthPotion + " Health Potions in your Inventory.");
            }
        }

        public static void HeroUseHealth(Hero hero)
        {
            if (hero.inventory.healthPotion <= 0)
            {
                Console.WriteLine("You can't use a Health Potion because you don't have any!!");
            }
            else
            {
                hero.health = hero.health + 300;
                Console.WriteLine("You now have " + hero.health);
                hero.inventory.healthPotion = hero.inventory.healthPotion - 1;
                Console.WriteLine("You now have " + hero.inventory.healthPotion + " Health Potions in your Inventory.");
            }
        }

        public static void HeroStats(Hero hero)
        {
            Console.WriteLine("|Name:" + hero.name + "|Health:" + hero.health + "|Damage:" + hero.currentDamage + "|Level:" + hero.level + "|XP:" 
                + hero.xp + "|Health Potions:" + hero.inventory.healthPotion + "|");
        }

    }
}
