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
    /// Logique d'interaction pour WindowTutoriel.xaml
    /// </summary>
    public partial class WindowTutoriel : Window
    {
        private const int NbLignes = 6;
        private const int NbColonnes = 7;
        private const double TailleCase = 46;
        private const double TailleJeton = 38;
        private static readonly SolidColorBrush BrVide = Geler(80, 255, 255, 255); // blanc translucide
        private static readonly SolidColorBrush BrRouge = Geler(255, 231, 76, 60);
        private static readonly SolidColorBrush BrJaune = Geler(255, 241, 196, 15);
        private static readonly SolidColorBrush BrActif = Geler(255, 41, 128, 185); // dot actif (bleu)
        private static readonly SolidColorBrush BrInactif = Geler(255, 200, 200, 200); // dot inactif (gris)

        private List<EtapeTutoriel> _etapes = null!;
        private int _index;

        private readonly Ellipse[,] _cellules = new Ellipse[NbLignes, NbColonnes];
        private readonly TextBlock[] _fleches = new TextBlock[NbColonnes];
        private Ellipse[] _dots = null!;
        public WindowTutoriel()
        {
            InitializeComponent();
            InitialiserEtapes();
            ConstruirePlateau();
            ConstruireFleches();
            ConstruirePoints();
            AfficherEtape(0);
        }
        private static SolidColorBrush Geler(byte a, byte r, byte g, byte b)
        {
            var br = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            br.Freeze();
            return br;
        }
        private sealed record EtapeTutoriel(
            string Titre,
            string Description,
            int[,] Plateau,
            (int L, int C)[] Gagnantes,
            string Astuce = "",
            int ColFleche = -1
        );

        private void InitialiserEtapes()
        {
            var vide = new int[NbLignes, NbColonnes];
            var aucune = Array.Empty<(int, int)>();

            _etapes = new List<EtapeTutoriel>
            {
                new(
                    Titre:       "Bienvenue dans Puissance 4 !",
                    Description: "Le but du jeu est d'aligner 4 jetons de votre couleur " + "en premier.\n"
                                 + "L'alignement peut être horizontal, vertical ou diagonal.\n",
                    Plateau: new int[,]
                    {
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 }
                    },
                    Gagnantes:   aucune,
                    Astuce:      "Utilisez ◀ ▶ pour naviguer"
                ),

                new(
                    Titre:       "Faire tomber un jeton",
                    Description: "Cliquez sur la colonne de votre choix pour y " + "faire tomber un jeton.\n\n"
                                 + "Le jeton descend automatiquement jusqu'à la case " + "libre la plus basse de cette colonne.\n\n"
                                 + "La flèche ▼ indique la colonne choisie.",
                    Plateau: new int[,]
                    {
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 1, 0, 0, 0 }   // rouge en bas, colonne 4[3]
                    },
                    Gagnantes:  aucune,
                    ColFleche:  3
                ),

                new(
                    Titre:       "Les tours de jeu",
                    Description: "Les deux joueurs alternent leurs tours.\n\n"
                                 + "Rouge joue  →  Jaune joue  →  Rouge joue…\n\n"
                                 + "L'indicateur en haut de l'écran vous rappelle "
                                 + "à tout moment à qui c'est le tour.",
                    Plateau: new int[,]
                    {
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 1, 2, 0, 0, 0 },
                        { 0, 1, 2, 1, 2, 0, 0 }
                    },
                    Gagnantes: aucune
                ),

                new(
                    Titre:       "Aligner 4 jetons !",
                    Description: "Le premier à former une ligne de 4 jetons "
                                 + "consécutifs remporte la manche.\n\n"
                                 + "Ici, le joueur rouge aligne 4 jetons horizontalement "
                                 + "et remporte la manche.",
                    Plateau: new int[,]
                    {
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 2, 2, 2, 0, 0, 0 },
                        { 0, 1, 1, 1, 1, 0, 0 }
                    },
                    Gagnantes: new (int, int)[] { (5,1), (5,2), (5,3), (5,4) }
                ),

                new(
                    Titre:       "Pensez en diagonale !",
                    Description: "L'alignement diagonal est souvent le plus difficile " + "à repérer et le plus redoutable.\n\n"
                                 + "Ici, le joueur rouge gagne avec une diagonale montante.",
                    Plateau: new int[,]
                    {
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 1, 0, 0, 0 },
                        { 0, 0, 1, 2, 0, 0, 0 },
                        { 0, 1, 2, 2, 0, 0, 0 },
                        { 1, 2, 1, 2, 2, 0, 0 }
                    },
                    Gagnantes: new (int, int)[] { (5,0), (4,1), (3,2), (2,3) },
                    Astuce:    "Anticipez les diagonales adverses."
                )
            };
        }

        private void ConstruirePlateau()
        {
            for (int col = 0; col < NbColonnes; col ++)
                GridPlateau.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(TailleCase) });
            for (int ligne = 0; ligne < NbLignes; ligne++)
                GridPlateau.RowDefinitions.Add(new RowDefinition { Height = new GridLength(TailleCase) });

            for (int ligne = 0; ligne < NbLignes; ligne++)
                for (int col = 0; col < NbColonnes; col++)
                {
                    var cercle = new Ellipse{Width = TailleJeton, Height = TailleJeton, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Fill = BrVide,};
                    Grid.SetRow(cercle, ligne);
                    Grid.SetColumn(cercle, col);
                    GridPlateau.Children.Add(cercle);
                    _cellules[ligne, col] = cercle;
                }
        }

        private void ConstruireFleches()
        {
            for (int col = 0; col < NbColonnes; col++)
                GridFleche.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(TailleCase) });

            for (int col = 0; col < NbColonnes; col++)
            {
                var fleche = new TextBlock {Text = "▼", FontSize = 15, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Visibility = Visibility.Hidden};
                Grid.SetColumn(fleche, col);
                GridFleche.Children.Add(fleche);
                _fleches[col] = fleche;
            }
        }

        private void ConstruirePoints()
        {
            _dots = new Ellipse[_etapes.Count];
            for (int i = 0; i < _etapes.Count; i++)
            {
                var dot = new Ellipse {Width = 9, Height = 9, Margin = new Thickness(4, 0, 4, 0), Fill = BrInactif};
                SpProgres.Children.Add(dot);
                _dots[i] = dot;
            }
        }

        private void AfficherEtape(int index)
        {
            _index = index;
            var etape = _etapes[index];

            TxbNumeroEtape.Text = $"Étape {index + 1} / {_etapes.Count}";
            TxbTitreEtape.Text = etape.Titre;
            TxbDescription.Text = etape.Description;

            if (!string.IsNullOrEmpty(etape.Astuce))
            {
                TxbAstuce.Text = etape.Astuce;
                BdrAstuce.Visibility = Visibility.Visible;
            }
            else
            {
                BdrAstuce.Visibility = Visibility.Collapsed;
            }

            var victoire = new HashSet<(int, int)>(etape.Gagnantes);

            for (int ligne = 0; ligne < NbLignes; ligne++)
                for (int col = 0; col < NbColonnes; col++)
                {
                    bool estGagnante = victoire.Contains((ligne, col));
                    var cell = _cellules[ligne, col];

                    cell.Fill = etape.Plateau[ligne, col] switch { 1 => BrRouge, 2 => BrJaune, _ => BrVide};

                    cell.Stroke = estGagnante ? Brushes.White : null;
                    cell.StrokeThickness = estGagnante ? 3 : 0;
                    cell.Width = estGagnante ? TailleJeton + 3 : TailleJeton;
                    cell.Height = estGagnante ? TailleJeton + 3 : TailleJeton;
                }

            for (int col = 0; col < NbColonnes; col++)
            {
                _fleches[col].Visibility = Visibility.Visible;
                _fleches[col].Foreground = col == etape.ColFleche ? Brushes.Black : Brushes.White;
            }


            for (int i = 0; i < _dots.Length; i++)
                _dots[i].Fill = i == index ? BrActif : BrInactif;

            BtnPrecedent.IsEnabled = index > 0;

            bool estDerniere = index == _etapes.Count - 1;
            BtnSuivant.Visibility = estDerniere ? Visibility.Collapsed : Visibility.Visible;
            BtnTerminer.Visibility = estDerniere ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BtnSuivant_Click(object sender, RoutedEventArgs e)
        {
            if (_index < _etapes.Count - 1)
                AfficherEtape(_index + 1);
        }

        private void BtnPrecedent_Click(object sender, RoutedEventArgs e)
        {
            if (_index > 0)
                AfficherEtape(_index - 1);
        }

        private void BtnTerminer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var jeu = new WindowJeu(6, 7, 4, "Normal", "Facile");
            jeu.ShowDialog();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right when _index < _etapes.Count - 1:
                    AfficherEtape(_index + 1);
                    break;
                case Key.Left when _index > 0:
                    AfficherEtape(_index - 1);
                    break;
                case Key.Escape:
                    Close();
                    break;
            }
        }
    }
}
