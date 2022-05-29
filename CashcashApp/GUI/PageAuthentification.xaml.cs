using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CashcashApp
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class PageAuthentification : Page
    {
        MainWindow main;
        public PageAuthentification(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
            connMessage.Visibility = Visibility.Hidden;

            cbBdd.Items.Add("MySQL");
            cbBdd.Items.Add("PostgreSQL");
            cbBdd.SelectedIndex = 0;
        }

        // Empêche de vider le choix de BDD
        private void cbBdd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbBdd.SelectedIndex == -1)
            {
                cbBdd.SelectedIndex = 0;
            }
        }

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void btnConnexion_ClickAsync(object sender, RoutedEventArgs e)
        {
            await AfficherConnexionAsync();

            string bdd = cbBdd.SelectedItem.ToString()!; // ! spécifie que le résultat ne sera pas null
            try
            {
                var persistance = Util.NouvelleConnexionBDD(bdd, tbUtilisateur.Text, tbMdp.Password);
                persistance.TesterConnexion();
                //MessageBox.Show("Connexion réussie");
                GestionMateriels gestion = new(persistance);
                PageListeDesClients pageListeDesClients = new(main, gestion);
                main.frame.Content = pageListeDesClients;
            }
            catch (Exception ex)
            {
                Util.AfficherErreurBDD(bdd, ex);
            }
            finally
            {
                connMessage.Visibility = Visibility.Hidden;
            }
        }

        private async Task AfficherConnexionAsync()
        {
            connMessage.Visibility = Visibility.Visible;
            await Task.Delay(1); // 1ms
        }

    }
}
