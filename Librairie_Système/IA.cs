using System;
using System.Collections.Generic;
using System.Text;

namespace Librairie_Système
{
    public class IA
    {
        public static int[] CalculerOrdre(int nbCol)
        {
            int[] ordre = new int[nbCol];
            int milieu = nbCol / 2;
            int index = 0;
            ordre[index++] = milieu;

            for (int i = 1; i <= milieu; i++)
            {
                if (milieu - i >= 0)
                    ordre[index++] = milieu - i;

                if (milieu + i < nbCol)
                    ordre[index++] = milieu + i;
            }

            return ordre;
        }




        public static bool PartieTerminee(int[,] grille, int nbCol, int nbLig, int nbJetons, int ia, int j2, int[,] table)
        {
            int score = Heuristique.Parcours(grille, nbCol, nbLig, nbJetons, ia, j2, table);

            if (score <= -100000 || score >= 100000)
                return true;

            for (int j = 0; j < nbCol; j++)
            {
                if (grille[0, j] == 0)
                    return false;
            }

            return true;
        }
    }
}
