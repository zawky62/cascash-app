using System;

namespace CashcashApp
{
    /* BDDConfig contient les adresses de connexion aux serveurs de bdd
    Ce choix a été fait afin de ne pas publier les adresses sur github
    En effet, cette classe est incluse dans le fichier .gitignore */
    public class BDDConfig
    {
        public string BDD { get; }
        public string Host { get; }
        public string NomBaseDonnees { get; }
        public int Port { get; }
        public string Username { get; }
        public string Password { get; }
        public BDDConfig(string bdd)
        {
            BDD = bdd;
            Host = "localhost";
            NomBaseDonnees = "cashcash";

            switch (bdd)
            {
                case "MySQL":
                    Port = 3306;
                    Username = "root";
                    Password = "";
                    break;
                case "PostgreSQL":
                    Port = 5432;
                    Username = "postgres";
                    Password = "";
                    break;
                default:
                    Port = 0;
                    Username = "";
                    Password = "";
                    break;
            }
        }
    }
}