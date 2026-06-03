using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Text;

namespace Librairie_Accès_BD.ModelesTables
{
    public class EquipeBD
    {
        private int _numequipe;
        private string _nomequipe;

        public EquipeBD(string nomequipe)
        {
            this._nomequipe = nomequipe;
        }

        public string NomEquipe
        {
            get { return this._nomequipe; }
            set { this._nomequipe = value; }
        }
    }
}
