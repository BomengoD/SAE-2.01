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
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class WindowConnexion : Window
    {
        public WindowConnexion()
        {
            InitializeComponent();
        }
        private void BtnConnexion_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtId.Text;
            string mdp = TxtMdp.Password;

             
            if (id == "admin" && mdp == "1234") //à remplacer par BDD si besoin
            {
                WindowProfil fen = new WindowProfil();
                fen.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Identifiants incorrects", "Erreur",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close();
        }
    }
}

