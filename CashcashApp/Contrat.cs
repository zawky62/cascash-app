using System;
using System.Collections.Generic;

namespace CashcashApp
{

    public class Contrat
    {
        private int id;
        private DateOnly dateSignature;
        private DateOnly dateRenouvellement;
        private List<Materiel> materielsAssures; // Tous les matériels sous contrat de maintenance

        public Contrat() // TBD
        {
            this.id = 3;
            this.dateSignature = new DateOnly();
            this.dateRenouvellement = new DateOnly(2022, 01, 05);
            this.materielsAssures = new List<Materiel> { };
        }
        public double GetJoursRestants() // TBD
        {
            // Renvoie le nombre de jours avant que le contrat arrive à échéance
            DateTime ajd = new DateTime();
            DateTime echeance = dateRenouvellement.AddYears(1).ToDateTime(new TimeOnly(0, 0, 0));

            var diff = echeance.Subtract(ajd).TotalDays;

            return diff;
        }

        public bool EstValide() // TBD
        {
            // indique si le contrat est valide (la date du jour est entre la date de signature et la date d’échéance)
            return false;
        }

        public void AjouteMateriel(Materiel unMateriel) // TBD
        {
            // ajoute unMatériel à la collection lesMaterielsAssures si la date de signature du contrat est
            // antérieure à la date d’installation du matériel
        }
    }
}