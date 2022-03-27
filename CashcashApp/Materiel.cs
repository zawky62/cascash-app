using System;

namespace CashcashApp
{
    public class Materiel
    {
        public int numSerie;
        public DateOnly dateVente;
        public DateOnly dateInstallation;
        public float prixVente;
        public string emplacement;
        public Contrat contrat;
        public Type type;

        public Materiel() // TBD
        {
            this.numSerie = 0;
            this.dateVente = new DateOnly();
            this.dateInstallation = new DateOnly();
            this.prixVente = 0;
            this.emplacement = "";
            this.contrat = new Contrat();
            this.type = new Type();
        }
        public string XmlMateriel() // TBD
        {
            // Retourne la chaîne correspondant au code XML représentant le matériel (voir annexe).
            string temp = $"<materiel numSerie=\"{numSerie}\">" +
                            $"<type reference=\"{type.reference}\" libelle=\"{type.libelle}\"/>" +
                            $"<date_vente>{dateVente}</date_vente>" +
                            $"<date_installation>{dateInstallation}</date_installation>" +
                            $"<prix_vente>{prixVente}</prix_vente>" +
                            $"<emplacement>\"{emplacement}\"</emplacement>" +
                            $"<nbJourAvantEcheance>{contrat.GetJoursRestants()}</nbJourAvantEcheance>" +
                          "</materiel>";
            return temp;
        }
    }
}