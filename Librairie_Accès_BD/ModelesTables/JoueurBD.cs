using Librairie_Accès_BD;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
namespace Librairie_Accès_BD.ModelesTables
{
    public class JoueurBD
    {
        private int? _numjoueur;
        private string? _nomjoueur;
        private string? _prenomjoueur;
        private string _pseudojoueur;
        private string _mdpjoueur;
        private DateTime? _datenaissance;
        private string _typejoueur;
        private int? _numNationalite;
        private int? _numEquipe;
        private int? _numConfig;

        public JoueurBD(string pseudojoueur, string mdpjoueur, string typejoueur, int? numjoueur = null, DateTime? datenaissance=null, string? nomjoueur=null, string? prenomjoueur=null, int? numNationalite=null, int? numEquipe=null, int? numConfig=null)
        {
            this._numjoueur = numjoueur;
            this._nomjoueur = nomjoueur;
            this._prenomjoueur = prenomjoueur;
            this._pseudojoueur = pseudojoueur;
            this._mdpjoueur = mdpjoueur;
            this._datenaissance = datenaissance;
            this._typejoueur = typejoueur;
            this._numNationalite = numNationalite;
            this._numEquipe = numEquipe;
            this._numConfig = numConfig;
        }
        public int? NumJoueur
        {
            get { return this._numjoueur; }
            set { this._numjoueur = value; }
        }
        public string? NomJoueur
        {
            get { return this._nomjoueur; }
            set { this._nomjoueur = value; }
        }

        public string? PrenomJoueur
        {
            get { return this._prenomjoueur; }
            set { this._prenomjoueur = value; }
        }
        public string PseudoJoueur
        {
            get { return this._pseudojoueur; }
            set { this._pseudojoueur = value; }
        }
        public string MdpJoueur
        {
            get { return this._mdpjoueur; }
            set { this._mdpjoueur = value; }
        }
        public DateTime? DateNaissance
        {
            get { return this._datenaissance; }
            set { this._datenaissance = value; }
        }

        public string TypeJoueur
        {
            get { return this._typejoueur; }
            set { this._typejoueur = value;}
        }

        public int? NumNationalite
        {
            get { return this._numNationalite; }
            set { this._numNationalite = value; }
        }

        public int? NumEquipe
        {
            get { return this._numEquipe; }
            set { this._numEquipe = value; }
        }

        public int? NumConfig
        {
            get { return this._numConfig; }
            set { this._numConfig = value; }
        }




    }

    
}
