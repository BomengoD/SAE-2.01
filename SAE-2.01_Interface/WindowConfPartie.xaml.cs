using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SAE_2._01_Interface
{
    public partial class WindowConfPartie : Window
    {
        public WindowConfPartie()
        {
            InitializeComponent();
            MiseAJourFenetre();
            MiseAJourJetons();
        }

        private void MiseAJourFenetre()
        {
            btn6x7.IsChecked = true;
            _nbLignes = 6;
            _nbColonnes = 7; 
            btn4x5.IsChecked = false;
            btn8x9.IsChecked = false;
            btn10x11.IsChecked = false;

            btnPartieNormale.IsChecked = true;
            _mode = "Normale";
            btnChrono.IsChecked = false;
            btnChallenge.IsChecked = false;

            btnInvite.IsChecked = false;
            btnIA.IsChecked = false;
            BtnFacile.IsChecked = false;
            BtnMoyen.IsChecked = false;
            BtnDifficile.IsChecked = false;
            StackPanelDifficulte.Visibility = Visibility.Collapsed;

        }

        private (int rec, int max) CalculerNbJetons(int lignes, int colonnes)
        {
            return (Math.Min(lignes, colonnes) / 2 + 1, Math.Min(lignes, colonnes)-1);
        }

        private string _mode;
        private void BtnMode_Click(object sender, RoutedEventArgs e)
        {
            _mode = (string)((ToggleButton)sender).Tag;

            if (sender != btnPartieNormale)
                btnPartieNormale.IsChecked = false;
            if (sender != btnChrono)
                btnChrono.IsChecked = false;
            if (sender != btnChallenge) 
                btnChallenge.IsChecked = false;
        }

        private int _nbLignes;
        private int _nbColonnes;
        private void BtnGrille_Click(object sender, RoutedEventArgs e)
        {
            string tag = (string)((ToggleButton)sender).Tag;
            string[] parts = tag.Split(',');

            _nbLignes = int.Parse(parts[0]);
            _nbColonnes = int.Parse(parts[1]);

            if (sender != btn4x5)
                btn4x5.IsChecked = false;
            if (sender != btn6x7)
                btn6x7.IsChecked = false;
            if (sender != btn8x9)
                btn8x9.IsChecked = false;
            if (sender != btn10x11)
                btn10x11.IsChecked = false;

            MiseAJourJetons();
        }

        private void MiseAJourJetons()
        {
            int lignes = _nbLignes;
            int colonnes = _nbColonnes;
            (int rec, int max) = CalculerNbJetons(lignes, colonnes);

            if (max < 3)
            {
                SliderJetons.Value = 3;
                SliderJetons.Maximum = 3;
            }
            else if (rec < 3)
            {
                SliderJetons.Value = 3;
                SliderJetons.Maximum = max;
            }
            else
            {
                SliderJetons.Value = rec;
                SliderJetons.Maximum = max;
            }
        }

        private void SliderJetons_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TxtValeurJetons != null)
                TxtValeurJetons.Text = ((int)SliderJetons.Value).ToString();
        }

        private string _adv;
        private void BtnAdversaire_Click(object sender, RoutedEventArgs e)
        {
            _adv = (string)((ToggleButton)sender).Tag;

            if (_adv == "IA")
                StackPanelDifficulte.Visibility = Visibility.Visible;
            else
                StackPanelDifficulte.Visibility = Visibility.Collapsed;

            if (sender != btnInvite)
                btnInvite.IsChecked = false;
            if (sender != btnIA)
                btnIA.IsChecked = false;
        }

        private void BtnDifficulte_Click(object sender, RoutedEventArgs e)
        {
            _adv = (string)((ToggleButton)sender).Tag;

            if (sender != BtnFacile) 
                BtnFacile.IsChecked = false;
            if (sender != BtnMoyen) 
                BtnMoyen.IsChecked = false;
            if (sender != BtnDifficile) 
                BtnDifficile.IsChecked = false;
        }

        private void btnLancer_Click(object sender, RoutedEventArgs e)
        {
            bool adversaireChoisi = btnInvite.IsChecked == true || btnIA.IsChecked == true;

            bool difficulteOK = btnInvite.IsChecked == true || BtnFacile.IsChecked == true || BtnMoyen.IsChecked == true || BtnDifficile.IsChecked == true;

            if (!adversaireChoisi || !difficulteOK)
            {
                MessageBox.Show("Veuillez sélectionner un adversaire et une difficulté (si adversaire IA) avant de lancer la partie.", "Configuration Incomplète", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                int _jetons = (int)SliderJetons.Value;
                var jeu = new WindowJeu(_nbLignes, _nbColonnes, _jetons, _mode, _adv);
                jeu.ShowDialog();
            }
        }

        private void BtnReinitialiser_Click(object sender, RoutedEventArgs e)
        {
            MiseAJourFenetre();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Retourner au menu ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MainWindow home = new MainWindow();
                home.Show();
                this.Close();
            }
        }
    }
}