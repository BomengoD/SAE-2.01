using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAE_2._01_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PageProfil_Click(object sender, RoutedEventArgs e)
        {
            var joueurco = ((App)Application.Current).GestionJoueur?.JoueurCo;
            if (joueurco != null)
            {
                WindowConsulterProfil consulterProfil = new WindowConsulterProfil();
                consulterProfil.Show();
                this.Close();
            }
            else
            {
                WindowProfil windowProfil = new WindowProfil();
                windowProfil.Show();
                this.Close();
            }

        }

        private void PageParametres_Click(object sender, RoutedEventArgs e)
        {
            Parametre parametre = new Parametre();
            parametre.Show();
            this.Close();
        }

        private void Jouer_Click(object sender, RoutedEventArgs e)
        {

            var rep = MessageBox.Show(
                "Voulez-vous regarder le tutoriel ?",
                "Tutoriel",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (rep == MessageBoxResult.Yes)
            {
                var tutoriel = new WindowTutoriel();
                tutoriel.ShowDialog();
            }
            else
            {
                var configuration = new WindowConfPartie();
                configuration.ShowDialog();

                var jouer = new WindowJeu();
                jouer.ShowDialog();

            }
        }
    }
}