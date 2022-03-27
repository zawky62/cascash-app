namespace CashcashApp
{
    public class Connexion
    {
        // Constructeur
        Connexion(string ipBase, int port, string nomBaseDonnee) // TBD
        {
            // Construit un objet PersistanceSql. Cet objet permettra de charger les données depuis une base
            // de données ou deF les sauvegarder dans la base.
            ipBase = "";
            port = 0;
            nomBaseDonnee = "";
        }

        public void RangerDansBase(object obj) // TBD
        {
            // Stocke les données de l'objet dans la base de données.

            // switch (obj.GetType().Name)
            // {
            //     case "Client":
            //     case "Contrat":
            //     case "Materiel":
            //     case ...
            // }

        }

        public object? ChargerDepuisBase(string id, string nomClasse) // TBD
        {
            // Retourne l’objet de la classe NomClasse dont l’identifiant est "id". Cet objet est chargé
            // depuis la base de données, ainsi que l’ensemble de ses objets liés (voir l’exemple d’utilisation
            // ci-dessous). Retourne NULL si aucun objet de cette classe ne possède cet identifiant.
            return null;
        }
    }
}