using System;

namespace CashcashApp
{
    public class Materiel
    {
        private int numSerie;
        private DateOnly dateVente;
        private float prixVente;
        private string emplacement;
        private TypeMateriel leType;

        public Materiel()
        {
            this.numSerie = 0;
            this.dateVente = new DateOnly();
            this.prixVente = 0;
            this.emplacement = "";
            this.leType = new TypeMateriel();
        }
        public string xmlMateriel()
        {
            // Retourne la chaîne correspondant au code XML représentant le matériel (voir annexe).
            return "";
        }
    }
}