using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CashcashApp
{
    public class Client
    {
        private int id;
        private string raisonSociale;
        private string siren;
        private string codeApe;
        private string adresse;
        private string telClient;
        private string email;
        private int dureeDeplacement;
        private int distanceKm;
        private List<Materiel> materiels;  // Tous les matériels du client.
        private Contrat contrat;  // peut être nul si le client ne possède pas de contrat

        public Client()
        {
            this.id = 1;
            this.raisonSociale = "ab";
            this.siren = "ab";
            this.codeApe = "ab";
            this.adresse = "ab";
            this.telClient = "ab";
            this.email = "ab";
            this.dureeDeplacement = 0;
            this.distanceKm = 0;
            this.materiels = new List<Materiel> { new Materiel(), new Materiel() };
            this.contrat = new Contrat();
        } // TBD
        public string GetNom() => raisonSociale;
        public int GetId() => id;
        public List<Materiel> GetMateriels() => materiels;
        public List<Materiel> GetMaterielsSousContrat()
        {
            // Retourne l'ensemble des matériels pour lesquels le client a souscrit un contrat de maintenance qui
            // est encore valide (la date du jour est entre la date de signature et la date d’échéance)
            List<Materiel> temp = new();
            foreach (Materiel materiel in GetMateriels())
            {
                if (materiel.contrat.EstValide())
                {
                    temp.Add(materiel);
                }
            }
            return temp;
        } // TBD
        public List<Materiel> GetMaterielsHorsContrat()
        {
            // Retourne l'ensemble des matériels pour lesquels le client n'a pas souscrit un contrat de maintenance qui
            // est encore valide (la date du jour est entre la date de signature et la date d’échéance)
            List<Materiel> temp = new();
            foreach (Materiel materiel in GetMateriels())
            {
                if (!materiel.contrat.EstValide())
                {
                    temp.Add(materiel);
                }
            }
            return temp;
        } // TBD
        public bool EstAssure()
        {
            // Retourne vrai si le client est assuré, faux sinon
            foreach (Materiel materiel in GetMateriels())
            {
                if (materiel.contrat.EstValide())
                {
                    return true;
                }
            }
            return false;
        } // TBD

    }
}