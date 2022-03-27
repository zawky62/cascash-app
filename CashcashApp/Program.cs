using System;
using System.Xml;
using System.Xml.Schema;

namespace CashcashApp
{
    public class Program
    {
        private static Connexion? conn;
        private static bool xmlIsValid;

        public Client? GetClientById(int idClient) // TBD
        {
            // Retourne l'objet Client qui possède l'identifiant idClient passé en paramètre,
            // retourne null si aucun Client ne possède cet identifiant.
            return null;
        }
        public string XmlClient(Client client) // TBD
        {
            // Retourne une chaîne de caractères qui représente le document XML de la liste des matériels
            // du client passé en paramètre comme le montre l'exemple de l'annexe.

            // Début du fichier
            string xml = "<? xml version=\"1.0\" encoding=\"UTF-8\" ?>";
            xml += GetDTD();
            xml += "<listeMateriel>";
            xml += $"<materiels idClient=\"{client.GetId()}\">";

            // Matériels sous contrat
            xml += "<sousContrat>";
            foreach (var materiel in client.GetMaterielsSousContrat())
            {
                xml += materiel.XmlMateriel();
            }
            xml += "</sousContrat>";

            // Matériels hors contrat
            xml += "<horsContrat>";
            foreach (var materiel in client.GetMaterielsHorsContrat())
            {
                xml += materiel.XmlMateriel();
            }
            xml += "</horsContrat>";

            return xml;
        }
        public static bool XmlClientValide(string xml) // TBD
        {
            // Retourne un booléen Vrai si le fichier xml respecte la DTD référencée dans le fichier XML, Faux sinon

            xmlIsValid = true;

            // Set the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationType = ValidationType.DTD;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

            // Create the XmlReader object.
            XmlReader reader = XmlReader.Create(xml, settings);

            // Parse the file.
            while (reader.Read()) ;

            return xmlIsValid;
        }

        private static void ValidationCallBack(object? sender, ValidationEventArgs e)
        {
            xmlIsValid = false;
        }

        private static string GetDTD() // TBD
        {
            return @"<!DOCTYPE listeMateriel [
            <!ELEMENT listeMateriel (materiels)>
            <!ELEMENT materiels (sousContrat?, horsContrat?)>
            <!ATTLIST materiels idClient CDATA #REQUIRED>
            <!ELEMENT sousContrat (materiel)>
            <!ELEMENT horsCont rat (materiel)>
            <!ELEMENT materiel (type, date_vente, date_installation, prix_vente?, emplacement, nbJourAvantEcheance?)>
            <!ATTLIST materiel numSerie CDATA #REQUIRED>
            <!ELEMENT type (#PCDATA)>
            <!ELEMENT date_vente (#PCDATA)>
            <!ELEMENT date_installation (#PCDATA)>
            <!ELEMENT prix_vente (#PCDATA)>
            <!ELEMENT emplacement (#PCDATA)>
            <!ELEMENT nbJourAvantEcheance (#PCDATA)>
            ]>";
        }
    }
}