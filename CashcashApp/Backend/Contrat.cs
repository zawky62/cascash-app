using System;

namespace CashcashApp
{

    public class Contrat
    {
        public int Id { get; }
        public int IdClient { get; }
        public DateTime DateSignature { get; }
        public DateTime DateRenouvellement { get; }

        public Contrat(int id, DateTime dateSignature, DateTime dateRenouvellement, int idClient) // TBD
        {
                Id = id;
                DateSignature = dateSignature;
                DateRenouvellement = dateRenouvellement;
                IdClient = idClient;
        }
    }
}