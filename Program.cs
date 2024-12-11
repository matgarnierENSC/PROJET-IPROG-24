﻿using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

int longueurPlateau;
int largeurPlateau;
char[,] plateau;
int grenadesRestantes = 0;




void JeuGame()
{

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


    
while (MortPersonnage() == false || PlusDeGrenade() == false )

{



    Console.WriteLine(TrouverPosition('O'));
    Console.WriteLine("\nC'est au tour d'Owen !");
    Thread.Sleep(1000);
    DeplacerOwen();

    AfficherPlateau();
    Thread.Sleep(2000);
    GrenadeInteraction();
    
    

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
    
    Console.Clear();
}

Console.WriteLine("Merci d'avoir joué !");
}
}

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
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[X] ");
                    break;
                case '.':
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("[ ] ");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("[ ] ");
                    break;
            }
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
    Console.WriteLine("Choisissez la distance de la grenade (1, 2, ou 3 cases) :");
    int distance = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Utilisez les flèches pour choisir la direction.");
    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

    int cibleX = x, cibleY = y;
    switch (keyInfo.Key)
    {
        case ConsoleKey.UpArrow: cibleX = x - distance; break;
        case ConsoleKey.DownArrow: cibleX = x + distance; break;
        case ConsoleKey.LeftArrow: cibleY = y - distance; break;
        case ConsoleKey.RightArrow: cibleY = y + distance; break;
        default: Console.WriteLine("Direction invalide."); return;
    }

    if (cibleX >= 0 && cibleX < longueurPlateau && cibleY >= 0 && cibleY < largeurPlateau)
    {
        plateau[cibleX, cibleY] = 'X';
        
    }
    
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

bool MortPersonnage()
{   
    bool FindeJeu = false ;
    while (!FindeJeu)
    {
    if (TrouverPosition('O') == (-1,-1) ||
        TrouverPosition('M') == (-1,-1) ||
        TrouverPosition('B') == (-1,-1) )
    {FindeJeu = true;}
    }
    return false; 
}

void GrenadeInteraction()
    {
    if (grenadesRestantes > 0)
    {
        Console.WriteLine("Voulez-vous lancer une grenade ? (oui/non)");
        string reponse = Console.ReadLine();
        if (reponse.ToLower() == "oui")
        {
            LancerGrenade();
            AfficherPlateau();
            Thread.Sleep(2000);
        }
    }
    }
bool PlusDeGrenade()   
    {   
        
        if (grenadesRestantes <= 0)
        {
        Console.WriteLine("Aucune grenade restante.");
        Thread.Sleep(1000);
        } 
        return false;

    }


JeuGame();