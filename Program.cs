using Zemphas;

// Initiates the starting inventory
Inventory zemphasInventory = new Inventory(new Sword("Dull Blade", 20, "dagger", "None"), 0, 0, 3);

// Loads the Hero with all of the base stats, and the hero stores the inventory
Hero zemphas = new Hero(Modifiers.heroName(), Modifiers.heroHealth(), Modifiers.heroStrength(), Modifiers.heroCurrentDamage(),
    Modifiers.heroBaseDamage(), Modifiers.heroStartingLevel(), Modifiers.heroXP(), Modifiers.heroCritChance(), Modifiers.heroCritDamage(),
    Modifiers.heroEvasiveness(), zemphasInventory, Modifiers.heroAlive());

// Load state of Hero after first level
zemphas = Level.Level1(zemphas);
// Checks to make sure Hero is still alive and if he is then load state of Hero into Second Level
if (zemphas.alive)
{
    Level.Level2(zemphas);
}


