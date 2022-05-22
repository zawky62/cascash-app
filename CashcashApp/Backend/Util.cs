using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CashcashApp
{
    internal static class Util
    {
        public static DateTime CreerDate(string str)
        {
            string[] temp = str.Split(" ")[0].Split("/");
            if (temp.Length != 3)
            {
                throw new Exception("Date invalide");
            }
            DateTime date = new(Int32.Parse(temp[2]),
                                Int32.Parse(temp[1]),
                                Int32.Parse(temp[0]));
            return date;
        }

        // NouvelleConnexionBDD démarre une nouvelle connexion avec la BDD et renvoie l'objet GestionMateriel en cas de succès.
        // En cas d'échec, propose de réessayer ou quitter. Si l'utilisateur quitte, renvoie null et arrête le programme.
        public static PersistanceSQL NouvelleConnexionBDD(string bdd, string utilisateur, string mdp)
        {
            try
            {
                BDDConfig info = new(bdd, utilisateur, mdp);
                PersistanceSQL donnees = new(bdd, info); // exception si echec de connexion
                return donnees;
            }
            catch
            {
                throw;
            }
        }

        public static void AfficherErreurBDD(string bdd, Exception ex)
        {
            string message;
            if (bdd == "MySQL")
                switch (ex.Source)
                {
                    case "System.Private.CoreLib":
                        message = "Vérifiez que XAMPP, Apache et MySQL soient allumés.";
                        break;
                    case "MySql.Data":
                        message = "Utilisateur ou mot de passe incorrect.";
                        break;
                    default:
                        message = "Erreur critique";
                        break;
                }
            else
                message = "PostgreSQL n'est pas encore implémenté";

            string titre = "Impossible de se connecter";
            MessageBoxButton boutons = MessageBoxButton.OK;
            MessageBoxImage icone = MessageBoxImage.Error;
            MessageBox.Show(message, titre, boutons, icone);
        }
    }
}
