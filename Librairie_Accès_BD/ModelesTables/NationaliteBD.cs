using System;
using System.Collections.Generic;
using System.Text;

namespace Librairie_Accès_BD.ModelesTables
{
    public class NationaliteBD
    {
        private int _numnationalite;
        private string _nompays ;
        private string _abvpays ;

        public NationaliteBD(string nompays, string abvpays)
        {
            this._nompays = nompays;
            this._abvpays = abvpays;
        }

        public string NomPays
        {
            get { return this._nompays; }
            set { this._nompays = value; }
        }

        public string AbvPays
        {
            get { return this._abvpays; }
            set { this._abvpays = value; }
        }
    }
}
