using System;
using System.Collections.Generic;
using System.Text;
using Librairie_Accès_BD;
using Librairie_Accès_BD.ModelesTables;
using Librairie_Accès_BD.Tables;
namespace Librairie_Système.Gestion
{
    public class GestionJoueur
    {
        private readonly TableJoueur _table;
        public JoueurBD? JoueurCo { get; private set; }

        public GestionJoueur(ConnexionBD co)
        {
            this._table = new TableJoueur(co);
        }
        public async Task SeConnecter(string pseudo, string mdp)
        {

            var joueur = await _table.ObtenirJoueur(pseudo) ?? throw new Exception("Pseudo introuvable");
            if (joueur.MdpJoueur != mdp) throw new Exception("Mot de passe incorrect");

            JoueurCo = joueur;


        }

        public void SeDeconnecter()
        {
            JoueurCo = null;
        }

        public async Task Inscrire(string pseudo, string mdp, string typeJoueur = "Humain",int? numjoueur=null, DateTime? dateNaissance = null, string? nom = null, string? prenom = null, int? numNationalite = null, int? numEquipe = null, int? numConfig = null)
        {
            if (string.IsNullOrWhiteSpace(pseudo) || string.IsNullOrWhiteSpace(mdp)) throw new Exception("Ces champs sont requis");
            if (dateNaissance >= DateTime.Today) throw new Exception("Date de naissance invalide");
            if (numNationalite.HasValue && numNationalite < 1) throw new Exception("Numéro de nationalité invalide");
            if (numEquipe.HasValue && numEquipe < 1) throw new Exception("Numéro d'équipe invalide");
            if (numConfig.HasValue && numConfig < 1) throw new Exception("Numéro de configuration invalide");

            var existant = await _table.ObtenirJoueur(pseudo);
            if (existant != null) throw new Exception("Pseudo déjà utilisé");

            var joueur = new JoueurBD(pseudo, mdp, typeJoueur, numjoueur, dateNaissance, nom, prenom, numNationalite, numEquipe, numConfig);

            await _table.AjouterJoueur(joueur);
        }

        public async Task Modifier(string pseudo, string? mdp, string? typeJoueur = "Humain", DateTime? dateNaissance = null, string? nom = null, string? prenom = null, int? numNationalite = null, int? numEquipe = null, int? numConfig = null)
        {
            if(JoueurCo == null)
                throw new Exception("Aucun joueur connecté");
            if (dateNaissance >= DateTime.Today)
                throw new Exception("Date de naissance invalide");
            if (numNationalite.HasValue && numNationalite < 1)
                throw new Exception("Numéro de nationalité invalide");
            if (numEquipe.HasValue && numEquipe < 1)
                throw new Exception("Numéro d'équipe invalide");
            if (numConfig.HasValue && numConfig < 1)
                throw new Exception("Numéro de configuration invalide");
            var existant = await _table.ObtenirJoueur(pseudo);
            if (existant != null && existant.NumJoueur != JoueurCo.NumJoueur) throw new Exception("Pseudo déjà utilisé");
            JoueurCo.PseudoJoueur = pseudo;
            if (!string.IsNullOrWhiteSpace(mdp))
                JoueurCo.MdpJoueur = mdp;

            if (dateNaissance.HasValue)
                JoueurCo.DateNaissance = dateNaissance.Value;

            if (!string.IsNullOrWhiteSpace(nom))
                JoueurCo.NomJoueur = nom;

            if (!string.IsNullOrWhiteSpace(prenom))
                JoueurCo.PrenomJoueur = prenom;

            if (numNationalite.HasValue)
                JoueurCo.NumNationalite = numNationalite.Value;

            if (numEquipe.HasValue)
                JoueurCo.NumEquipe = numEquipe.Value;

            if (numConfig.HasValue)
                JoueurCo.NumConfig = numConfig.Value;
            await _table.ModifierJoueur(JoueurCo);
        }
    }
}
