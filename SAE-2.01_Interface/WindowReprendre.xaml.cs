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
    /// Logique d'interaction pour WindowReprendre.xaml
    /// </summary>
    public partial class WindowReprendre : Window
    {
        public WindowReprendre()
        {
            InitializeComponent();
        }

        private void NouvellePartie_Click(object sender, RoutedEventArgs e)
        {
            var config = new WindowConfPartie();
            config.Show();
            this.Close();
        }

        private void ReprendrePartie_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
