using System;
using System.Collections.Generic;
using System.Text;

namespace Librairie_Accès_BD.ModelesTables
{
    public class JouerBD
    {
        private int _numjoueura;
        private int _numJoueurb;
        private DateTime _date;
        private TimeSpan _heure;
        private int _numPartie;

        public JouerBD(int numjoueura, int numJoueurb, DateTime date, TimeSpan heure, int numPartie)
        {
            _numjoueura = numjoueura;
            _numJoueurb = numJoueurb;
            _date = date;
            _heure = heure;
            _numPartie = numPartie;
        }

        public int NumJoueurA
        {
            get { return _numjoueura; }
            set { _numjoueura = value; }
        }
        public int NumJoueurB
        {
            get { return _numJoueurb; }
            set { _numJoueurb = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public TimeSpan Heure
        {
            get { return _heure; }
            set { _heure = value; }
        }
        public int NumPartie
        {
            get { return _numPartie; }
            set { _numPartie = value; }
        }
    }
}
