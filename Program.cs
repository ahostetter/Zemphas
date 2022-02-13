using Zemphas;

bool wantToPlay = true;

while (wantToPlay)
{
    int userChoice = 1000;
    int i = 0;

    while (i == 0)
    {
        Console.WriteLine("Play Game [0]");
        Console.WriteLine("Exit [1]");

        try
        {
            userChoice = Convert.ToInt32(Console.ReadLine());
        }
        catch (Exception)
        {
            Console.WriteLine("You did not put in a number");
            userChoice = 1000;
        }

        if (userChoice == 0)
        {
            Console.WriteLine("Starting Game");
            Console.WriteLine();
            i = 1;
        }
        else if (userChoice == 1)
        {
            Console.WriteLine("Exiting Game");
            i = 1;
            wantToPlay = false;
        }
        else
        {
            Console.WriteLine("You did not put in a correct choice");
        }
    }

    if (wantToPlay == false)
    {
        break;
    }

    bool heroAlive = true;

    while (heroAlive == true)
    {
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

        if (!zemphas.alive)
        {
            heroAlive = false;
        }
    }
}






