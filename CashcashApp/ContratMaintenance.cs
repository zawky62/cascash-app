using System;
using System.Collections.Generic;

namespace CashcashApp
{

    public class ContratMaintenance
    {
        private string numContrat;
        private DateOnly dateSignature;
        private DateOnly dateEcheance;
        private List<Materiel> lesMaterielsAssures; // Tous les matériels sous contrat de maintenance

        public ContratMaintenance()
        {
            this.numContrat = "";
            this.dateSignature=new DateOnly();
            this.dateEcheance = new DateOnly();
            this.lesMaterielsAssures = new List<Materiel> { };
        }
        public int getJoursRestants()
        {
            // Renvoie le nombre de jours avant que le contrat arrive à échéance
            return 0;
        }

        public bool estValide()
        {
            // indique si le contrat est valide (la date du jour est entre la date de signature et la date d’échéance)
            return false;
        }

        public void ajouteMateriel(Materiel unMateriel)
        {
            // ajoute unMatériel à la collection lesMaterielsAssures si la date de signature du contrat est
            // antérieure à la date d’installation du matériel
        }
    }
}