using System.Threading;

int longueurPlateau;
int largeurPlateau;
char[,] plateau;
int grenadesRestantes = 0;
int grenadeX = -1;
int grenadeY = -1;

void JeuGame()
{
    Console.WriteLine("Bienvenue dans Jurensic World !");
    Console.Write("Entrez la longueur du plateau : ");
    longueurPlateau = Convert.ToInt32(Console.ReadLine());
    Console.Write("Entrez la largeur du plateau : ");
    largeurPlateau = Convert.ToInt32(Console.ReadLine());

    int grenadesRestantes = longueurPlateau;

    Console.WriteLine("\nInitialisation du plateau...");
    InitialiserPlateau();
    AfficherPlateau();
    Thread.Sleep(2000);

    Console.WriteLine(TrouverPosition('O'));
    Console.WriteLine("\nC'est au tour d'Owen !");
    Thread.Sleep(1000);
    DeplacerOwen();

    AfficherPlateau();
    Thread.Sleep(2000);

    if (grenadesRestantes > 0)
    {
        Console.WriteLine("Voulez-vous lancer une grenade ? (oui/non)");
        string reponseGrenade = Console.ReadLine();
        if (reponseGrenade.ToLower() == "oui")
        {
            LancerGrenade();
            AfficherPlateau();
        }
    }
    else
    {
        Console.WriteLine("Aucune grenade restante.");
    }

    Console.WriteLine("\nC'est au tour de Maisie !");
    Thread.Sleep(1000);
    DeplacerMaisie();
    AfficherPlateau();
    Thread.Sleep(2000);

    Console.WriteLine("\nC'est au tour de Blue !");
    Thread.Sleep(1000);
    DeplacerBlue();
    Console.Clear();
    AfficherPlateau();
    Thread.Sleep(2000);

    Console.WriteLine("\nC'est au tour de l'Indominus !");
    Thread.Sleep(1000);
    DeplacerIndominus();
    AfficherPlateau();
    Thread.Sleep(2000);
    Console.WriteLine(TrouverPosition('O'));
    Console.WriteLine(TrouverPosition('I'));
}

Console.WriteLine("Merci d'avoir joué !");


void InitialiserPlateau()
{
    plateau = new char[longueurPlateau, largeurPlateau];
    for (int i = 0; i < longueurPlateau; i++)
    {
        for (int j = 0; j < largeurPlateau; j++)
        {
            plateau[i, j] = '.';
        }
    }
    plateau[0, 0] = 'O';
    plateau[1, 1] = 'B';
    plateau[2 % longueurPlateau, 2 % largeurPlateau] = 'M';
    plateau[longueurPlateau-2, largeurPlateau-2] = 'I';
}

void AfficherPlateau()
{
    Console.WriteLine("\nPlateau de jeu :");
    for (int i = 0; i < longueurPlateau; i++)
    {
        for (int j = 0; j < largeurPlateau; j++)
        {
            switch (plateau[i, j])
            {
                case 'O':
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[O] ");
                    break;
                case 'B':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("[B] ");
                    break;
                case 'M':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[M] ");
                    break;
                case 'I':
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("[I] ");
                    break;
                case 'X':
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[X] ");
                    break;
                case '.':
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("[ ] ");
                    break;
            }
            Console.ResetColor();
        }
        Console.WriteLine();
    }
}

(int, int) TrouverPosition(char perso)
{
    for (int i = 0; i < longueurPlateau; i++)
    {
        for (int j = 0; j < largeurPlateau; j++)
        {
            if (plateau[i, j] == perso)
            {
                return (i, j);
            }
        }
    }
    return (-1, -1);
}

void DeplacerOwen()
{
    Console.WriteLine("Déplacez Owen : Utilisez les flèches directionnelles (Haut, Bas, Gauche, Droite)");
    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
    (int x, int y) = TrouverPosition('O');

    int nouveauX = x, nouveauY = y;
    switch (keyInfo.Key)
    {
        case ConsoleKey.UpArrow: nouveauX = x - 1; break;
        case ConsoleKey.DownArrow: nouveauX = x + 1; break;
        case ConsoleKey.LeftArrow: nouveauY = y - 1; break;
        case ConsoleKey.RightArrow: nouveauY = y + 1; break;
        default: Console.WriteLine("Touche invalide."); return;
    }

    if (nouveauX >= 0 && nouveauX < longueurPlateau && nouveauY >= 0 && nouveauY < largeurPlateau && plateau[nouveauX, nouveauY] == '.')
    {
        plateau[x, y] = '.';
        plateau[nouveauX, nouveauY] = 'O';
    }
    else
    {
        Console.WriteLine("Déplacement invalide.");
    }
}

void LancerGrenade()
{
    (int x, int y) = TrouverPosition('O');
    Console.WriteLine("Choisissez la distance de la grenade :");
    Console.WriteLine("Appuyer sur 1 pour lancer à 1 case d'Owen");
    Console.WriteLine("Appuyer sur 2 pour lancer à 2 cases d'Owen");
    Console.WriteLine("Appuyer sur 3 pour lancer à 3 cases d'Owen");

    int distance = 0;
    bool validDistance = false;
    while (!validDistance)
    {
        string input = Console.ReadLine();
        validDistance = int.TryParse(input, out distance) && (distance == 1 || distance == 2 || distance == 3);
        if (!validDistance)
        {
            Console.WriteLine("Distance invalide. Choisissez 1, 2 ou 3.");
        }
    }

    Console.WriteLine("Utilisez les flèches directionnelles pour choisir la direction de la grenade :");
    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

    int cibleX = x, cibleY = y;

    switch (keyInfo.Key)
    {
        case ConsoleKey.UpArrow: cibleX = x - distance; break;
        case ConsoleKey.DownArrow: cibleX = x + distance; break;
        case ConsoleKey.LeftArrow: cibleY = y - distance; break;
        case ConsoleKey.RightArrow: cibleY = y + distance; break;
        default:
            Console.WriteLine("Direction invalide.");
            return;
    }

    if (cibleX < 0 || cibleX >= longueurPlateau || cibleY < 0 || cibleY >= largeurPlateau)
    {
        Console.WriteLine("La grenade ne peut pas être lancée hors des limites du plateau.");
        return;
    }

    if (VerifierCollisionGrenade(cibleX, cibleY))
    {
        Environment.Exit(0);
    }

    plateau[cibleX, cibleY] = 'X';
    Console.WriteLine($"Grenade lancée sur ({cibleX}, {cibleY}) !");

    Random rand = new Random();
    int directionAdj = rand.Next(4);
    int adjX = cibleX, adjY = cibleY;

    switch (directionAdj)
    {
        case 0: adjX = cibleX - 1; break;
        case 1: adjX = cibleX + 1; break;
        case 2: adjY = cibleY - 1; break;
        case 3: adjY = cibleY + 1; break;
    }

    if (adjX >= 0 && adjX < longueurPlateau && adjY >= 0 && adjY < largeurPlateau)
    {
        if (VerifierCollisionGrenade(adjX, adjY))
        {
            plateau[adjX, adjY] = 'X';
            Console.WriteLine($"L'éclat de la grenade a touché la case ({adjX}, {adjY}) !");
            Console.WriteLine($"Un personnage est mort en ({adjX}, {adjY}) !");
            Environment.Exit(0);
        }
        else
        {
            plateau[adjX, adjY] = 'X';
            Console.WriteLine($"L'éclat de la grenade a touché la case ({adjX}, {adjY}) !");
        }
    }

    grenadesRestantes--;
}

bool VerifierCollisionGrenade(int x, int y)
{
    if (plateau[x, y] == 'O' || plateau[x, y] == 'M' || plateau[x, y] == 'B')
    {
        Console.WriteLine($"La grenade a touché {plateau[x, y]} à la position ({x}, {y}) !");
        plateau[x, y] = 'X';
        AfficherPlateau();
        Console.WriteLine("Partie terminée !");
        return true;
    }
    return false;
}

void DeplacerMaisie()
{
    (int x, int y) = TrouverPosition('M');
    Random rand = new Random();
    int direction = rand.Next(4);

    int nouveauX = x;
    int nouveauY = y;
    bool validMove = false;

    while (!validMove)
    {
        switch (direction)
        {
            case 0: 
                nouveauX = x - 1;
                break;
            case 1: 
                nouveauX = x + 1;
                break;
            case 2:
                nouveauY = y - 1;
                break;
            case 3: 
                nouveauY = y + 1;
                break;
        }

        if (nouveauX >= 0 && nouveauX < longueurPlateau &&
            nouveauY >= 0 && nouveauY < largeurPlateau &&
            plateau[nouveauX, nouveauY] == '.')
        {
            validMove = true;
        }
        else
        {
            direction = rand.Next(4);
        }
    }

    plateau[x, y] = '.';
    plateau[nouveauX, nouveauY] = 'M';

    Console.WriteLine($"Maisie était en ({x}, {y})");
    Console.WriteLine($"Maisie s'est déplacée vers ({nouveauX}, {nouveauY})");
}

void DeplacerBlue()
{
    (int x, int y) = TrouverPosition('B');
    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
    int nouveauX = x, nouveauY = y;

    switch (keyInfo.Key)
    {
        case ConsoleKey.UpArrow: nouveauX = x - 1; break;
        case ConsoleKey.DownArrow: nouveauX = x + 1; break;
        case ConsoleKey.LeftArrow: nouveauY = y - 1; break;
        case ConsoleKey.RightArrow: nouveauY = y + 1; break;
        default: Console.WriteLine("Touche invalide."); return;
    }

    if (nouveauX >= 0 && nouveauX < longueurPlateau && nouveauY >= 0 && nouveauY < largeurPlateau && plateau[nouveauX, nouveauY] == '.')
    {
        plateau[x, y] = '.';
        plateau[nouveauX, nouveauY] = 'B';
    }
    else
    {
        Console.WriteLine("Déplacement invalide.");
    }
}

void DeplacerIndominus()
{
    (int x, int y) = TrouverPosition('I');
    Random rand = new Random();
    int direction = rand.Next(4);

    int nouveauX = x;
    int nouveauY = y;
    bool validMove = false;

    while (!validMove)
    {
        switch (direction)
        {
            case 0:
                nouveauX = x - 1;
                break;
            case 1:
                nouveauX = x + 1;
                break;
            case 2:
                nouveauY = y - 1;
                break;
            case 3:
                nouveauY = y + 1;
                break;
        }

        if (nouveauX >= 0 && nouveauX < longueurPlateau &&
            nouveauY >= 0 && nouveauY < largeurPlateau &&
            plateau[nouveauX, nouveauY] == '.')
        {
            validMove = true;
        }
        else
        {
            direction = rand.Next(4);
        }
    }

    plateau[x, y] = '.';
    plateau[nouveauX, nouveauY] = 'I';

    Console.WriteLine($"L'Indominus était en ({x}, {y})");
    Console.WriteLine($"L'indominus s'est déplacée vers ({nouveauX}, {nouveauY})");
}



JeuGame();
