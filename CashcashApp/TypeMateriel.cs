namespace CashcashApp
{
    public class TypeMateriel
    {
        private string referenceInterne;
        private string libelleTypeMateriel;
        private Famille laFamille;

        public TypeMateriel()
        {
            this.referenceInterne = "";
            this.libelleTypeMateriel = "";
            this.laFamille = new Famille();
        }
    }
}