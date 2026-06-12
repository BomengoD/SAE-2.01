using Librairie_Système;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SAE_2._01_Interface
{
    public partial class WindowJeu : Window
    {
        private int _nbLignes;
        private int _nbColonnes;
        private int _nbJetons;
        private int _profondeur;
        private string _difficulte;
        private string _adversaire;
        private string _mode;
        private int _secondesChrono;
        private int _secondesRestantes;
        private DispatcherTimer _timer;

        private const int JOUEUR1 = 1;
        private const int JOUEUR2 = 2;

        private int[,] _grille;
        private int[,] _table;
        private int[] _ordre;
        private bool _partieEnCours;
        private bool _tourJoueur;

        private const int NB_MANCHES = 3;
        private int _mancheActuelle;
        private int _scoreJ1;
        private int _scoreJ2;
        private bool _challengeEnCours;

        public WindowJeu(int nbLignes, int nbColonnes, int nbJetons, string mode, string adv)
        {
            InitializeComponent();

            _adversaire = (adv == "Invite") ? "Invite" : "IA";
            _difficulte = adv;
            _mode = mode;
            _secondesChrono = 30;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += Timer_Tick;

            if (_mode == "Challenge")
            {
                _mancheActuelle = 1;
                _scoreJ1 = 0;
                _scoreJ2 = 0;
                _challengeEnCours = true;
                InitialiserBandeauChallenge();
            }

            ActualiserPartie(nbLignes, nbColonnes, nbJetons);
        }

        private int CalculerProfondeur(int jetons) => _difficulte switch
        {
                "Moyen" => 2,
                "Difficile" => Math.Clamp(12 - jetons, 4, 10),
                _ => 0
            };

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

            if (_mode == "Challenge")
            {
                GridChallenge.Visibility = Visibility.Visible;
                MettreAJourBandeauChallenge();
            }
            else
            {
                GridChallenge.Visibility = Visibility.Collapsed;
            }

            if (_mode == "Chronometre")
            {
                GridChrono.Visibility = Visibility.Visible;
                ProgressChrono.Maximum = _secondesChrono;
                DemarrerChrono();
            }
            else
            {
                GridChrono.Visibility = Visibility.Collapsed;
                _timer.Stop();
            }

            BorderGrille.Background = ThemePartie.CouleurCadre;
            Grille.Rows = _nbLignes;
            Grille.Columns = _nbColonnes;
            Grille.Children.Clear();

            AfficherStatutTour();

            for (int ligne = 0; ligne < _nbLignes; ligne++)
                for (int col = 0; col < _nbColonnes; col++)
                {
                    var cercle = new Ellipse
                    {
                        Fill = ThemePartie.CouleurFond,
                        Stroke = Brushes.Transparent,
                        Margin = new Thickness(4),
                        Tag = col,
                        Stretch = Stretch.Uniform
                    };
                    cercle.MouseLeftButtonDown += Cercle_Click;
                    Grille.Children.Add(cercle);
                }
            }

        private void InitialiserBandeauChallenge()
        {
            LabelNomJ1.Text = (_adversaire == "Invite") ? "Joueur 1" : "Vous";
            LabelNomJ2.Text = (_adversaire == "Invite") ? "Joueur 2" : "IA";
        }

        private void MettreAJourBandeauChallenge()
        {
            LabelScoreJ1.Text = _scoreJ1.ToString();
            LabelScoreJ2.Text = _scoreJ2.ToString();
            LabelManche.Text = $"Manche {_mancheActuelle} / {NB_MANCHES}";

            var dots = new[] { DotManche1, DotManche2, DotManche3 };
            for (int i = 0; i < dots.Length; i++)
                if (i >= _mancheActuelle - 1)
                    dots[i].Fill = new SolidColorBrush(Color.FromRgb(85, 85, 85));
        }

        private void MarquerDotManche(int manche, Brush couleur)
        {
            switch (manche)
            {
                case 1: DotManche1.Fill = couleur; break;
                case 2: DotManche2.Fill = couleur; break;
                case 3: DotManche3.Fill = couleur; break;
            }
        }

        private void DemarrerChrono()
        {
            _timer.Stop();
            _secondesRestantes = _secondesChrono;
            MettreAJourBarreChrono();
            _timer.Start();
        }

        private void MettreAJourBarreChrono()
        {
            ProgressChrono.Value = _secondesRestantes;
            LabelChrono.Text = $"{_secondesRestantes}s";

            ProgressChrono.Foreground = _secondesRestantes > _secondesChrono / 2 ? Brushes.LimeGreen : _secondesRestantes > _secondesChrono / 4 ? Brushes.Orange : Brushes.Red;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _secondesRestantes--;
            MettreAJourBarreChrono();
            if (_secondesRestantes <= 0)
            {
                _timer.Stop();
                JouerColonneParDefaut();
            }
        }

        private void JouerColonneParDefaut()
        {
            for (int col = 0; col < _nbColonnes; col++)
            {
                if (!Jeu.ColonneJouable(_grille, col)) continue;

                if (_adversaire == "Invite")
                {
                    int joueurActuel = _tourJoueur ? JOUEUR1 : JOUEUR2;
                    string nom = _tourJoueur ? "Joueur 1" : "Joueur 2";
                    LabelStatut.Text = $"Temps écoulé ! {nom} joue automatiquement…";
                    LabelStatut.Foreground = Brushes.OrangeRed;

                    JouerEtAfficher(col, joueurActuel);
                    if (_partieEnCours)
                    {
                        _tourJoueur = !_tourJoueur;
                        AfficherStatutTour();
                        DemarrerChrono();
                    }
                }
                else
                {
                    LabelStatut.Text = "Temps écoulé ! Coup automatique joué…";
                    LabelStatut.Foreground = Brushes.OrangeRed;
                    JouerEtAfficher(col, JOUEUR1);
                    if (_partieEnCours) FaireJouerIA();
                }
                return;
            }
        }

        private void AfficherStatutTour()
        {
            string prefixe = (_mode == "Challenge") ? $"Manche {_mancheActuelle}/{NB_MANCHES} — " : $"Grille {_nbLignes}×{_nbColonnes} — {_nbJetons} jetons — ";

            if (_adversaire == "Invite")
            {
                if (_tourJoueur)
                {
                    LabelStatut.Text = prefixe + "Tour du Joueur 1";
                    LabelStatut.Foreground = Brushes.White;
                }
                else
                {
                    LabelStatut.Text = prefixe + "Tour du Joueur 2";
                    LabelStatut.Foreground = Brushes.Cyan;
                }
            }
            else
            {
                LabelStatut.Text = prefixe + "À vous de jouer !";
                LabelStatut.Foreground = Brushes.White;
            }
        }

        private void Cercle_Click(object sender, MouseButtonEventArgs e)
        {
            if (!_partieEnCours)
                return;
            if (_adversaire == "IA" && !_tourJoueur)
                return;

            int col = (int)((Ellipse)sender).Tag;
            if (!Jeu.ColonneJouable(_grille, col))
                return;

            if (_mode == "Chronometre") 
                _timer.Stop();

            if (_adversaire == "Invite")
            {
                int joueurActuel = _tourJoueur ? JOUEUR1 : JOUEUR2;
                JouerEtAfficher(col, joueurActuel);

                if (_partieEnCours)
                {
                    _tourJoueur = !_tourJoueur;
                    AfficherStatutTour();
                    if (_mode == "Chronometre") 
                        DemarrerChrono();
                }
            }
            else
            {
                JouerEtAfficher(col, JOUEUR1);
                if (_partieEnCours) 
                    FaireJouerIA();
            }
        }

        private void FaireJouerIA()
        {
                _tourJoueur = false;
                LabelStatut.Text = "L'IA réfléchit…";
                LabelStatut.Foreground = Brushes.Yellow;

                Dispatcher.InvokeAsync(() =>
                {
                    int colIA;

                    if (_difficulte == "Facile")
                    {
                    var jouables = new List<int>();
                        for (int c = 0; c < _nbColonnes; c++)
                            if (Jeu.ColonneJouable(_grille, c))
                            jouables.Add(c);
                    colIA = jouables[new Random().Next(jouables.Count)];
                    }
                    else
                    {
                    colIA = AlphaBetaClass.ChoisirCoupAlphaBeta( _grille, _nbColonnes, _nbLignes, _nbJetons, _profondeur, JOUEUR2, JOUEUR1, _table, _ordre);
                    }

                    JouerEtAfficher(colIA, JOUEUR2);

                    if (_partieEnCours)
                    {
                        _tourJoueur = true;
                    AfficherStatutTour();
                    if (_mode == "Chronometre") DemarrerChrono();
                    }
            }, DispatcherPriority.Background);
            }

        private void JouerEtAfficher(int col, int joueur)
        {
            int ligne = Jeu.JouerCoup(_grille, _nbLignes, col, joueur);
            if (ligne == -1)
                return;

            int index = ligne * _nbColonnes + col;
            ((Ellipse)Grille.Children[index]).Fill = (joueur == JOUEUR1) ? ThemePartie.CouleurJoueur : ThemePartie.CouleurIA;

            if (!IA.PartieTerminee(_grille, _nbColonnes, _nbLignes, _nbJetons, JOUEUR2, JOUEUR1, _table))
                return;

                _partieEnCours = false;
            _timer.Stop();

            int vainqueur = DeterminerVainqueur();

            if (_mode == "Challenge")
                TraiterFinMancheChallenge(vainqueur);
            else
                AfficherFinPartie(vainqueur);
        }

        private int DeterminerVainqueur()
        {
            int score = Heuristique.Parcours(_grille, _nbColonnes, _nbLignes, _nbJetons, JOUEUR2, JOUEUR1, _table);
                if (score >= 100000)
                return JOUEUR2;
            if (score <= -100000) 
                return JOUEUR1;
            return 0;
        }

        private void AfficherFinPartie(int vainqueur)
                {
            if (_adversaire == "Invite")
            {
                if (vainqueur == JOUEUR1)
                { 
                    LabelStatut.Text = "Le Joueur 1 a gagné ! 🎉"; LabelStatut.Foreground = Brushes.White; 
                }
                else if (vainqueur == JOUEUR2)
                {
                    LabelStatut.Text = "Le Joueur 2 a gagné ! 🎉"; LabelStatut.Foreground = Brushes.Cyan; 
                }
                else
                {
                    LabelStatut.Text = "Match nul !"; LabelStatut.Foreground = Brushes.Orange; 
                }
            }
            else
            {
                if (vainqueur == JOUEUR2)
                { 
                    LabelStatut.Text = "L'IA a gagné !"; LabelStatut.Foreground = Brushes.Yellow; 
                }
                else if (vainqueur == JOUEUR1)
                { 
                    LabelStatut.Text = "Vous avez gagné ! 🎉"; LabelStatut.Foreground = Brushes.Lime; 
                }
                else
                { 
                    LabelStatut.Text = "Match nul !"; LabelStatut.Foreground = Brushes.Orange; 
                }
            }
        }

        private void TraiterFinMancheChallenge(int vainqueur)
        {
            string nomJ1 = (_adversaire == "Invite") ? "Joueur 1" : "Vous";
            string nomJ2 = (_adversaire == "Invite") ? "Joueur 2" : "IA";

            if (vainqueur == JOUEUR1)
            {
                _scoreJ1++;
                MarquerDotManche(_mancheActuelle, (_adversaire == "Invite") ? Brushes.White : Brushes.Lime);
                LabelStatut.Text = $"Manche {_mancheActuelle} : {nomJ1} gagne ! Prochaine manche dans 3s…";
                LabelStatut.Foreground = (_adversaire == "Invite") ? Brushes.White : Brushes.Lime;
            }
            else if (vainqueur == JOUEUR2)
            {
                _scoreJ2++;
                MarquerDotManche(_mancheActuelle, (_adversaire == "Invite") ? Brushes.Cyan : Brushes.Yellow);
                LabelStatut.Text = $"Manche {_mancheActuelle} : {nomJ2} gagne ! Prochaine manche dans 3s…";
                LabelStatut.Foreground = (_adversaire == "Invite") ? Brushes.Cyan : Brushes.Yellow;
            }
            else
            {
                MarquerDotManche(_mancheActuelle, Brushes.Orange);
                LabelStatut.Text = $"Manche {_mancheActuelle} : Nul ! Prochaine manche dans 3s…";
                    LabelStatut.Foreground = Brushes.Orange;
                }

            LabelScoreJ1.Text = _scoreJ1.ToString();
            LabelScoreJ2.Text = _scoreJ2.ToString();

            int manchesRestantes = NB_MANCHES - _mancheActuelle;
            bool j1Assure = _scoreJ1 > _scoreJ2 + manchesRestantes;
            bool j2Assure = _scoreJ2 > _scoreJ1 + manchesRestantes;
            bool finChallenge = _mancheActuelle >= NB_MANCHES || j1Assure || j2Assure;

            if (finChallenge)
            {
                _challengeEnCours = false;
                var pause = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
                pause.Tick += (s, e) => { pause.Stop(); AfficherFinChallenge(nomJ1, nomJ2); };
                pause.Start();
            }
            else
            {
                _mancheActuelle++;
                var pause = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
                pause.Tick += (s, e) => { pause.Stop(); ActualiserPartie(_nbLignes, _nbColonnes, _nbJetons); };
                pause.Start();
            }
        }

        private void AfficherFinChallenge(string nomJ1, string nomJ2)
        {
            if (_scoreJ1 > _scoreJ2)
            {
                LabelStatut.Text = $"🏆 {nomJ1} remporte le challenge {_scoreJ1}-{_scoreJ2} !";
                LabelStatut.Foreground = (_adversaire == "Invite") ? Brushes.White : Brushes.Lime;
            }
            else if (_scoreJ2 > _scoreJ1)
            {
                LabelStatut.Text = $"🏆 {nomJ2} remporte le challenge {_scoreJ2}-{_scoreJ1} !";
                LabelStatut.Foreground = (_adversaire == "Invite") ? Brushes.Cyan : Brushes.Yellow;
            }
            else
            {
                LabelStatut.Text = $"🤝 Égalité parfaite {_scoreJ1}-{_scoreJ2} !";
                LabelStatut.Foreground = Brushes.Orange;
            }
        }
    }
}