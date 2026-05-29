using System;
using System.Collections.Generic;
using System.Text;
using Librairie_Accès_BD;
namespace Librairie_Système
{
    public class Joueur
    {
        private readonly ConnexionBD bd;

        public Joueur(ConnexionBD bd)
        {
            this.bd = bd;
        }
    }
}
