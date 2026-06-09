using Librairie_Système;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SAE_2._01_Interface
{
    /// <summary>
    /// Logique d'interaction pour WindowJeu.xaml
    /// </summary>
    public partial class WindowJeu : Window
    {
        private int _nbLignes;
        private int _nbColonnes;
        private int _nbJetons;
        private int _profondeur;
        private const int JOUEUR1 = 1;
        private const int JOUEUR2 = 2;

        private int[,] _grille;
        private int[,] _table;
        private int[] _ordre;
        private bool _partieEnCours;
        private bool _tourJoueur;

        public WindowJeu(int nbLignes, int nbColonnes, int nbJetons, string mode, string adv)
        {
            InitializeComponent();
            ActualiserPartie(nbLignes, nbColonnes, nbJetons);
        }

        private int CalculerProfondeur(int jetons)
        {
            return Math.Clamp(12 - jetons, 4, 10);
        }
        
        private void ActualiserPartie(int nbLignes, int nbColonnes, int nbJetons)
        {
            _nbLignes = nbLignes;
            _nbColonnes = nbColonnes;
            _nbJetons = nbJetons;
            _profondeur = CalculerProfondeur(_nbJetons);

            _grille = new int[_nbLignes, _nbColonnes];
            _table = Heuristique.CalculerTable(_nbLignes, _nbColonnes, _nbJetons);
            _ordre = IA.CalculerOrdre(_nbColonnes);
            _partieEnCours = true;
            _tourJoueur = true;

            LabelStatut.Text = $"Grille {_nbLignes}×{_nbColonnes} — {_nbJetons} jetons à aligner — À vous de jouer !";
            LabelStatut.Foreground = Brushes.White;

            Grille.Rows = _nbLignes;
            Grille.Columns = _nbColonnes;
            Grille.Children.Clear();
            BorderGrille.Background = ThemePartie.CouleurCadre;

            for (int ligne = 0; ligne < _nbLignes; ligne++)
            {
                for (int col = 0; col < _nbColonnes; col++)
                {
                    Ellipse cercle = new Ellipse {Fill = ThemePartie.CouleurFond, Stroke = Brushes.Transparent, Margin = new Thickness(4), Tag = col, Stretch = Stretch.Uniform};

                    cercle.MouseLeftButtonDown += Cercle_Click;
                    Grille.Children.Add(cercle);
                }
            }
        }
        

        private void Cercle_Click(object sender, MouseButtonEventArgs e)
        {
            if (!_partieEnCours || !_tourJoueur)
                return;

            int col = (int)((Ellipse)sender).Tag;

            if (!Jeu.ColonneJouable(_grille, col))
                return;

            JouerEtAfficher(col, JOUEUR1);

            if (!_partieEnCours)
                return;

            _tourJoueur = false;
            LabelStatut.Text = "L'IA réfléchit…";
            LabelStatut.Foreground = Brushes.Yellow;

            Dispatcher.InvokeAsync(() =>
            {
                int colIA = AlphaBetaClass.ChoisirCoupAlphaBeta(_grille, _nbColonnes, _nbLignes, _nbJetons, _profondeur, JOUEUR2, JOUEUR1, _table, _ordre);

                JouerEtAfficher(colIA, JOUEUR2);

                if (_partieEnCours)
                {
                    _tourJoueur = true;
                    LabelStatut.Text = $"Grille {_nbLignes}×{_nbColonnes} — {_nbJetons} jetons à aligner — À vous de jouer !";
                    LabelStatut.Foreground = Brushes.White;
                }
            }, System.Windows.Threading.DispatcherPriority.Background);
        }

        private void JouerEtAfficher(int col, int joueur)
        {
            int ligne = Jeu.JouerCoup(_grille, _nbLignes, col, joueur);
            if (ligne == -1)
                return;

            int index = ligne * _nbColonnes + col;
            Ellipse e = (Ellipse)Grille.Children[index];
            e.Fill = (joueur == JOUEUR1) ? ThemePartie.CouleurJoueur : ThemePartie.CouleurIA;

            if (IA.PartieTerminee(_grille, _nbColonnes, _nbLignes, _nbJetons, JOUEUR2, JOUEUR1, _table))
            {
                _partieEnCours = false;

                int score = Heuristique.Parcours(_grille, _nbColonnes, _nbLignes, _nbJetons, JOUEUR2, JOUEUR1, _table);

                if (score >= 100000)
                {
                    LabelStatut.Text = "L'IA a gagné !";
                    LabelStatut.Foreground = Brushes.Yellow;
                }
                else if (score <= -100000)
                {
                    LabelStatut.Text = "Vous avez gagné ! 🎉";
                    LabelStatut.Foreground = Brushes.Lime;
                }
                else
                {
                    LabelStatut.Text = "Match nul !";
                    LabelStatut.Foreground = Brushes.Orange;
                }
            }
        }
    }
}