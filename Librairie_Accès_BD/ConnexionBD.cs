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
//Pour l'instant je gère la connexion grâce à un fichier secret : quand on fait clique droit sur le projet dans l'explorateur de solutions, y a une option pour gérer les données secrètes
//et en cliquant dessus ça génère un fichier secrets.json où je met les infos de connexion dedans, le souci c'est que vous devez tous le faire pour l'instant, j'ai pas d'inspi pour mieux faire
/*
{
  "Ssh:Server": "10.1.139.240",
  "Ssh:UserID": "identifiantupjv",
  "Ssh:Password": "mdpupjv",
  "Database:Server": "10.1.139.236",
  "Database:Name": "baseb3",
  "Database:UserID": "b3",
  "Database:Password": "FirstPromo1"
}
*/
namespace Librairie_Accès_BD
{
    public class ConnexionBD : IDisposable,IAsyncDisposable
    {
        private readonly string sshServer;
        private readonly string sshUserID;
        private readonly string sshPassword;

        private readonly string databaseServer;
        private readonly string databaseName;
        private readonly string databaseUserID;
        private readonly string databasePassword;
        
        private SshClient? Client;
        private ForwardedPortLocal? forwardedPort;
        private MySqlConnection? connection;
        private uint localPort;

        private bool _dispose;
        public MySqlConnection Connection => connection ?? throw new InvalidOperationException("Connexion non ouverte.");

        public ConnexionBD()
        {

            var config = new ConfigurationBuilder()
                .AddUserSecrets<ConnexionBD>()
                .Build();

            sshServer = config["Ssh:Server"] ?? throw new InvalidOperationException("Ssh:Server manquant");
            sshUserID = config["Ssh:UserID"] ?? throw new InvalidOperationException("Ssh:UserID manquant");
            sshPassword = config["Ssh:Password"] ?? throw new InvalidOperationException("Ssh:Password manquant");
            databaseServer = config["Database:Server"] ?? throw new InvalidOperationException("Database:Server manquant");
            databaseName = config["Database:Name"] ?? throw new InvalidOperationException("Database:Name manquant");
            databaseUserID = config["Database:UserID"] ?? throw new InvalidOperationException("Database:UserID manquant");
            databasePassword = config["Database:Password"] ?? throw new InvalidOperationException("Database:Password manquant");

            (Client, forwardedPort, localPort) = ConfigurationSsh.ConnectSsh(sshServer, sshUserID, sshPassword,databaseServer);

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
        public async Task OuvrirConnexion()
        {
            
             await connection!.OpenAsync();

        }

        public async Task FermerConnexion()
        {
            
            await connection!.CloseAsync();
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_dispose) 
                return;

            if (disposing)
            {
                
                connection?.Dispose();
            }


            _dispose = true;
        }
        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
            Dispose(disposing: false);
            GC.SuppressFinalize(this);
            
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (_dispose)
                return;


            if (connection is IAsyncDisposable asyncConn)
                await asyncConn.DisposeAsync();
            else
                connection?.Dispose();

            forwardedPort?.Stop();
            forwardedPort?.Dispose();
            Client?.Disconnect();
            Client?.Dispose();

            _dispose = true;
        }
    }
}
