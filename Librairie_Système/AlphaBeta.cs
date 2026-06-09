using System;
using System.Collections.Generic;
using System.Text;

namespace Librairie_Système
{
    public class AlphaBetaClass
    {
        public static int AlphaBeta(int[,] grille, int nbCol, int nbLig, int nbJetons, int profondeur, int alpha, int beta, bool estMax, int ia, int j2, int[,] table, int[] ordre)
        {
            if (profondeur == 0 || IA.PartieTerminee(grille, nbCol, nbLig, nbJetons, ia, j2, table))
            {
                return Heuristique.Parcours(grille, nbCol, nbLig, nbJetons, ia, j2, table);
            }

            if (estMax)
            {
                int meilleurScore = int.MinValue;

                for (int idx = 0; idx < ordre.Length; idx++)
                {
                    int j = ordre[idx];

                    if (Jeu.ColonneJouable(grille, j))
                    {
                        int i = Jeu.JouerCoup(grille, nbLig, j, ia);

                        int score = AlphaBeta(grille, nbCol, nbLig, nbJetons, profondeur - 1, alpha, beta, false, ia, j2, table, ordre);

                        Jeu.AnnulerCoup(grille, i, j);

                        if (score > meilleurScore)
                            meilleurScore = score;

                        if (meilleurScore > alpha)
                            alpha = meilleurScore;

                        if (beta <= alpha)
                            break;
                    }
                }

                return meilleurScore;
            }
            else
            {
                int meilleurScore = int.MaxValue;

                for (int idx = 0; idx < ordre.Length; idx++)
                {
                    int j = ordre[idx];

                    if (Jeu.ColonneJouable(grille, j))
                    {
                        int i = Jeu.JouerCoup(grille, nbLig, j, j2);

                        int score = AlphaBeta(grille, nbCol, nbLig, nbJetons, profondeur - 1, alpha, beta, true, ia, j2, table, ordre);

                        Jeu.AnnulerCoup(grille, i, j);

                        if (score < meilleurScore)
                            meilleurScore = score;

                        if (meilleurScore < beta)
                            beta = meilleurScore;

                        if (beta <= alpha)
                            break;
                    }
                }

                return meilleurScore;
            }
        }

        public static int ChoisirCoupAlphaBeta(int[,] grille, int nbCol, int nbLig, int nbJetons, int profondeur, int ia, int j2, int[,] table, int[] ordre)
        {
            for (int idx = 0; idx < ordre.Length; idx++)
            {
                int j = ordre[idx];

                if (Jeu.ColonneJouable(grille, j))
                {
                    int i = Jeu.JouerCoup(grille, nbLig, j, ia);

                    if (IA.PartieTerminee(grille, nbCol, nbLig, nbJetons, ia, j2, table))
                    {
                        Jeu.AnnulerCoup(grille, i, j);
                        return j;
                    }

                    Jeu.AnnulerCoup(grille, i, j);
                }
            }

            for (int idx = 0; idx < ordre.Length; idx++)
            {
                int j = ordre[idx];

                if (Jeu.ColonneJouable(grille, j))
                {
                    int i = Jeu.JouerCoup(grille, nbLig, j, j2);

                    if (IA.PartieTerminee(grille, nbCol, nbLig, nbJetons, ia, j2, table))
                    {
                        Jeu.AnnulerCoup(grille, i, j);
                        return j;
                    }

                    Jeu.AnnulerCoup(grille, i, j);
                }
            }

            int meilleurScore = int.MinValue;
            int meilleureColonne = -1;

            int alpha = int.MinValue;
            int beta = int.MaxValue;

            for (int idx = 0; idx < ordre.Length; idx++)
            {
                int j = ordre[idx];

                if (Jeu.ColonneJouable(grille, j))
                {
                    int i = Jeu.JouerCoup(grille, nbLig, j, ia);

                    int score = AlphaBeta(grille, nbCol, nbLig, nbJetons, profondeur - 1, alpha, beta, false, ia, j2, table, ordre);

                    Jeu.AnnulerCoup(grille, i, j);

                    if (score > meilleurScore)
                    {
                        meilleurScore = score;
                        meilleureColonne = j;
                    }

                    if (meilleurScore > alpha)
                        alpha = meilleurScore;
                }
            }

            return meilleureColonne;
        }

    }
}
