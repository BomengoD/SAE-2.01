using System;
using System.Collections.Generic;
using System.Text;
using Librairie_Accès_BD;
namespace Librairie_Système
{
    public class TableJoueur
    {
        private readonly ConnexionBD co;
        public TableJoueur()
        {
            co = new ConnexionBD();
        }


        public void Ajouter_Joueur(string nom, string prenom, string pseudo, string motDePasse, string DateNaissance, string TypeJoueur)
        {
            string requete = $"INSERT INTO Joueurs (NomJoueur, PrenomJoueur, PseudoJoueur, MdpJoueur,DateNaissance,TypeJoueur) VALUES ('{nom}', '{prenom}', '{pseudo}', '{motDePasse}', '{DateNaissance}', '{TypeJoueur}')";
        }
    }
}
