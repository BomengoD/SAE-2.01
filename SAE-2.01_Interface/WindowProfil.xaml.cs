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

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            TxbErreurProfilNom.Text = "";
            TxbErreurMDP.Text = "";
            TxbErreurMDPConfirm.Text = "";

            bool erreur = false;

            if (string.IsNullOrWhiteSpace(TxtProfilNom.Text))
            {
                TxbErreurProfilNom.Text = "Le nom est obligatoire.";
                erreur = true;
            }

            if (TxtMDP.Text.Length < 6)
            {
                TxbErreurMDP.Text = "Le mot de passe doit contenir au moins 6 caractères.";
                erreur = true;
            }

            if (TxtMDP.Text != TxtMDPConfirm.Text)
            {
                TxbErreurMDPConfirm.Text = "Les mots de passe ne correspondent pas.";
                erreur = true;
            }

            if (!erreur)
            {
                MessageBox.Show("Profil créé avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void BtnReinitialiser_Click(object sender, RoutedEventArgs e)
        {
            TxtProfilNom.Text = "";
            TxtMDP.Text = "";
            TxtMDPConfirm.Text = "";
            TxbErreurProfilNom.Text = "";
            TxbErreurMDP.Text = "";
            TxbErreurMDPConfirm.Text = "";
        }
    }
}
