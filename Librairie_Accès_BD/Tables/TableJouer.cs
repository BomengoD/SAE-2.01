using Librairie_Accès_BD.ModelesTables;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Librairie_Accès_BD.Tables
{
    public class TableJouer
    {
        private readonly ConnexionBD _co;

        public TableJouer(ConnexionBD co)
        {
            _co = co;
        }

        public async Task<List<JouerBD>> ObtenirTous()
        {
            var result = new List<JouerBD>();
            using var cmd = new MySqlCommand("SELECT * FROM JOUER", _co.Connection);
            using var lecteur = await cmd.ExecuteReaderAsync();
            while (lecteur.Read())
                result.Add(Constructeur(lecteur));
            return result;
        }

        public async Task<JouerBD?> Obtenir(int numJoueurA, int numJoueurB, int numPartie)
        {
            using var cmd = new MySqlCommand(
                "SELECT * FROM JOUER WHERE NumJoueurA = @numA AND NumJoueurB = @numB AND NumPartie = @numP", _co.Connection);
            cmd.Parameters.AddWithValue("@numA", numJoueurA);
            cmd.Parameters.AddWithValue("@numB", numJoueurB);
            cmd.Parameters.AddWithValue("@numP", numPartie);
            using var lecteur = await cmd.ExecuteReaderAsync();
            return lecteur.Read() ? Constructeur(lecteur) : null;
        }

        public async Task Ajouter(JouerBD j)
        {
            using var cmd = new MySqlCommand(@"
            INSERT INTO JOUER 
                (NumJoueurA, NumJoueurB, Date, Heure, NumPartie)
            VALUES 
                (@numA, @numB, @date, @heure, @numP);
            ", _co.Connection);
            cmd.Parameters.AddWithValue("@numA", j.NumJoueurA);
            cmd.Parameters.AddWithValue("@numB", j.NumJoueurB);
            cmd.Parameters.AddWithValue("@date", j.Date);
            cmd.Parameters.AddWithValue("@heure", j.Heure);
            cmd.Parameters.AddWithValue("@numP", j.NumPartie);
            await cmd.ExecuteNonQueryAsync();
        }
        
        public async Task Modifier(JouerBD j)
        {
            using var cmd = new MySqlCommand(@"
            UPDATE JOUER SET
                Date = @date, Heure = @heure
            WHERE NumJoueurA = @numA AND NumJoueurB = @numB AND NumPartie = @numP;
            ", _co.Connection);
            cmd.Parameters.AddWithValue("@date", j.Date);
            cmd.Parameters.AddWithValue("@heure", j.Heure);
            cmd.Parameters.AddWithValue("@numA", j.NumJoueurA);
            cmd.Parameters.AddWithValue("@numB", j.NumJoueurB);
            cmd.Parameters.AddWithValue("@numP", j.NumPartie);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task Supprimer(int numJoueurA, int numJoueurB, int numPartie)
        {
            using var cmd = new MySqlCommand(@"
            DELETE FROM JOUER 
            WHERE NumJoueurA = @numA AND NumJoueurB = @numB AND NumPartie = @numP;
            ", _co.Connection);
            cmd.Parameters.AddWithValue("@numA", numJoueurA);
            cmd.Parameters.AddWithValue("@numB", numJoueurB);
            cmd.Parameters.AddWithValue("@numP", numPartie);
            await cmd.ExecuteNonQueryAsync();
        }

        private static JouerBD Constructeur(MySqlDataReader lecteur)
        {
            return new JouerBD(
           lecteur.GetInt32("NumJoueurA"),
           lecteur.GetInt32("NumJoueurB"),
           lecteur.GetDateTime("Date"),
           lecteur.GetTimeSpan("Heure"),
           lecteur.GetInt32("NumPartie"));
        }
    }
}
