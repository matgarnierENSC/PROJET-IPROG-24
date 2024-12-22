using System.Threading;

int longueurPlateau;
int largeurPlateau;
char[,] plateau;
int grenadesRestantes = 0;


void MenuPrincipal()
    {
        AfficherTitre("=== MENU PRINCIPAL ===");
        
        AfficherEcranDemarrage();
        string choix;
        do
        {
            Console.WriteLine("1 - Lancer le jeu");
            Console.WriteLine("2 - Lancer les tests");
            Console.WriteLine("3 - Quitter");
            Console.Write("\nEntrez votre choix : ");

            choix = Console.ReadLine()!;

            switch (choix)
            {
                case "1":
                    AfficherMessageAnime("Démarrage du jeu...\n", ConsoleColor.Green);
                    JeuGame();
                    return;

                case "2":
                    AfficherMessageAnime("Démarrage des tests...\n", ConsoleColor.Yellow);
                    LancerTests();
                    return;

                case "3":
                    AfficherMessageAnime("Fermeture du jeu. À bientôt !\n", ConsoleColor.Magenta);
                    Environment.Exit(0);
                    break;

                default:
                    AfficherMessageAnime("Choix invalide. Veuillez réessayer.\n", ConsoleColor.Red);
                    break;
            }
        } while (true);
    }


void LancerTests()
    {
        AfficherTitre("=== MENU DES TESTS ===");
        string choix;
        do
        {
            Console.WriteLine("1 - Test : Indominus mange Owen");
            Console.WriteLine("2 - Test : Recul de l'Indominus avec Maisie");
            Console.WriteLine("3 - Retour au menu principal");
            Console.Write("\nEntrez votre choix : ");

            choix = Console.ReadLine()!;

            switch (choix)
            {
                case "1":
                    TestMangeParIndo();
                    break;

                case "2":
                    TestRepousserIndominusAvecMaisie();
                    break;

                case "3":
                    AfficherMessageAnime("Retour au menu principal...\n", ConsoleColor.Cyan);
                    MenuPrincipal();
                    return;

                default:
                    AfficherMessageAnime("Choix invalide. Veuillez réessayer.\n", ConsoleColor.Red);
                    break;
            }
        } while (true);
    }

void AfficherEcranDemarrage()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Black;

        Console.WriteLine(new string('=', 40));
        Console.WriteLine("     *** BIENVENUE DANS LE JEU ***");
        Console.WriteLine(new string('=', 40));
        Console.ForegroundColor = ConsoleColor.DarkRed;
       Console.WriteLine(@"
                          
      | | |  | |  __ \|  ____| \ | |/ ____|_   _/ ____| |  __ \ /\   |  __ \| |/ /
      | | |  | | |__) | |__  |  \| | (___   | || |      | |__) /  \  | |__) | ' / 
  _   | | |  | |  _  /|  __| | . ` |\___ \  | || |      |  ___/ /\ \ |  _  /|  <  
 | |__| | |__| | | \ \| |____| |\  |____) |_| || |____  | |  / ____ \| | \ \| . \ 
  \____/ \____/|_|  \_\______|_| \_|_____/|_____\_____| |_| /_/    \_\_|  \_\_|\_\

");
        Console.ResetColor();

        AfficherMessageAnime("\nChargement du jeu en cours...\n", ConsoleColor.Gray);
        Thread.Sleep(2000);
        Console.Clear();
    }

void AfficherTitre(string titre)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(new string('*', titre.Length + 8));
        Console.WriteLine($"**  {titre}  **");
        Console.WriteLine(new string('*', titre.Length + 8));
        Console.ResetColor();
    }

void AfficherMessageAnime(string message, ConsoleColor couleur = ConsoleColor.White)
    {
        Console.ForegroundColor = couleur;
        foreach (char lettre in message)
        {
            Console.Write(lettre);
            Thread.Sleep(50); 
        }
        Console.ResetColor();
    }

void JeuGame()
{
    // Affichage initial
    AfficherMessageAnime("Le jeu commence maintenant...\n", ConsoleColor.Green);
    Console.Clear();
    AfficherTitre("=== JurENSCsic World ===");

    // Saisie des dimensions du plateau
    Console.Write("Entrez la longueur du plateau : ");
    longueurPlateau = Convert.ToInt32(Console.ReadLine());
    Console.Write("Entrez la largeur du plateau : ");
    largeurPlateau = Convert.ToInt32(Console.ReadLine());

    grenadesRestantes = longueurPlateau;

    // Initialisation du plateau
    Console.Clear();
    AfficherTitre("Initialisation du plateau...");
    InitialiserPlateau();
    AfficherPlateau();
    Thread.Sleep(2000);

    bool personnageMange = false;

    //Conditions de victoires et de défaite
    while (!IndominusEstIsolee() && grenadesRestantes > 0 && !personnageMange)
    {
        // Tour d'Owen
        Console.Clear();
        AfficherTitre("Tour d'Owen");
        Console.WriteLine("Voici l'état actuel du plateau :");
        AfficherPlateau();
        Thread.Sleep(1000);
        AfficherApercuDeplacement("Owen");
        DeplacerOwen();
        AfficherAvecDelaiEtEffacement();

        // Gestionnairedes grenades
        if (grenadesRestantes > 0)
        {
            Console.Clear();
            Console.WriteLine($"Grenades restantes : {grenadesRestantes}");
            Console.WriteLine("Voulez-vous lancer une grenade ? (oui/non)");
            string reponseGrenade = Console.ReadLine()!;
            if (reponseGrenade == "oui")
            {
                Console.Clear();
                AfficherTitre("Lancer de grenade");
                Console.WriteLine("Voici l'état actuel du plateau :");
                AfficherPlateau();
                LancerGrenade();
                grenadesRestantes--;
                AfficherAvecDelaiEtEffacement();

                if (IndominusEstIsolee()) break;
                
                Console.Clear(); 
            }
        }
        else
        {
            Console.WriteLine("Aucune grenade restante.");
            Thread.Sleep(1500);
        }

        // Tour de Maisie
        Console.Clear();
        AfficherTitre("Tour de Maisie");
        Console.WriteLine("Voici l'état actuel du plateau :");
        AfficherPlateau();
        Thread.Sleep(2000); 

        DeplacerMaisie();
        AfficherAvecDelaiEtEffacement();

        // Tour de Blue
        Console.Clear();
        AfficherTitre("Tour de Blue");
        Console.WriteLine("Voici l'état actuel du plateau :");
        AfficherPlateau();
        Thread.Sleep(2000); 
        AfficherApercuDeplacement("Blue");
        DeplacerBlue();
        AfficherAvecDelaiEtEffacement();

        // Tour de l'Indominus
        Console.Clear();
        AfficherTitre("Tour de l'Indominus");
        Console.WriteLine("Voici l'état actuel du plateau :");
        AfficherPlateau();
        Thread.Sleep(2000);
        DeplacerIndominus(out personnageMange);
        AfficherAvecDelaiEtEffacement();

        if (personnageMange) break;
    }

    // Résultats liés au conditions de victoires et de défaites
    Console.Clear();
    if (IndominusEstIsolee())
    {
        AfficherTitre("Victoire !");
        Console.WriteLine("L'Indominus Rex est enfermée seule par le fossé !");
    }
    else if (personnageMange)
    {
        AfficherTitre("Défaite !");
        Console.WriteLine("L'Indominus Rex a mangé Maisie ou Owen !");
    }
    else
    {
        AfficherTitre("Défaite !");
        Console.WriteLine("Plus de grenades disponibles.");
    }

    Console.WriteLine("\nMerci d'avoir joué !");
    Thread.Sleep(3000);
}

//Facilité lecture fonction JeuGame()
void AfficherApercuDeplacement(string personnage)
{
    Console.WriteLine("Choisissez une direction lors de votre tour !");
    Console.WriteLine("Fèche du haut - Haut");
    Console.WriteLine("Flèche du bas - Bas");
    Console.WriteLine("Flèche de gauche - Gauche");
    Console.WriteLine("Flèche de droite - Droite");
    Thread.Sleep(1500); 
}

//Facilité lecture jeu 
void AfficherAvecDelaiEtEffacement()
{
    Console.Clear();      
    AfficherPlateau();    
    Thread.Sleep(2000);    
}


//Initialisaiton du plateau de jeu
void InitialiserPlateau()
{
    plateau = new char[longueurPlateau, largeurPlateau];
    Random rand = new Random();

    for (int i = 0; i < longueurPlateau; i++)
    {
        for (int j = 0; j < largeurPlateau; j++)
        {
            plateau[i, j] = '.';
        }
    }

    char[] personnages = { 'O', 'B', 'M', 'I' };

    foreach (char personnage in personnages)
    {
        int x, y;
        do
        {
            x = rand.Next(longueurPlateau);
            y = rand.Next(largeurPlateau);
        }
        while (plateau[x, y] != '.');

        plateau[x, y] = personnage;
    }
}

//Design des différents éléments du plateau et permet de mettre à jour l'affichage du plateau après chaque action
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

// Permet de trouver la position d'un personnage donné et de renvoyer ses coordonnées. Renvoie (-1, -1) si pas de coordonnées.
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

//Gérer les déplacements d'Owen avec le clavier
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

    if (nouveauX >= 0 && nouveauX < longueurPlateau && nouveauY >= 0 && nouveauY < largeurPlateau && plateau[nouveauX, nouveauY] == '.') //Vérifie que le déplacement est possible
    {
        plateau[x, y] = '.';
        plateau[nouveauX, nouveauY] = 'O';
    }
    else
    {
        Console.WriteLine("Déplacement invalide.");
    }
}

// Gérer les lancers de grenades ainsi que leurs éclats.
void LancerGrenade()
{
    (int x, int y) = TrouverPosition('O');
    bool grenadePlacee = false;

    while (!grenadePlacee)
    {
        Console.WriteLine("Choisissez la distance de la grenade :");
        Console.WriteLine("Appuyer sur 1 pour lancer à 1 case d'Owen");
        Console.WriteLine("Appuyer sur 2 pour lancer à 2 cases d'Owen");
        Console.WriteLine("Appuyer sur 3 pour lancer à 3 cases d'Owen");

        int distance = 0;
        bool validDistance = false;
        while (!validDistance)
        {
            string input = Console.ReadLine()!;
            validDistance = int.TryParse(input, out distance) && (distance == 1 || distance == 2 || distance == 3); //Vérifie que la touche appuyer par l'utilisateur est bonne (1,2 ou 3).
            if (!validDistance)
            {
                Console.WriteLine("Distance invalide. Choisissez 1, 2 ou 3.");
            }
        }

        Console.WriteLine("Utilisez les flèches directionnelles pour choisir la direction de la grenade :");
        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); //Permet de ne pas renvoyer la touche du clavier appuyée sur la console

        int cibleX = x, cibleY = y;

        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow: cibleX = x - distance; break;
            case ConsoleKey.DownArrow: cibleX = x + distance; break;
            case ConsoleKey.LeftArrow: cibleY = y - distance; break;
            case ConsoleKey.RightArrow: cibleY = y + distance; break;
            default:
                Console.WriteLine("Direction invalide. Recommencez.");
                continue;
        }

        if (cibleX < 0 || cibleX >= longueurPlateau || cibleY < 0 || cibleY >= largeurPlateau)
        {
            Console.WriteLine("La grenade ne peut pas être lancée hors des limites du plateau. Recommencez.");
            continue;
        }

        if (plateau[cibleX, cibleY] == 'I')
        {
            Console.WriteLine("La grenade ne peut pas tomber directement sur l'Indominus. Elle est repoussée ! Recommencez.");
            continue;
        }

        grenadePlacee = true; 

        if (VerifierGrenadeTouchePersonnage(cibleX, cibleY)) //Vérifie que la grenade ne touche pas un personnage (à part I).
        {
            Environment.Exit(0);
        }

        plateau[cibleX, cibleY] = 'X';
        Console.WriteLine($"Grenade lancée sur ({cibleX}, {cibleY}) !");

        // Gestion des éclats
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
            if (plateau[adjX, adjY] != 'I')
            {
                if (VerifierGrenadeTouchePersonnage(adjX, adjY))
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
            else
            {
                Console.WriteLine("L'éclat ne peut pas affecter la case occupée par l'Indominus Rex.");
            }
        }
    }
}

//Booléen vérifiant que la grenade touche ou non un personnage (à part I).
bool VerifierGrenadeTouchePersonnage(int x, int y)
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

//Gérer les déplacements aléatoires de Maisie
//Ne pas oubliez de refaire Maisie car bug quand elle est bloquée
void DeplacerMaisie()
{
    (int x, int y) = TrouverPosition('M');
    Random rand = new Random();
    int nouveauX = 0;
    int nouveauY = 0;
    bool validMove = false;

    // Vérification si Maisie est bloquée
    bool estBloquee = true;
    int[,] directions = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

    for (int i = 0; i < 4; i++)
    {
        nouveauX = x + directions[i, 0];
        nouveauY = y + directions[i, 1];
        if (nouveauX >= 0 && nouveauX < longueurPlateau &&
            nouveauY >= 0 && nouveauY < largeurPlateau &&
            plateau[nouveauX, nouveauY] == '.')
        {
            estBloquee = false;
            break;
        }
    }

    if (estBloquee)
    {
        Console.WriteLine("Maisie est entourée et ne peut pas bouger !");
        return;
    }
    // Si Maisie n'est pas bloquée, on cherche un mouvement valide et elle bouge de manière aléatoire dans les directions possibles.
    while (!validMove)
    {
        int direction = rand.Next(4);
        nouveauX = x + directions[direction, 0];
        nouveauY = y + directions[direction, 1];

        if (nouveauX >= 0 && nouveauX < longueurPlateau &&
            nouveauY >= 0 && nouveauY < largeurPlateau &&
            plateau[nouveauX, nouveauY] == '.')
        {
            validMove = true;
        }
    }

    plateau[x, y] = '.';
    plateau[nouveauX, nouveauY] = 'M';

    Console.WriteLine($"Maisie était en ({x}, {y})");
    Console.WriteLine($"Maisie s'est déplacée vers ({nouveauX}, {nouveauY})");
}

//Similaire à DeplacerOwen() mais pour Blue
void DeplacerBlue()
{
    (int blueX, int blueY) = TrouverPosition('B');
    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
    int nouveauX = blueX, nouveauY = blueY;

    switch (keyInfo.Key)
    {
        case ConsoleKey.UpArrow: nouveauX = blueX - 1; break;
        case ConsoleKey.DownArrow: nouveauX = blueX + 1; break;
        case ConsoleKey.LeftArrow: nouveauY = blueY - 1; break;
        case ConsoleKey.RightArrow: nouveauY = blueY + 1; break;
        default:
            Console.WriteLine("Touche invalide.");
            return;
    }

    if (nouveauX >= 0 && nouveauX < longueurPlateau &&
        nouveauY >= 0 && nouveauY < largeurPlateau &&
        (plateau[nouveauX, nouveauY] == '.' || plateau[nouveauX, nouveauY] == 'I'))
    {
        if (plateau[nouveauX, nouveauY] == 'I')
        {
            RepousserIndominus(blueX, blueY, nouveauX, nouveauY);
        }
        else
        {
            plateau[blueX, blueY] = '.';
            plateau[nouveauX, nouveauY] = 'B';
        }
    }
    else
    {
        Console.WriteLine("Déplacement invalide.");
    }
}

//Similitude avec Maisie mais on rajoute un booléen qui change quand I est sur la même case que M et O (Condition PersonnageMangé)
void DeplacerIndominus(out bool personnageMange)
{
    personnageMange = false;
    (int x, int y) = TrouverPosition('I');
    Random rand = new Random();
    int nouveauX = 0;
    int nouveauY = 0;
    bool validMove = false;

    // Vérification si l'Indominus est bloquée
    bool estBloquee = true;
    int[,] directions = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

    for (int i = 0; i < 4; i++)
    {
        nouveauX = x + directions[i, 0];
        nouveauY = y + directions[i, 1];
        if (nouveauX >= 0 && nouveauX < longueurPlateau &&
            nouveauY >= 0 && nouveauY < largeurPlateau &&
            (plateau[nouveauX, nouveauY] == '.' || plateau[nouveauX, nouveauY] == 'M' || plateau[nouveauX, nouveauY] == 'O'))
        {
            estBloquee = false;
            break;
        }
    }

    if (estBloquee)
    {
        Console.WriteLine("L'Indominus Rex est bloquée et ne peut pas bouger !");
        return;
    }

    // Si l'Indominus n'est pas bloquée, elle bouge de manière aléatoire dans les directions possibles.
    while (!validMove)
    {
        int direction = rand.Next(4);
        nouveauX = x + directions[direction, 0];
        nouveauY = y + directions[direction, 1];

        if (nouveauX >= 0 && nouveauX < longueurPlateau &&
            nouveauY >= 0 && nouveauY < largeurPlateau &&
            (plateau[nouveauX, nouveauY] == '.' || plateau[nouveauX, nouveauY] == 'M' || plateau[nouveauX, nouveauY] == 'O'))
        {
            validMove = true;

            // Si l'Indominus mange un personnage
            if (plateau[nouveauX, nouveauY] == 'M' || plateau[nouveauX, nouveauY] == 'O')
            {
                personnageMange = true;
                char victime = plateau[nouveauX, nouveauY];
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"L'Indominus a mangé {victime} à la position ({nouveauX}, {nouveauY}) !");
                Console.ResetColor();
            }
        }
    }

    // Mise à jour du plateau
    plateau[x, y] = '.';
    plateau[nouveauX, nouveauY] = 'I';

    Console.WriteLine($"L'Indominus était en ({x}, {y})");
    Console.WriteLine($"L'Indominus s'est déplacée vers ({nouveauX}, {nouveauY})");
}



void RepousserIndominus(int blueX, int blueY, int indominusX, int indominusY)
{
    int dx = 0, dy = 0;

    //Vérifier dans quelle direction Blue pousse l'Indominus.
    if (indominusX > blueX) 
        dx = 1;  // Indominus est à droite de Blue.
    else if (indominusX < blueX) 
        dx = -1; // Indominus est à gauche de Blue

    
    if (indominusY > blueY) 
        dy = 1;  // Indominus est en bas de Blue
    else if (indominusY < blueY) 
        dy = -1; // Indominus est en haut de Blue


    int currentX = indominusX;
    int currentY = indominusY;
    int maxSteps = 3; // Nombre maximum de cases sur lesquelles l'Indominus peut reculer
    bool peutReculer = false; // Drapeau pour savoir si l'Indominus peut reculer

    // Vérification de la possibilité de déplacement de l'Indominus dans la direction calculée
    for (int step = 1; step <= maxSteps; step++)
    {
        int nextX = currentX + dx * step;
        int nextY = currentY + dy * step;

        // Si la prochaine position dépasse les limites du plateau, on arrête le mouvement
        if (nextX < 0 || nextX >= longueurPlateau || nextY < 0 || nextY >= largeurPlateau)
            break;

        // Si la case est vide ('.'), l'Indominus peut reculer.
        if (plateau[nextX, nextY] == '.')
        {
            peutReculer = true;
            break;
        }

        // Si la case est occupée par un autre personnage ou un obstacle (X, M, O, B), l'Indominus ne peut pas reculer.
        if (plateau[nextX, nextY] == 'X' || 
            plateau[nextX, nextY] == 'M' || 
            plateau[nextX, nextY] == 'O' || 
            plateau[nextX, nextY] == 'B')
        {
            break;
        }
    }

    // Si l'Indominus ne peut pas reculer, fin de la fonction.
    if (!peutReculer)
    {
        Console.WriteLine("L'Indominus ne peut pas reculer. Blue reste sur sa case.");
        return;
    }

    // Si l'Indominus peut reculer, on déplace l'Indominus, on fait cela dans une boucle for pour vérifier à chaque case que l'Indo peut encore reculer.
    for (int step = 1; step <= maxSteps; step++)
    {
        int nextX = currentX + dx;
        int nextY = currentY + dy;

        // Si la prochaine position dépasse les limites du plateau, on arrête le déplacement
        if (nextX < 0 || nextX >= longueurPlateau || nextY < 0 || nextY >= largeurPlateau)
        {
            Console.WriteLine($"L'Indominus atteint un bord après {step - 1} cases. S'arrête en ({currentX}, {currentY}).");
            break;
        }

        // Si la case est occupée par un autre personnage ou un obstacle (X, M, O, B), l'Indominus ne peut pas avancer.
        if (plateau[nextX, nextY] == 'X' || 
            plateau[nextX, nextY] == 'M' || 
            plateau[nextX, nextY] == 'O' || 
            plateau[nextX, nextY] == 'B')
        {
            Console.WriteLine($"L'Indominus rencontre un obstacle après {step - 1} cases. S'arrête en ({currentX}, {currentY}).");
            break;
        }

        // Sinon l'indominus se déplace d'une case dans la direction dans laquele Blue la pousse.
        currentX = nextX;
        currentY = nextY;
    }

    // Blue prend la place de l'Indominus et l'Indominus recule.
    plateau[indominusX, indominusY] = 'B';
    plateau[blueX, blueY] = '.';
    plateau[currentX, currentY] = 'I';

    
    Console.WriteLine($"Blue prend la place de l'Indominus en ({indominusX}, {indominusY}), l'Indominus recule à ({currentX}, {currentY}).");
}

// Fonction récursive vérifiant les cases adjacentes dans les 4 directions et marquant les cases visités tout en s'arrêtant si un X est touché.
(bool[,], bool) VerifierCase(bool[,] visite, int x, int y, int cibleX, int cibleY, bool contact)
{
    // Vérifie que la position (x, y) est hors du plateau ou déjà visitée.
    if (x < 0 || x >= longueurPlateau || y < 0 || y >= largeurPlateau || visite[x, y])
        return (visite, contact);  //Marque les cases visitées comme visitées.

    // Si la case contient un obstacle ('X'), on ne continue pas l'exploration dans cette direction.
    if (plateau[x, y] == 'X')
        return (visite, contact);  

    // Si la position actuelle correspond à la position de l'Indominus, on marque le contact.
    if (x == cibleX && y == cibleY)
        contact = true;

    // Marque la case comme visitée.
    visite[x, y] = true;

    
    // Parcours tout le plateau à partir de la position (x, y) jusqu'à ce qu'on rencontre un obstacle.
    (visite, contact) = VerifierCase(visite, x - 1, y, cibleX, cibleY, contact); 
    (visite, contact) = VerifierCase(visite, x + 1, y, cibleX, cibleY, contact); 
    (visite, contact) = VerifierCase(visite, x, y - 1, cibleX, cibleY, contact); 
    (visite, contact) = VerifierCase(visite, x, y + 1, cibleX, cibleY, contact); 

    
    return (visite, contact);
}

// Cette fonction détermine si l'Indominus est isolé.
bool IndominusEstIsolee()
{
   
    var (xOwen, yOwen) = TrouverPosition('O');
    var (xMaisie, yMaisie) = TrouverPosition('M');
    var (xIndominus, yIndominus) = TrouverPosition('I');

    // Crée un tableau pour suivre les cases visitées et une variable pour savoir si le contact a été établi entre deux positions.
    bool[,] visite = new bool[longueurPlateau, largeurPlateau];
    bool contact = false;

    // Vérifie si Owen peut atteindre l'Indominus à partir de sa position.
    (visite, contact) = VerifierCase(visite, xOwen, yOwen, xIndominus, yIndominus, contact);
    bool owenAvecIndominus = contact; 

    // Réinitialise les variables pour faire la même chose mais avec Maisie.
    visite = new bool[longueurPlateau, largeurPlateau];
    contact = false;

    
    (visite, contact) = VerifierCase(visite, xMaisie, yMaisie, xIndominus, yIndominus, contact);
    bool maisieAvecIndominus = contact; 

    // Si Owen et Maisie ne peuvent atteindre l'Indominus, alors l'Indominus est isolé.
    return !owenAvecIndominus && !maisieAvecIndominus;
}

///////// Tests /////////////////////////////////////////////////////////////////////////////////

void TestMangeParIndo()
{
    Console.WriteLine("=== Tests du déplacement d'Indominus ===");

    Console.WriteLine("\n--- Test : Indominus mange Owen ---");
    plateau = new char[,]
    {
        { 'X', 'X', 'X', 'X' },
        { '.', 'X', 'X', 'X' },
        { 'X', 'I', 'O', 'X' },
        { 'X', 'X', 'X', 'X' }
    };
    longueurPlateau = plateau.GetLength(0);
    largeurPlateau = plateau.GetLength(1);
    AfficherPlateau();

    Console.WriteLine("Indominus doit se déplacer pour manger Owen.");

    bool personnageMange = false;
    DeplacerIndominus(out personnageMange);

    AfficherPlateau();

    Console.WriteLine("Résultat attendu : True (Owen est mangé)");
    Console.WriteLine("Résultat obtenu : " + personnageMange);

    if (!personnageMange)
    {
        Console.WriteLine("Erreur : L'Indominus aurait dû manger Owen !");
    }
}

void TestRepousserIndominusAvecMaisie()
{
    Console.WriteLine("=== Tests du recul de l'Indominus lorsque Blue la pousse ===");

    plateau = new char[,]
    {
        { 'X', 'X', 'X', 'X', 'X', 'X' },
        { 'X', '.', '.', '.', '.', 'X' },
        { 'X', 'B', 'I', '.', 'M', '.' },
        { 'X', '.', '.', '.', '.', 'X' },
        { 'X', 'X', 'X', 'X', 'X', 'X' }
    };
    longueurPlateau = plateau.GetLength(0);
    largeurPlateau = plateau.GetLength(1);
    AfficherPlateau();

    Console.WriteLine("Blue doit pousser l'Indominus, Maisie est à 3 cases sur la droite.");

    var (blueX, blueY) = TrouverPosition('B');
    var (indominusX, indominusY) = TrouverPosition('I');
    var (maisieX, maisieY) = TrouverPosition('M');

    Console.WriteLine($"Position initiale de Blue : ({blueX}, {blueY})");
    Console.WriteLine($"Position initiale de l'Indominus : ({indominusX}, {indominusY})");
    Console.WriteLine($"Position initiale de Maisie : ({maisieX}, {maisieY})");

    RepousserIndominus(blueX, blueY, indominusX, indominusY);

    AfficherPlateau();

    var (newIndominusX, newIndominusY) = TrouverPosition('I');
    Console.WriteLine("Résultat attendu : L'Indominus recule mais ne peut pas atteindre Maisie.");
    Console.WriteLine("Position finale attendue de l'Indominus : (2, 1)");
    Console.WriteLine("Position finale actuelle de l'Indominus : " + $"({newIndominusX}, {newIndominusY})");
}

//Lancement du Jeu 

MenuPrincipal();






