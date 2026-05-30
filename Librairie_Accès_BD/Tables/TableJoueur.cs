using MySqlConnector;
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


        public async Task AjouterJoueur(string nomjoueur, string prenomjoueur, string pseudo, string mdp, string datenaissance, string typejoueur)
        {
            await using var co = new ConnexionBD();
            await co.OuvrirConnexion();

            string sql = "INSERT INTO Joueur (NomJoueur, PrenomJoueur, PseudoJoueur, MdpJoueur, DateNaissance, TypeJoueur) VALUES (@nom, @prenom, @pseudo, @mdp, @date_naissance, @type)";
            using var cmd = new MySqlCommand(sql, co.Connection);
            cmd.Parameters.AddWithValue("@NomJoueur", nomjoueur);
            cmd.Parameters.AddWithValue("@PrenomJoueur", prenomjoueur);
            cmd.Parameters.AddWithValue("@PseudoJoueur", pseudo);
            cmd.Parameters.AddWithValue("@MdpJoueur", mdp);
            cmd.Parameters.AddWithValue("@DateNaissance", datenaissance);
            cmd.Parameters.AddWithValue("@TypeJoueur", typejoueur);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
