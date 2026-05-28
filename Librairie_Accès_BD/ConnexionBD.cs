using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using MySqlConnector;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Librairie_Accès_BD
{
    public class ConnexionBD : IDisposable
    {
        //Il faut les infos SSH donc les infos d'un étudiant connecté au VPN de l'université pour que ça fonctionne
        private readonly string sshServer;
        private readonly string sshUserID;
        private readonly string sshPassword;
        //Il faut ensuite les infos de la bases de données qu'on va utiliser
        private readonly string databaseServer;
        private readonly string databaseName;
        private readonly string databaseUserID;
        private readonly string databasePassword;
        
        private SshClient? Client;
        private ForwardedPortLocal? forwardedPort;
        private MySqlConnection? connection;
        private uint localPort;
        public MySqlConnection Connection => connection ?? throw new InvalidOperationException("Connexion non ouverte.");

        //Constrcteur de la classe qui établit la connexion SSH et prépare la connexion à la base de données
        public ConnexionBD()
        {
            // Lecture des infos confidentielles pour la connexion
            var config = new ConfigurationBuilder()
                .AddUserSecrets<ConnexionBD>()
                .Build();
            //Soit il y a , soit il y a pas
            sshServer = config["Ssh:Server"] ?? throw new InvalidOperationException("Ssh:Server manquant");
            sshUserID = config["Ssh:UserID"] ?? throw new InvalidOperationException("Ssh:UserID manquant");
            sshPassword = config["Ssh:Password"] ?? throw new InvalidOperationException("Ssh:Password manquant");
            databaseServer = config["Database:Server"] ?? throw new InvalidOperationException("Database:Server manquant");
            databaseName = config["Database:Name"] ?? throw new InvalidOperationException("Database:Name manquant");
            databaseUserID = config["Database:UserID"] ?? throw new InvalidOperationException("Database:UserID manquant");
            databasePassword = config["Database:Password"] ?? throw new InvalidOperationException("Database:Password manquant");
            //établir la connexion SSH et obtenir le port local redirigé
            (Client, forwardedPort, localPort) = ConfigurationSsh.ConnectSsh(sshServer, sshUserID, sshPassword,databaseServer);
            // construire la chaîne de connexion MySQL en utilisant le port local redirigé
            var NotreCo = new MySqlConnectionStringBuilder
            {
                Server = "127.0.0.1",
                Port = localPort,
                Database = databaseName,
                UserID = databaseUserID,
                Password = databasePassword,
            };

            connection = new MySqlConnection(NotreCo.ConnectionString);
        }
        public void OuvrirConnexion()
        {
            //On ouvre la co
            connection?.Open();

        }

        public void FermerConnexion()
        {
            // Ferme la connexion à la base de données ou serveur sql si vous préférez mais on peut la re ouvrir après
            connection?.Close();
        }
        public void Dispose()
        {
            // Ferme la connexion au serveur SSH et là faut tout recommencer pour se reconnecter
            connection?.Dispose();
            forwardedPort?.Stop();
            forwardedPort?.Dispose();
            Client?.Disconnect();
            Client?.Dispose();
        }
    }
}
