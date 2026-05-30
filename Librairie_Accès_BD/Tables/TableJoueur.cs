using Librairie_Accès_BD.ModelesTables;
using MySqlConnector;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Text;

namespace Librairie_Accès_BD.Tables
{
    public class TableJoueur
    {
        private readonly ConnexionBD _co;
        public TableJoueur(ConnexionBD co)
        {
            _co = co;
        }


        public async Task AjouterJoueur(Joueur joueur)
        {
            string sql = "INSERT INTO Joueur (NomJoueur, PrenomJoueur, PseudoJoueur, MdpJoueur, DateNaissance, TypeJoueur, NumNationalite, NumEquipe, NumConfig) VALUES (@nom, @prenom, @pseudo, @mdp, @date_naissance, @type,@nation,@equipe,@config)";
            using var cmd = new MySqlCommand(sql, _co.Connection);
            cmd.Parameters.AddWithValue("@nom", joueur.NomJoueur);
            cmd.Parameters.AddWithValue("@prenom", joueur.PrenomJoueur);
            cmd.Parameters.AddWithValue("@pseudo", joueur.PseudoJoueur);
            cmd.Parameters.AddWithValue("@mdp", joueur.MdpJoueur);
            cmd.Parameters.AddWithValue("@date_naissance", joueur.DateNaissance);
            cmd.Parameters.AddWithValue("@type", joueur.TypeJoueur);
            cmd.Parameters.AddWithValue("@nation", joueur.NumNationalite);
            cmd.Parameters.AddWithValue("@equipe", joueur.NumEquipe);
            cmd.Parameters.AddWithValue("@config", joueur.NumConfig);

            await cmd.ExecuteNonQueryAsync();
        }
        public async Task<Joueur?> ObtenirJoueur(int NumJoueur)
        {
            string sql = "SELECT * FROM Joueur WHERE NumJoueur = @num";
            using var cmd = new MySqlCommand(sql, _co.Connection);
            cmd.Parameters.AddWithValue("@num", NumJoueur);
            using var lecteur = await cmd.ExecuteReaderAsync();
            if (await lecteur.ReadAsync())
            {
                return Constructeur(lecteur);
            }
            else
            {
                return null;
            }
        }

        public async Task<Joueur?> ObtenirJoueur(string pseudo)
        {
            string sql = "SELECT * FROM Joueur WHERE PseudoJoueur = @pseudo";
            using var cmd = new MySqlCommand(sql, _co.Connection);
            cmd.Parameters.AddWithValue("@pseudo", pseudo);
            using var lecteur = await cmd.ExecuteReaderAsync();
            if (await lecteur.ReadAsync())
            {
                return Constructeur(lecteur);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Joueur>> ObtenirTousLesJoueurs()
        {
            string sql = "SELECT * FROM Joueur";
            using var cmd = new MySqlCommand(sql, _co.Connection);
            using var lecteur = await cmd.ExecuteReaderAsync();
            List<Joueur> joueurs = new List<Joueur>();
            while (await lecteur.ReadAsync())
            {
                joueurs.Add(Constructeur(lecteur));
            }
            return joueurs;
        } 
        public async Task ModifierJoueur(Joueur joueur)
        {
            string sql = "UPDATE Joueur SET NomJoueur = @nom, PrenomJoueur = @prenom, PseudoJoueur = @pseudo, MdpJoueur = @mdp, DateNaissance = @date_naissance, TypeJoueur = @type, NumNationalite = @nation, NumEquipe = @equipe, NumConfig = @config WHERE NumJoueur = @num";
            using var cmd = new MySqlCommand(sql, _co.Connection);
            cmd.Parameters.AddWithValue("@num", joueur.NumJoueur);
            cmd.Parameters.AddWithValue("@nom", joueur.NomJoueur);
            cmd.Parameters.AddWithValue("@prenom", joueur.PrenomJoueur);
            cmd.Parameters.AddWithValue("@pseudo", joueur.PseudoJoueur);
            cmd.Parameters.AddWithValue("@mdp", joueur.MdpJoueur);
            cmd.Parameters.AddWithValue("@date_naissance", joueur.DateNaissance);
            cmd.Parameters.AddWithValue("@type", joueur.TypeJoueur);
            cmd.Parameters.AddWithValue("@nation", joueur.NumNationalite as object ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@equipe", joueur.NumEquipe as object ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@config", joueur.NumConfig as object ?? DBNull.Value);
            await cmd.ExecuteNonQueryAsync();
        }
        public async Task SupprimeJoueur(int NumJoueur)
        {
            string sql = "DELETE FROM Joueur WHERE NumJoueur = @num";
            using var cmd = new MySqlCommand(sql, _co.Connection);
            cmd.Parameters.AddWithValue("@num", NumJoueur);
            await cmd.ExecuteNonQueryAsync();
        }

        private static Joueur Constructeur(MySqlDataReader lecteur)
        {
            return new Joueur(
                lecteur.GetString("NomJoueur"),
                lecteur.GetString("PrenomJoueur"),
                lecteur.GetString("PseudoJoueur"),
                lecteur.GetString("MdpJoueur"),
                lecteur.GetDateTime("DateNaissance"),
                lecteur.GetString("TypeJoueur"),
                lecteur.IsDBNull(lecteur.GetOrdinal("NumNationalite")) ? null : lecteur.GetInt32("NumNationalite"),
                lecteur.IsDBNull(lecteur.GetOrdinal("NumEquipe")) ? null : lecteur.GetInt32("NumEquipe"),
                lecteur.IsDBNull(lecteur.GetOrdinal("NumConfig")) ? null : lecteur.GetInt32("NumConfig")
            );
        }
    }
}
