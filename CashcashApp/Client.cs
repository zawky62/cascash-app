using System.Collections.Generic;

namespace CashcashApp
{

    public class Client
    {
        private string numClient;
        private string raisonSociale;
        private string siren;
        private string codeApe;
        private string adresse;
        private string telClient;
        private string email;
        private int dureeDeplacement;
        private int distanceKm;
        private List<Materiel> lesMateriels;  // Tous les matériels du client.
        private ContratMaintenance leContrat;  // peut être nul si le client ne possède pas de contrat

        public Client()
        {
            this.numClient="";
            this.raisonSociale="";
            this.siren="";
            this.codeApe="";
            this.adresse="";
            this.telClient="";
            this.email = "";
            this.dureeDeplacement=0;
            this.distanceKm=0;
            this.lesMateriels=new List<Materiel> { };
            this.leContrat=new ContratMaintenance();


        }
        public List<Materiel> getMateriels()
        {
            // Retourne l'ensemble des matériels du client
            return lesMateriels;
        }

        public List<Materiel> getMaterielsSousContrat()
        {
            // Retourne l'ensemble des matériels pour lesquels le client a souscrit un contrat de maintenance qui
            // est encore valide (la date du jour est entre la date de signature et la date d’échéance)
            return lesMateriels;
        }

        public bool estAssure()
        {
            return false;
            // Retourne vrai si le client est assuré, faux sinon
        }

    }
}