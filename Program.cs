using Zemphas;

Hero zemphas = new Hero("Zemphas", 2000, 200, .8, .5, .4);
Inventory zemphasInventory = new Inventory(new Sword("Dull Blade", 20, "dagger", "None"), 3);

Level.Level1(zemphasInventory, zemphas);
//Level.Level2();

