using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Schema;

namespace CashcashApp
{
    public class GestionMateriels
    {
        public PersistanceSQL Donnees { get; }  // Attribut qui permet de rendre les objets métiers accessibles.
        public bool XmlIsValid { get; private set; }
        public GestionMateriels(PersistanceSQL donnees)
        {
            // Construit un objet GestionMateriels avec un modèle de persistance associé.
            Donnees = donnees;
        }

        public string XmlClient(int idClient) // TBD
        {
            // Retourne une chaîne de caractères qui représente le document XML de la liste des matériels du client passé en paramètre

            try
            {
                StringBuilder xmlBuilder = new($"<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
                xmlBuilder.Append(DTD());
                xmlBuilder.Append($"<listeMateriel><materiels idClient=\"{idClient}\">");
                xmlBuilder.Append(Donnees.ChargerLesMaterielsEnXML(idClient));
                xmlBuilder.Append("</materiels></listeMateriel>");

                string xml = xmlBuilder.ToString();

                if (XmlClientValide(xml))
                {
                    return xml;
                }
                else
                {
                    throw new Exception("Le XML généré n'est pas valide");
                }
            }
            catch
            {
                throw;
            }
        }


        private bool XmlClientValide(string xml) // TBD
        {
            try
            {
                XmlIsValid = true;

                string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string repertoire = Path.Combine(appdata, "Cashcash");
                if (!Directory.Exists(repertoire))
                {
                    Directory.CreateDirectory(repertoire);
                }
                string emplacement = Path.Combine(repertoire, "cashcash-temp.xml");

                using (FileStream fs = File.Create(emplacement))
                {
                    byte[] bytes = new UTF8Encoding(true).GetBytes(xml);
                    // Add some information to the file.
                    fs.Write(bytes, 0, bytes.Length);
                }

                // Set the validation settings.
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Parse;
                settings.ValidationType = ValidationType.DTD;
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

                // Create the XmlReader object.
                using (XmlReader reader = XmlReader.Create(emplacement, settings))
                {
                    while (reader.Read()) ;
                }

                return XmlIsValid;
            }
            catch
            {
                throw;
            }
        }

        private void ValidationCallBack(object? sender, ValidationEventArgs e)
        {
            XmlIsValid = false;
            MessageBox.Show(e.Message);
        }

        private string DTD() // TBD
        {
            return @"<!DOCTYPE listeMateriel [
            <!ELEMENT listeMateriel (materiels)>
            <!ELEMENT materiels (sousContrat?, horsContrat?)>
            <!ATTLIST materiels idClient CDATA #REQUIRED>
            <!ELEMENT sousContrat (materiel*)>
            <!ELEMENT horsContrat (materiel*)>
            <!ELEMENT materiel (type, date_vente, date_installation, prix_vente?, emplacement, nbJourAvantEcheance?)>
            <!ATTLIST materiel numSerie CDATA #REQUIRED>
            <!ELEMENT type (#PCDATA)>
            <!ATTLIST type reference CDATA #REQUIRED
                           libelle CDATA #REQUIRED>
            <!ELEMENT date_vente (#PCDATA)>
            <!ELEMENT date_installation (#PCDATA)>
            <!ELEMENT prix_vente (#PCDATA)>
            <!ELEMENT emplacement (#PCDATA)>
            <!ELEMENT nbJourAvantEcheance (#PCDATA)>
            ]>";
        }
    }
}