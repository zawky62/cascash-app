namespace CashcashApp
{
    public class Client
    {
        public int Id { get; }
        public string RaisonSociale { get; }
        public string Siren { get; }
        public string CodeApe { get; }
        public string Adresse { get; }
        public string CodePostal { get; }
        public string Ville { get; }
        public string Pays { get; }
        public string Telephone { get; }
        public string Email { get; }
        public float DistanceKm { get; }
        public float DureeDeplacementMinutes { get; }
        public string CodeAgence { get; }

        public Client(int id, string raisonSociale, string siren, string codeApe,
        string adresse, string codePostal, string ville, string pays, string telephone, string email,
        float distanceKm, float dureeDeplacementMinutes, string codeAgence) // TBD
        {
            Id = id;
            RaisonSociale = raisonSociale;
            Siren = siren;
            CodeApe = codeApe;
            Adresse = adresse;
            CodePostal = codePostal;
            Ville = ville;
            Pays = pays;
            Telephone = telephone;
            Email = email;
            DistanceKm = distanceKm;
            DureeDeplacementMinutes = dureeDeplacementMinutes;
            CodeAgence = codeAgence;
        }
    }
}