using System;
using System.Collections.Generic;
using System.Text;

namespace Librairie_Accès_BD.ModelesTables
{
    public class SauvegardeBD
    {
        private int _numpartie;
        private int _numligne;
        private int _numcolonne;
        private int _placementjeton;

        public SauvegardeBD(int numpartie, int numligne, int numcolonne, int placementjeton)
        {
            this._numpartie = numpartie;
            this._numligne = numligne;
            this._numcolonne = numcolonne;
            this._placementjeton = placementjeton;
        }

        public int NumPartie
        {
            get { return this._numpartie; }
            set { this._numpartie = value; }
        }
        public int NumLigne
        {
            get { return this._numligne; }
            set { this._numligne = value; }
        }
        public int NumColonne
        {
            get { return this._numcolonne; }
            set { this._numcolonne = value; }
        }
        public int PlacementJeton
        {
            get { return this._placementjeton; }
            set { this._placementjeton = value; }
        }

    }
}
