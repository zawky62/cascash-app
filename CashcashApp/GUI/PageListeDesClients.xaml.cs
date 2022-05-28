using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Path = System.IO.Path;

namespace CashcashApp
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class PageListeDesClients : Page
    {
        MainWindow main;
        GestionMateriels gestion;
        public PageListeDesClients(MainWindow main, GestionMateriels gestion)
        {
            InitializeComponent();
            this.main = main;
            this.gestion = gestion;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // pour afficher une liste vide en cas de bug du chargement
            List<Client> clients = new();

            try
            {
                clients = gestion.Donnees.ChargerLesClients();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            dgClients.ItemsSource = clients;
        }

        private void btnRelance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = (Client)dgClients.SelectedItem;
                int delai = 30;
                string relance = $"http://127.0.0.1/cashcash-web/index.php/admin/pdf/relance?id={client.Id}&delai={delai}";

                var process = new ProcessStartInfo
                {
                    FileName = relance,
                    UseShellExecute = true
                };
                Process.Start(process);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void btnXML_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = (Client)dgClients.SelectedItem;

                string bureau = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string repertoire = Path.Combine(bureau, "Cashcash");
                if (!Directory.Exists(repertoire))
                {
                    Directory.CreateDirectory(repertoire);
                }

                string nomFichier = $"materielClient{client.Id}.xml";
                string emplacement = Path.Combine(repertoire, nomFichier);
                using (FileStream fs = File.Create(emplacement))
                {
                    byte[] xml = new UTF8Encoding(true).GetBytes(gestion.XmlClient(client.Id));
                    // Add some information to the file.
                    fs.Write(xml, 0, xml.Length);
                }

                //System.Diagnostics.Process.Start("explorer.exe", emplacement);
                MessageBox.Show(emplacement);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btnMat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Client client = (Client)((Button)e.Source).DataContext;
                PageListeDesMaterielsDuClient pageListeDesMaterielsDuClient = new(main, gestion, client);
                pageListeDesMaterielsDuClient.Title += client.RaisonSociale;
                main.frame.Content = pageListeDesMaterielsDuClient;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}