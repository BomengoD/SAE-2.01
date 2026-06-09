using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SAE_2._01_Interface
{
    public partial class WindowChangerPhoto : Window
    {
        public WindowChangerPhoto()
        {
            InitializeComponent();
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Les paramètres ont été sauvegardés.",
                "Validation",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}