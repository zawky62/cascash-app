using System;
using System.Text;

namespace CashcashApp
{
    public class Materiel
    {
        // Materiel
        public string NumSerie { get; }
        public DateTime? DateVente { get; }
        public DateTime? DateInstall { get; }
        public float? PrixVente { get; }
        public string? Emplacement { get; }

        // Type
        public string? TypeReference { get; }
        public string? TypeLibelle { get; }

        // Client
        public int? ClientId { get; }
        public string? ClientRaisonSociale { get; }

        // Contrat
        public int? ContratId { get; }
        public DateTime? ContratDateSign { get; }
        public DateTime? ContratDateRenouv { get; }

        public Materiel(string numSerie, DateTime? dateVente = null, DateTime? dateInstall = null, float? prixVente = null,
            string? emplacement = null, string? typeReference = null, string? typeLibelle = null, int? clientId = null,
            string? clientRaisonSociale = null, int? contratId = null, DateTime? contratDateSign = null, DateTime? contratDateRenouv = null)
        {
            NumSerie = numSerie;
            DateVente = dateVente;
            DateInstall = dateInstall;
            PrixVente = prixVente;
            Emplacement = emplacement;
            TypeReference = typeReference;
            TypeLibelle = typeLibelle;
            ClientId = clientId;
            ClientRaisonSociale = clientRaisonSociale;
            ContratId = contratId;
            ContratDateSign = contratDateSign;
            ContratDateRenouv = contratDateRenouv;
        }

        public int JoursRestants()
        {
            if (ContratDateRenouv == null)
                return 0;
            DateTime echeance = ContratDateRenouv.Value.AddYears(1);
            return (int)echeance.Subtract(DateTime.Now).TotalDays;
        }

        public string XmlMateriel()
        {
            StringBuilder xml = new();

            xml.Append($"<materiel numSerie=\"{NumSerie}\">");
            xml.Append($"<type reference=\"{TypeReference}\" libelle=\"{TypeLibelle}\"/>");
            xml.Append($"<date_vente>{DateVente:dd/MM/yyyy}</date_vente>");
            xml.Append($"<date_installation>{DateInstall:dd/MM/yyyy}</date_installation>");
            xml.Append($"<prix_vente>{PrixVente}</prix_vente>");
            xml.Append($"<emplacement>\"{Emplacement}\"</emplacement>");

            int joursRestants = JoursRestants();
            if (joursRestants > 0)
            {
                xml.Append($"<nbJourAvantEcheance>{joursRestants}</nbJourAvantEcheance>");
            }

            xml.Append("</materiel>");

            return xml.ToString();
        }
    }
}