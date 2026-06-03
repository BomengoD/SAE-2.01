using Librairie_Accès_BD.ModelesTables;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Librairie_Accès_BD.Tables
{
    public class TablePartie
    {
        private readonly ConnexionBD _co;

        public TablePartie(ConnexionBD co)
        {
            _co = co;
        }
        public async Task<List<PartieBD>> ObtenirToutesLesParties()
        {
            var result = new List<PartieBD>();
            using var cmd = new MySqlCommand("SELECT * FROM PARTIE", _co.Connection);
            using var lecteur = await cmd.ExecuteReaderAsync();
            while (lecteur.Read())
                result.Add(Constructeur(lecteur));
            return result;
        }

        public async Task<PartieBD?> ObtenirPartie(int id)
        {
            using var cmd = new MySqlCommand(
                "SELECT * FROM PARTIE WHERE NumPartie = @id", _co.Connection);
            cmd.Parameters.AddWithValue("@id", id);
            using var lecteur = await cmd.ExecuteReaderAsync();
            return lecteur.Read() ? Constructeur(lecteur) : null;
        }

        public async Task Ajouter(PartieBD p)
        {
            using var cmd = new MySqlCommand(@"
            INSERT INTO PARTIE 
                (PremierJoueur, NbCoups, NbLignes, NbColonnes, NbJetonsAlignes, ResultatJA, NumCompetition)
            VALUES 
                (@premier, @coups, @lignes, @colonnes, @jetons, @resultat, @compet);
            ",_co.Connection);

            cmd.Parameters.AddWithValue("@premier", p.PremierJoueur);
            cmd.Parameters.AddWithValue("@coups", p.NbCoups);
            cmd.Parameters.AddWithValue("@lignes", p.NbLignes);
            cmd.Parameters.AddWithValue("@colonnes", p.NbColonnes);
            cmd.Parameters.AddWithValue("@jetons", p.NbJetonsAlignes);
            cmd.Parameters.AddWithValue("@resultat", p.ResultatJA);
            cmd.Parameters.AddWithValue("@compet", p.NumCompetition as object ?? DBNull.Value);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task Modifier(PartieBD p)
        {
            using var cmd = new MySqlCommand(@"
            UPDATE PARTIE SET
                NbCoups = @coups, ResultatJA = @resultat
            WHERE NumPartie = @id",
                _co.Connection);

            cmd.Parameters.AddWithValue("@id", p.NumPartie);
            cmd.Parameters.AddWithValue("@coups", p.NbCoups);
            cmd.Parameters.AddWithValue("@resultat", p.ResultatJA);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task Supprimer(int id)
        {
            using var cmd = new MySqlCommand(
                "DELETE FROM PARTIE WHERE NumPartie = @id", _co.Connection);
            cmd.Parameters.AddWithValue("@id", id);
            await cmd.ExecuteNonQueryAsync();
        }

        private static PartieBD Constructeur(MySqlDataReader lecteur) 
        {
            return new PartieBD(
           lecteur.GetInt32("PremierJoueur"),
           lecteur.GetInt32("NbCoups"),
           lecteur.GetInt32("NbLignes"),
           lecteur.GetInt32("NbColonnes"),
           lecteur.GetInt32("NbJetonsAlignes"),
           lecteur.GetString("ResultatJA"),
           lecteur.GetString("ResultatJB"),
           lecteur.GetString("TourActuel"),
           lecteur.IsDBNull(lecteur.GetOrdinal("NumCompetition")) ? null : lecteur.GetInt32("NumCompetition")
            );
        }
    }
}
