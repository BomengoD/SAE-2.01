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

        public class ConfigurationPartie
        {
            public string TypePartie { get; set; }
            public int Largeur { get; set; }
            public int Hauteur { get; set; }
            public int JetonsAligner { get; set; }
            public string Adversaire { get; set; }
        }
        private string typePartie;
        private int largeur, hauteur;
        private int jetonsAligner;
        private string adversaire;

        private void btn7x6_Click(object sender, RoutedEventArgs e)
        {
            adversaire = "IA";
        }

        private void btnPartieNormale_Click(object sender, RoutedEventArgs e)
        {
            typePartie = "normale";
        }

        private void btnLancer_Click(object sender, RoutedEventArgs e)
        {
            var config = new ConfigurationPartie
            {
                TypePartie = typePartie,
                Largeur = largeur,
                Hauteur = hauteur,
                JetonsAligner = jetonsAligner,
                Adversaire = adversaire
            };

            /*WindowJeu w = new WindowJeu(config);
            w.Show();
            this.Close();*/
        }

        private void txtJetons_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(txtJetons.Text, out jetonsAligner);
        }

    }
}
