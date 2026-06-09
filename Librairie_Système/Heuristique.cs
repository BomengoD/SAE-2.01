using System;
using System.Collections.Generic;
using System.Text;

namespace Librairie_Système
{
    public class Heuristique
    {
        public static int[,] CalculerTable(int nbLig, int nbCol, int nbJetons)
        {
            int[,] table = new int[nbLig, nbCol];
            for (int i = 0; i < nbLig; i++)
            {
                for (int j = 0; j <= nbCol - nbJetons; j++)
                {
                    for (int k = 0; k < nbJetons; k++)
                    {
                        table[i, j + k]++;
                    }
                }
            }

            for (int j = 0; j < nbCol; j++)
            {
                for (int i = 0; i <= nbLig - nbJetons; i++)
                {
                    for (int k = 0; k < nbJetons; k++)
                    {
                        table[i + k, j]++;
                    }
                }
            }

            for (int i = 0; i <= nbLig - nbJetons; i++)
            {
                for (int j = 0; j <= nbCol - nbJetons; j++)
                {
                    for (int k = 0; k < nbJetons; k++)
                    {
                        table[i + k, j + k]++;
                    }
                }
            }

            for (int i = nbJetons - 1; i < nbLig; i++)
            {
                for (int j = 0; j <= nbCol - nbJetons; j++)
                {
                    for (int k = 0; k < nbJetons; k++)
                    {
                        table[i - k, j + k]++;
                    }
                }
            }
            return table;
        }

        public static int AnalyseFenetre(int[] fenetre, int nbJetons, int ia, int j2)
        {
            int nbIA = 0;
            int nbJ2 = 0;

            foreach (int c in fenetre)
            {
                if (c == ia)
                    nbIA++;
                else if (c == j2)
                    nbJ2++;
            }

            if (nbIA > 0 && nbJ2 > 0)
                return 0;

            int scoreVictoire = (int)Math.Pow(10, nbJetons + 2);

            if (nbIA == nbJetons)
                return scoreVictoire;

            if (nbJ2 == nbJetons)
                return -scoreVictoire;

            if (nbIA > 0)
                return (int)Math.Pow(10, nbIA - 1);

            if (nbJ2 > 0)
                return -(int)Math.Pow(10, nbJ2 - 1);

            return 0;
        }

        public static int Parcours(int[,] grille, int nbCol, int nbLig, int nbJetons, int ia, int j2, int[,] table)
        {
            int score = 0;

            for (int i = 0; i < nbLig; i++)
            {
                for (int j = 0; j < nbCol; j++)
                {
                    if (grille[i, j] == ia)
                        score += table[i, j];
                    else if (grille[i, j] == j2)
                        score -= table[i, j];
                }
            }

            for (int i = 0; i < nbLig; i++)
            {
                for (int j = 0; j <= nbCol - nbJetons; j++)
                {
                    int[] fenetre = new int[nbJetons];

                    for (int k = 0; k < nbJetons; k++)
                        fenetre[k] = grille[i, j + k];

                    score += AnalyseFenetre(fenetre, nbJetons, ia, j2);
                }
            }

            for (int j = 0; j < nbCol; j++)
            {
                for (int i = 0; i <= nbLig - nbJetons; i++)
                {
                    int[] fenetre = new int[nbJetons];

                    for (int k = 0; k < nbJetons; k++)
                        fenetre[k] = grille[i + k, j];

                    score += AnalyseFenetre(fenetre, nbJetons, ia, j2);
                }
            }

            for (int i = 0; i <= nbLig - nbJetons; i++)
            {
                for (int j = 0; j <= nbCol - nbJetons; j++)
                {
                    int[] fenetre = new int[nbJetons];

                    for (int k = 0; k < nbJetons; k++)
                        fenetre[k] = grille[i + k, j + k];

                    score += AnalyseFenetre(fenetre, nbJetons, ia, j2);
                }
            }

            for (int i = nbJetons - 1; i < nbLig; i++)
            {
                for (int j = 0; j <= nbCol - nbJetons; j++)
                {
                    int[] fenetre = new int[nbJetons];

                    for (int k = 0; k < nbJetons; k++)
                        fenetre[k] = grille[i - k, j + k];

                    score += AnalyseFenetre(fenetre, nbJetons, ia, j2);
                }
            }

            return score;
        }
    }
}
