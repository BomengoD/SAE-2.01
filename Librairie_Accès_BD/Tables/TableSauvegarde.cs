using System;
using System.Collections.Generic;
using System.Text;
using Librairie_Accès_BD.ModelesTables;
using MySqlConnector;

namespace Librairie_Accès_BD.Tables
{
    public class TableSauvegarde
    {
        private readonly ConnexionBD _co;

        public TableSauvegarde(ConnexionBD co)
        {
            _co = co;
        }

        public async Task <SauvegardeBD?> Obtenir(int numPartie, int numLigne, int numColonne)
        {
            using var cmd = new MySqlCommand(
                "SELECT * FROM SAUVEGARDE WHERE NumPartie = @numP AND NumLigne = @numL AND NumColonne = @numC", _co.Connection);
            cmd.Parameters.AddWithValue("@numP", numPartie);
            cmd.Parameters.AddWithValue("@numL", numLigne);
            cmd.Parameters.AddWithValue("@numC", numColonne);
            using var lecteur = await cmd.ExecuteReaderAsync();
            return lecteur.Read() ? Constructeur(lecteur) : null;
        }

        public async Task<List<SauvegardeBD>> Obtenir(int numPartie)
        {
            using var cmd = new MySqlCommand(
                "SELECT * FROM SAUVEGARDE WHERE NumPartie = @numP", _co.Connection);
            cmd.Parameters.AddWithValue("@numP", numPartie);
            using var lecteur = await cmd.ExecuteReaderAsync();
            var sauvegardes = new List<SauvegardeBD>();
            while (lecteur.Read())
                sauvegardes.Add(Constructeur(lecteur));
            return sauvegardes;
        }

        public async Task Ajouter(SauvegardeBD s)
        {
            using var cmd = new MySqlCommand(@"
            INSERT INTO SAUVEGARDE 
                (NumPartie, NumLigne, NumColonne, PlacementJeton)
            VALUES 
                (@numP, @numL, @numC, @place);
            ", _co.Connection);
            cmd.Parameters.AddWithValue("@numP", s.NumPartie);
            cmd.Parameters.AddWithValue("@numL", s.NumLigne);
            cmd.Parameters.AddWithValue("@numC", s.NumColonne);
            cmd.Parameters.AddWithValue("@place", s.PlacementJeton);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task Supprimer(int numPartie)
        {
            using var cmd = new MySqlCommand(
                "DELETE FROM SAUVEGARDE WHERE NumPartie = @numP", _co.Connection);
            cmd.Parameters.AddWithValue("@numP", numPartie);
            await cmd.ExecuteNonQueryAsync();
        }


        private static SauvegardeBD Constructeur(MySqlDataReader lecteur)
        {
            return new SauvegardeBD(
                lecteur.GetInt32("NumPartie"),
                lecteur.GetInt32("NumLigne"),
                lecteur.GetInt32("NumColonne"),
                lecteur.GetInt32("PlacementJeton")
            );
        }
    }
}
