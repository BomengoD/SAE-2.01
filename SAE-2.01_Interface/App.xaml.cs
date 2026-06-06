using Librairie_Accès_BD;
using Librairie_Système.Gestion;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SAE_2._01_Interface
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ConnexionBD? BaseDeDonnees { get; private set; }
        public GestionJoueur? GestionJoueur { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                BaseDeDonnees = new ConnexionBD();
                await BaseDeDonnees.OuvrirConnexion();

                GestionJoueur = new GestionJoueur(BaseDeDonnees);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Impossible de se connecter : {ex.Message}");
                Shutdown();
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (BaseDeDonnees != null)
            {
                await BaseDeDonnees.DisposeAsync();
            }
            base.OnExit(e);
        }
    }

}
