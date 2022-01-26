using Zemphas;

Inventory zemphasInventory = new Inventory(new Sword("Dull Blade", 20, "dagger", "None"), 3);

Hero zemphas = new Hero("Zemphas", 2000, 200, .8, .5, .4, zemphasInventory);


Level.Level1(zemphas);
//Level.Level2();

