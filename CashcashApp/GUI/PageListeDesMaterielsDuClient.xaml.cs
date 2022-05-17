using System;
using System.Windows;
using System.Windows.Controls;

namespace CashcashApp
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class PageListeDesMaterielsDuClient : Page
    {
        MainWindow main;
        GestionMateriels gestion;
        Client client;
        public PageListeDesMaterielsDuClient(MainWindow main, GestionMateriels gestion, Client client)
        {
            InitializeComponent();
            this.main = main;
            this.gestion = gestion;
            this.client = client;

            tbTitre.Text += client.RaisonSociale;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var materiels = gestion.Donnees.ChargerLesMaterielsDuClient(client);
                dgMateriels.ItemsSource = materiels;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnContrat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var materiel = (Materiel)((Button)e.Source).DataContext;
                if (materiel.ContratId != null)
                {
                    PageDetailsDuContrat pageDetailsDuContrat = new(main, gestion, materiel);
                    pageDetailsDuContrat.Title += materiel.ContratId;
                    main.frame.Content = pageDetailsDuContrat;
                }
                else
                {
                    PageListeDesContratsDuClient pageListeDesContratsDuClient = new(main, gestion, materiel);
                    pageListeDesContratsDuClient.Title += materiel.ClientRaisonSociale;
                    main.frame.Content = pageListeDesContratsDuClient;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


    }
}