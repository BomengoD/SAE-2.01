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
    /// Logique d'interaction pour WindowProfil.xaml
    /// </summary>
    public partial class WindowProfil : Window
    {
        public WindowProfil()
        {
            InitializeComponent();
        }

        bool ModeConnexion = false;
        private void HyperlinkConnexion_Click(object sender, RoutedEventArgs e)
        {
            ModeConnexion = true;

            TxtProfilTitre.Text = "Connexion";

            BtnModifier.Visibility = Visibility.Collapsed;

            StackPanelMDPConfirm.Visibility = Visibility.Collapsed;

            TxbConnexion.Visibility = Visibility.Collapsed;
            TxbInscription.Visibility = Visibility.Visible;
        }

        private void HyperlinkInscription_Click(object sender, RoutedEventArgs e)
        {
            ModeConnexion = false;

            TxtProfilTitre.Text = "Création du Profil";

            BtnModifier.Visibility = Visibility.Visible;

            StackPanelMDPConfirm.Visibility = Visibility.Visible;


            TxbConnexion.Visibility = Visibility.Visible;
            TxbInscription.Visibility = Visibility.Collapsed;
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            TxbErreurProfilNom.Text = "";
            TxbErreurMDP.Text = "";
            TxbErreurMDPConfirm.Text = "";

            bool erreur = false;

            if (string.IsNullOrWhiteSpace(TxtProfilNom.Text))
            {
                TxbErreurProfilNom.Text = "Veuillez saisir un nom.";
                erreur = true;
            }

            if (PbMDP.Password.Length < 6)
            {
                TxbErreurMDP.Text = "Le mot de passe doit contenir au moins 6 caractères.";
                erreur = true;
            }

            if (!ModeConnexion && PbMDP.Password != PbMDPConfirm.Password)
            {
                TxbErreurMDPConfirm.Text = "Les mots de passe ne correspondent pas.";
                erreur = true;
            }

            if (!erreur)
            {
                if (ModeConnexion)
                    MessageBox.Show("Profil connecté avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Profil créé avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
        }

        private void BtnReinitialiser_Click(object sender, RoutedEventArgs e)
        {
            TxtProfilNom.Text = "";
            PbMDP.Password = "";
            PbMDPConfirm.Password = "";
            TxbErreurProfilNom.Text = "";
            TxbErreurMDP.Text = "";
            TxbErreurMDPConfirm.Text = "";
        }
    }
}
