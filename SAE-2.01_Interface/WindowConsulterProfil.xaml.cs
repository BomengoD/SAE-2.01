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
    /// Logique d'interaction pour WindowConsulterProfil.xaml
    /// </summary>
    public partial class WindowConsulterProfil : Window
    {
        public WindowConsulterProfil()
        {
            InitializeComponent();

        }
        private bool editionNom = false;

        private void BtnModifierNom_Click(object sender, RoutedEventArgs e)
        {
            editionNom = !editionNom;
            TxtProfilNom.IsReadOnly = !editionNom;
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            home.Show();
            this.Close();
        }

        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            WindowChangerPhoto changerPhoto = new WindowChangerPhoto();
            changerPhoto.Show();
            this.Close();
        }

    }
}
