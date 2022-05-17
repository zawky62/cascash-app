using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CashcashApp
{
    public partial class PageListeDesContratsDuClient : Page
    {
        MainWindow main;
        GestionMateriels gestion;
        Materiel materiel;
        public PageListeDesContratsDuClient(MainWindow main, GestionMateriels gestion, Materiel materiel)
        {
            InitializeComponent();
            this.main = main;
            this.gestion = gestion;
            this.materiel = materiel;

            tbTitre.Text += $"{materiel.NumSerie}\n({materiel.TypeLibelle})";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AfficherLesContratsDuClient(materiel.ClientId!.Value);
        }

        private void btnRafraichir_Click(object sender, RoutedEventArgs e)
        {
            var index = dgContrats.SelectedIndex;
            RafraichirContrats();
            RafraichirMateriels();
            dgContrats.SelectedIndex = index;
        }

        #region partie contrats
        private void btnNouveau_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gestion.Donnees.NouveauContrat(materiel.ClientId!.Value);
                RafraichirContrats();

                // selectionner le contrat venant d'être ajouté
                dgContrats.SelectedIndex = dgContrats.Items.Count - 1;
            }
            catch
            {
                MessageBox.Show("Erreur lors de la création du nouveau contrat");
            }
        }

        private void AfficherLesContratsDuClient(int idClient)
        {
            List<Contrat> contrats = new();
            try
            {
                contrats = gestion.Donnees.ChargerLesContratsDuClient(idClient);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgContrats.ItemsSource = contrats;
        }

        public void RafraichirContrats()
        {
            AfficherLesContratsDuClient(materiel.ClientId!.Value);
        }
        #endregion

        #region partie matériels
        private void btnAffecter_Click(object sender, RoutedEventArgs e)
        {
            if (dgContrats.SelectedItem == null)
            {
                MessageBox.Show("Selectionnez un contrat");
                return;
            }

            string numSerie = materiel.NumSerie;
            var contrat = (Contrat)dgContrats.SelectedItem;

            gestion.Donnees.AffecterLeContratAuMateriel(numSerie, contrat);

            RafraichirMateriels();
        }
        public void RafraichirMateriels()
        {
            if (dgContrats.SelectedItem == null)
                return;

            var contrat = (Contrat)dgContrats.SelectedItem;
            AfficherLesMaterielsDuContrat(contrat.Id);
        }

        private void AfficherLesMaterielsDuContrat(int idContrat)
        {
            List<Materiel> materiels = gestion.Donnees.ChargerLesMaterielsDuContrat(idContrat);
            dgMateriels.ItemsSource = materiels;
        }
        #endregion

        private void dgContrats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RafraichirMateriels();
        }
    }
}
