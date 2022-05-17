//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using CashcashApp;

//namespace CashcashApp.Tests
//{
//    [TestClass]
//    public class ContratTests
//    {
//        [TestMethod]
        
            
//        Client client = new(0, "Test SARL", "123456789", "A1234", "1 rue Test",
//        "01234", "Testville", "0123456789", "test@test.com", 35f, 30f, "LILLE1");
//        DateTime ajd = new DateTime();
        
//        public void TestEstValide_AvantPeriode_DoitRenvoyerFaux()
//        {
//            // Le contrat sera effectif dans 3 jours
//            Contrat contrat = new(0, new(ajd.AddDays(3)), new(ajd.AddDays(3)), client);
//            Assert.IsFalse(contrat.EstValide());
//        }
//        public void TestEstValide_SigneLeJourMeme_DoitRenvoyerVrai()
//        {
//            // Le contrat a été créé à l'instant
//            Contrat contrat = new(0, new(ajd), new(ajd), client);
//            Assert.IsFalse(contrat.EstValide());
//        }
//        public void TestEstValide_MilieuPeriodeValidite_DoitRenvoyerVrai()
//        {

//        }
//        public void TestEstValide_ExpireLeJourMeme_DoitRenvoyerFaux()
//        {

//        }
//        public void TestEstValide_ApresPeriode_DoitRenvoyerFaux()
//        {

//        }
//    }
//}