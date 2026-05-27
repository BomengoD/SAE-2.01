//Package qui permet de réaliser une connexion à une base de données MySQL
using Microsoft.VisualBasic.FileIO;
using MySqlConnector;
//Package qui permet de réaliser une connexion via SSH
using Renci.SshNet;
using Renci.SshNet.Security;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.X86;

namespace Librairie_Accès_BD
//On utilise une méthode statique  
/*
sshHostName: l'IP du server SSH ou son nom d'hôte
sshUserName: L'id de l'étudiant pour se connecter au serveur SSH
sshPassword: Le mot de passe de l'étudiant pour se connecter au serveur SSH
sshPort: Le port du serveur SSH (par défaut 22)
databaseServer: Le nom d'hôte ou l'adresse IP du serveur de base de données MySQL auquel se connecter via le tunnel SSH (par défaut "localhost") mais nous on va mettre l'adresse IP du serveur 240 là
databasePort: Le port sur lequel le serveur MySQL écoute (par défaut 3306)
*/
{
    public class ConfigurationSsh
    {
        public static (SshClient SshClient,ForwardedPortLocal forwardedport, uint Port) ConnectSsh(string sshHostName, string sshUserName, string sshPassword, string databaseServer,int sshPort = 22, int databasePort = 3306)
        {
            // vérifier les arguments
            //la classe ArgumentException est utilisée pour signaler qu'une méthode a reçu un argument qui n'est pas valide.
            if (string.IsNullOrEmpty(sshHostName))
                throw new ArgumentException($"{nameof(sshHostName)} doit être spécifié.", nameof(sshHostName));
            if (string.IsNullOrEmpty(sshUserName))
                throw new ArgumentException($"{nameof(sshUserName)} doit être spécifié.", nameof(sshUserName));
            if (string.IsNullOrEmpty(sshPassword))
                throw new ArgumentException($"{nameof(sshPassword)} doit être spécifié.", nameof(sshPassword));
            if (string.IsNullOrEmpty(databaseServer))
                throw new ArgumentException($"{nameof(databaseServer)} doit être spécifié.", nameof(databaseServer));

            // définir les méthodes d'authentification à utiliser (dans l'ordre) en fonction de ce qui est spécifié pour la sécurité SSH
            //var nous permet de déclarer une variable sans spécifier explicitement son type, le compilateur le définira lors de la compilation en fonction de la valeur qui lui est assignée.
            var authenticationMethods = new List<AuthenticationMethod>();
            if (!string.IsNullOrEmpty(sshPassword))
            {
                authenticationMethods.Add(new PasswordAuthenticationMethod(sshUserName, sshPassword));
            }

            // se connecter au serveur SSH
            //La classe SshClient du package Renci.SshNet est utilisée pour établir une connexion SSH avec un serveur distant
            //La classe ConnectionInfo est tout simplement utilisée pour les infos de connexion
            //ToArray() est une méthode qui convertit une collection en un tableau
            //Dans notre cas elle est utilisée pour convertir la liste des méthodes d'authentification en un tableau car le constructeur de SshClient attend un tableau
            var sshClient = new SshClient(new ConnectionInfo(sshHostName, sshPort, sshUserName, authenticationMethods.ToArray()));
            sshClient.Connect();

            // rediriger un port local vers le serveur de base de données et son port via le serveur SSH
            //La classe ForwardedPortLocal du package Renci.SshNet est utilisée pour créer un tunnel SSH qui redirige le trafic d'un port local vers un port distant via une connexion SSH
            var forwardedPort = new ForwardedPortLocal("127.0.0.1", databaseServer, (uint)databasePort);
            sshClient.AddForwardedPort(forwardedPort);
            forwardedPort.Start();

            return (sshClient, forwardedPort, forwardedPort.BoundPort);
        }
    }
}