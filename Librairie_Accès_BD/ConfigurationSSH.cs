
using Microsoft.VisualBasic.FileIO;
using MySqlConnector;
using Renci.SshNet;
using Renci.SshNet.Security;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.X86;

namespace Librairie_Accès_BD
{
    public class ConfigurationSsh
    {
        public static (SshClient SshClient,ForwardedPortLocal forwardedport, uint Port) ConnectSsh(string sshHostName, string sshUserName, string sshPassword, string databaseServer,int sshPort = 22, int databasePort = 3306)
        {

            if (string.IsNullOrEmpty(sshHostName))
                throw new ArgumentException($"{nameof(sshHostName)} doit être spécifié.", nameof(sshHostName));
            if (string.IsNullOrEmpty(sshUserName))
                throw new ArgumentException($"{nameof(sshUserName)} doit être spécifié.", nameof(sshUserName));
            if (string.IsNullOrEmpty(sshPassword))
                throw new ArgumentException($"{nameof(sshPassword)} doit être spécifié.", nameof(sshPassword));
            if (string.IsNullOrEmpty(databaseServer))
                throw new ArgumentException($"{nameof(databaseServer)} doit être spécifié.", nameof(databaseServer));

            var authenticationMethods = new List<AuthenticationMethod>();
            if (!string.IsNullOrEmpty(sshPassword))
            {
                authenticationMethods.Add(new PasswordAuthenticationMethod(sshUserName, sshPassword));
            }


            var sshClient = new SshClient(new ConnectionInfo(sshHostName, sshPort, sshUserName, authenticationMethods.ToArray()));
            sshClient.Connect();

 
            var forwardedPort = new ForwardedPortLocal("127.0.0.1", databaseServer, (uint)databasePort);
            sshClient.AddForwardedPort(forwardedPort);
            forwardedPort.Start();

            return (sshClient, forwardedPort, forwardedPort.BoundPort);
        }
    }
}