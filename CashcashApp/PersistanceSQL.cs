namespace CashcashApp
{
    public class PersistanceSQL
    {
        // Constructeur
        PersistanceSQL(string ipBase, int port, string nomBaseDonnee)
        {
            // Construit un objet PersistanceSql. Cet objet permettra de charger les données depuis une base
            // de données ou deF les sauvegarder dans la base.
            ipBase = "";
            port = 0;
            nomBaseDonnee = "";
        }

        public void RangerDansBase(object unObjet)
        {
            // Stocke les données de l'objet dans la base de données.
        }

        public object ChargerDepuisBase(string id, string nomClasse)
        {
            // Retourne l’objet de la classe NomClasse dont l’identifiant est "id". Cet objet est chargé
            // depuis la base de données, ainsi que l’ensemble de ses objets liés (voir l’exemple d’utilisation
            // ci-dessous). Retourne NULL si aucun objet de cette classe ne possède cet identifiant.
            return new object();
        }
    }
}