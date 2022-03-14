namespace CashcashApp
{
    public class GestionMateriels
    {
        private PersistanceSQL donnees; // Attribut qui permet de rendre les objets métiers accessibles.

        //Constructeur
        public GestionMateriels(PersistanceSQL lesDonnees)
        {
            // Construit un objet GestionMateriels avec un modèle de persistance associé.
            this.donnees = lesDonnees;
        }
        public Client getClient(int idClient)
        {
            // Retourne l'objet Distributeur qui possède l'identifiant idDistributeur passé en paramètre,
            // retourne null si aucun Distributeur ne possède cet identifiant.
            return new Client();
        }
        public string XmlClient(Client unClient)
        {
            // Retourne une chaîne de caractères qui représente le document XML de la liste des matériels
            // du client passé en paramètre comme le montre l'exemple de l'annexe.
            return "";
        }
        public static bool XmlClientValide(string xml)
        {
            // Retourne un booléen Vrai si le fichier xml respecte la DTD référencée dans le fichier XML, Faux sinon
            return false;
        }
    }
}