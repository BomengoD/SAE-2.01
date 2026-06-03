using System;
using System.Collections.Generic;
using System.Text;

namespace Librairie_Accès_BD.ModelesTables
{
    public class ConfigurationBD
    {
        private int _numConfig;
        private string _couleurGrille;
        private string _formeJetons;
        private string _couleurJetons;

        public ConfigurationBD(string couleurGrille, string formeJetons, string couleurJetons)
        {
            this._couleurGrille = couleurGrille;
            this._formeJetons = formeJetons;
            this._couleurJetons = couleurJetons;
        }

        public string CouleurGrille
        {
            get { return this._couleurGrille; }
            set { this._couleurGrille = value; }
        }

        public string FormeJetons
        {
            get { return this._formeJetons; }
            set { this._formeJetons = value; }
        }

        public string CouleurJetons
        {
            get { return this._couleurJetons; }
            set { this._couleurJetons = value; }
        }
    }
}
