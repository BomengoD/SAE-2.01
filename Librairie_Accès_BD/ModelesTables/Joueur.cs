using Librairie_Accès_BD;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
namespace Librairie_Accès_BD.ModelesTables
{
    public class Joueur : INotifyPropertyChanged
    {
        private int _numjoueur;
        private string _nomjoueur;
        private string _prenomjoueur;
        private string _pseudojoueur;
        private string _mdpjoueur;
        private string _datenaissance;
        private string _typejoueur;

        public int numjoueur
        {
            get { return _numjoueur; }
            set { _numjoueur = value; }
        }
        public string nomjoueur
        {
            get { return _nomjoueur; }
            set { _nomjoueur = value; }
        }

        public string prenomjoueur
        {
            get { return _prenomjoueur; }
            set { _prenomjoueur = value; }
        }
        public string pseudoJoueur
        {
            get { return _pseudojoueur; }
            set { _pseudojoueur = value; }
        }
        public string mdpJoueur
        {
            get { return _mdpjoueur; }
            set { _mdpjoueur = value; }
        }
        public string dateNaissance
        {
            get { return _datenaissance; }
            set { _datenaissance = value; }
        }

        public string typeJoueur
        {
            get { return _typejoueur; }
            set { _typejoueur = value; 

            
            }
        }
        public bool identifiants_valide(string pseudo, string mdp)
        {
            // Ici, vous pouvez implémenter la logique pour vérifier les identifiants du joueur
            // Par exemple, vous pouvez interroger la base de données pour vérifier si le pseudo et le mot de passe correspondent à un joueur existant
            // Retournez true si les identifiants sont valides, sinon false
            return pseudo == pseudoJoueur && mdp == mdpJoueur;
        }

        


        public event PropertyChangedEventHandler? PropertyChanged;
    }

    
}
