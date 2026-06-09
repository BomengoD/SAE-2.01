using System;
using System.Collections.Generic;
using System.Text;

namespace Librairie_Système
{
    public class Jeu
    {
        public static bool ColonneJouable(int[,] grille, int j)
        {
            return grille[0, j] == 0;
        }

        public static int JouerCoup(int[,] grille, int nbLig, int j, int joueur)
        {
            for (int i = nbLig - 1; i >= 0; i--)
            {
                if (grille[i, j] == 0)
                {
                    grille[i, j] = joueur;
                    return i;
                }
            }

            return -1;
        }

        public static void AnnulerCoup(int[,] grille, int i, int j)
        {
            grille[i, j] = 0;
        }
    }
}
