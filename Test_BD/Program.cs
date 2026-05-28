using Librairie_Accès_BD;
using MySqlConnector;
try
{
    Console.WriteLine("Connexion en cours...");
    using var bd = new ConnexionBD();
    bd.OuvrirConnexion();

    using var cmd = new MySqlCommand("SHOW TABLES;", bd.Connection);
    using var reader = cmd.ExecuteReader();

    Console.WriteLine("Tables trouvées :");
    while (reader.Read())
        Console.WriteLine($"  - {reader.GetString(0)}");
}
catch (Exception ex)
{
    Console.WriteLine($"Erreur : {ex.Message}");
}