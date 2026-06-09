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
    public partial class WindowConfPartie : Window
    {
        public WindowConfPartie()
        {
            InitializeComponent();
        }

        private string _mode;
        private void BtnMode_Click(object sender, RoutedEventArgs e)
        {
            _mode = (string)((Button)sender).Tag;
        }

        private int _nbLignes;
        private int _nbColonnes;
        private void BtnGrille_Click(object sender, RoutedEventArgs e)
        {
            string tag = (string)((Button)sender).Tag;
            string[] parts = tag.Split(',');

            _nbLignes = int.Parse(parts[0]);
            _nbColonnes = int.Parse(parts[1]);
        }

        private string _adv;
        private void BtnAdversaire_Click(object sender, RoutedEventArgs e)
        {
            _adv = (string)((Button)sender).Tag;
        }

        private void btnLancer_Click(object sender, RoutedEventArgs e)
        {
            var jeu = new WindowJeu(_nbLignes, _nbColonnes, _mode, _adv);
            jeu.ShowDialog();
        }

    }
}
