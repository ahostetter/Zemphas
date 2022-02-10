using Zemphas;

Inventory zemphasInventory = new Inventory(new Sword("Dull Blade", 20, "dagger", "None"), 0, 0, 3);

Hero zemphas = new Hero("Zemphas", 2000, 10, 0, 200, 1, 0, .1, .5, .1, zemphasInventory, true);


zemphas = Level.Level1(zemphas);

if (zemphas.alive)
{
    Level.Level2(zemphas);
}


