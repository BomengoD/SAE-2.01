using Librairie_Accès_BD;
using MySqlConnector;
try
{
    Console.WriteLine("Connexion en cours...");
    await using var bd = new ConnexionBD();
    bd.OuvrirConnexion();

    using var cmd = new MySqlCommand("SELECT * FROM JOUEUR;", bd.Connection);
    using var reader = cmd.ExecuteReader();

    Console.WriteLine("Tables trouvées :");
    while (reader.Read())
        Console.WriteLine($"  - {reader["Typejoueur"]}");
}
catch (Exception ex)
{
    Console.WriteLine($"Erreur : {ex.Message}");
} 