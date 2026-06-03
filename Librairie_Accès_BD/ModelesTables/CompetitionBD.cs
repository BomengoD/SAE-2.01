using System;
using System.Collections.Generic;
using System.Text;

namespace Librairie_Accès_BD.ModelesTables
{
    public class CompetitionBD
    {
        private int _numcompetition;
        private string _typecompetition;
        private DateTime _datedebut;
        private DateTime _datefin;

        public CompetitionBD(string typecompetition, DateTime datedebut, DateTime datefin)
        {
            this._typecompetition = typecompetition;
            this._datedebut = datedebut;
            this._datefin = datefin;
        }

        public string TypeCompetition
        {
            get { return this._typecompetition; }
            set { this._typecompetition = value; }
        }

        public DateTime DateDebut
        {
            get { return this._datedebut; }
            set { this._datedebut = value; }
        } 
        public DateTime DateFin
        {
            get { return this._datefin; }   
            set { this._datefin = value; }
        }
    }
}
