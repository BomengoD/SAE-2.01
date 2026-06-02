using System;
using System.Collections.Generic;
using System.Text;

namespace Librairie_Accès_BD.ModelesTables
{
    public class PartieBD
    {
        private int _numpartie;
        private int _premierjoueur;
        private int _nbcoups;
        private int _nblignes;
        private int _nbcolonnes;
        private int _nbjetonsalignes;
        private string _resultatja;
        private string _resultatjb;
        private string _touractuel;
        private int? _numcompetition;

        public PartieBD(int premierjoueur, int nbcoups, int nblignes, int nbcolonnes, int nbjetonsalignes, string resultatja, string resultatjb, string touractuel, int? numcompetition)
        {
            this._premierjoueur = premierjoueur;
            this._nbcoups = nbcoups;
            this._nblignes = nblignes;
            this._nbcolonnes = nbcolonnes;
            this._nbjetonsalignes = nbjetonsalignes;
            this._resultatja = resultatja;
            this._resultatjb = resultatjb;
            this._touractuel = touractuel;
            this._numcompetition = numcompetition;
        }

        public int NumPartie
        {
            get { return this._numpartie; }
            set { this._numpartie = value; }
        }
        
        public int PremierJoueur
        {
            get { return this._premierjoueur; }
            set { this._premierjoueur = value; }
        }

        public int NbCoups
        {
            get { return this._nbcoups; }
            set { this._nbcoups = value; }
        }
        
        public int NbLignes
        {
            get { return this._nblignes; }
            set { this._nblignes = value; }
        }
        
        public int NbColonnes
        {
            get { return this._nbcolonnes; }
            set { this._nbcolonnes = value; }
        }
        public int NbJetonsAlignes
        {
            get { return this._nbjetonsalignes; }
            set { this._nbjetonsalignes = value; }
        }
        public string ResultatJA
        {
            get { return this._resultatja; }
            set { this._resultatja = value; }
        }
        public string ResultatJB
        {
            get { return this._resultatjb; }
            set { this._resultatjb = value; }
        }

        public string TourActuel
        {
            get { return this._touractuel; }
            set { this._touractuel = value; }
        }

        public int? NumCompetition
        {
            get { return this._numcompetition; }
            set { this._numcompetition = value; }
        }

    }
}
