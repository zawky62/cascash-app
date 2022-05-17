using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CashcashApp
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class PageDetailsDuContrat : Page
    {
        MainWindow main;
        GestionMateriels gestion;
        Materiel materiel;
        public PageDetailsDuContrat(MainWindow main, GestionMateriels gestion, Materiel materiel)
        {
            InitializeComponent();
            this.main = main;
            this.gestion = gestion;
            this.materiel = materiel;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (materiel.ContratId == null)
                throw new ArgumentNullException("Pas de contrat valide");

            if (materiel.ContratDateSign == null || materiel.ContratDateRenouv == null)
                throw new ArgumentNullException("Une date n'est pas renseignée dans le contrat");

            if (materiel.ClientRaisonSociale == null)
                throw new ArgumentNullException("Le client n'est pas valide");

            tbClient.Text = $"{materiel.ClientRaisonSociale}";
            tbContratId.Text = $"{materiel.ContratId.Value}";
            tbDateSign.Text = $"{materiel.ContratDateSign.Value:dd/MM/yyyy}";
            tbDateRenouv.Text = $"{materiel.ContratDateRenouv.Value:dd/MM/yyyy}";

            List<Materiel> materiels = new();
            try
            {
                materiels = gestion.Donnees.ChargerLesMaterielsDuContrat(materiel.ContratId.Value);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            dgMateriels.ItemsSource = materiels;

        }

        private void btnAffecter_Click(object sender, RoutedEventArgs e)
        {
            PageListeDesContratsDuClient pageListeDesContratsDuClient = new(main, gestion, materiel);
            pageListeDesContratsDuClient.Title += materiel.ClientRaisonSociale;
            main.frame.Content = pageListeDesContratsDuClient;
        }
    }
}