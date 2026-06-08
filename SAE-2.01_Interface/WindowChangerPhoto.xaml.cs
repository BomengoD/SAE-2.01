using SAE_2._01_Interface;
using System.Windows;

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
        WindowConsulterProfil profil = new WindowConsulterProfil();
        profil.Show();
        this.Close();
    }

    private void BtnHome_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show(
            "Retourner au menu ?",
            "Confirmation",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            MainWindow home = new MainWindow();
            home.Show();
            this.Close();
        }
    }
}