using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashcashApp
{
    public class PersistanceSQL
    {
        public string bdd; // on souhaite se souvenir du type de bdd (mysql, postgres...) pour adapter les requêtes
        private MySqlConnection conn;

        // Constructeur
        public PersistanceSQL(string bdd, BDDConfig info) // TBD
        {
            // Construit un objet PersistanceSql. Cet objet permettra de charger les données depuis une base
            // de données ou de les sauvegarder dans la base.

            this.bdd = bdd;

            StringBuilder connString = new();
            connString.Append($"Server={info.Host};");
            connString.Append($"Database={info.NomBaseDonnees};");
            connString.Append($"Port={info.Port};");
            connString.Append($"User Id={info.Username};");
            connString.Append($"Password={info.Password};");
            conn = new(connString.ToString());
        }

        public void TesterConnexion()
        {
            try
            {
                conn.Open();
                conn.Close();
            }
            catch
            {
                throw;
            }
        }

        public void NouveauContrat(int idClient) // TBD 
        {

            try
            {
                conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"INSERT INTO contrat (date_signature, date_renouvellement, id_client)
                                    VALUES (@DateSign, @DateRenouv, @ClientID);";

                cmd.Parameters.AddWithValue("@DateSign", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateRenouv", DateTime.Now);
                cmd.Parameters.AddWithValue("@ClientID", idClient);

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void AffecterLeContratAuMateriel(string numSerie, Contrat contrat)
        {
            try
            {
                conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"UPDATE materiel
                                    SET id_contrat = @ContratID
                                    WHERE materiel.n_serie = @NumSerie;";

                cmd.Parameters.AddWithValue("@ContratID", contrat.Id);
                cmd.Parameters.AddWithValue("@NumSerie", numSerie);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Materiel> ChargerLesMaterielsDuClient(Client client) // liste des matériels d'un client 
        {
            List<Materiel> materiels = new();

            try
            {
                conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"SELECT client.id, client.raison_sociale, materiel.n_serie,
                                        materiel.date_vente, materiel.date_installation, type.libelle,
                                        contrat.id, contrat.date_signature, contrat.date_renouvellement
                                     FROM type, client, materiel LEFT JOIN contrat
                                     ON materiel.id_contrat = contrat.id
                                     WHERE materiel.id_client = @ClientID
                                     AND materiel.id_client = client.id
                                     AND materiel.reference = type.reference;";

                cmd.Parameters.AddWithValue("@ClientID", client.Id);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.FieldCount != 9)
                        throw new ArgumentOutOfRangeException("Un matériel n'est pas conforme (nombre d'arguments)");

                    string? clientId = reader[0].ToString();
                    string? clientRaisonSociale = reader[1].ToString();
                    string? matNumSerie = reader[2].ToString();
                    string? matDateVente = reader[3].ToString();
                    string? matDateInstall = reader[4].ToString();
                    string? matType = reader[5].ToString();
                    string? contratId = reader[6].ToString();
                    string? contratDateSign = reader[7].ToString();
                    string? contratDateRenouv = reader[8].ToString();

                    if (clientId == null || clientRaisonSociale == null || matNumSerie == null ||
                        matDateVente == null || matDateInstall == null || matType == null)
                        throw new ArgumentNullException("Un matériel n'est pas conforme (valeur nulle)");


                    Materiel materiel = new(clientId: Int32.Parse(clientId),
                                            clientRaisonSociale: clientRaisonSociale,
                                            numSerie: matNumSerie,
                                            dateVente: Util.CreerDate(matDateVente),
                                            dateInstall: Util.CreerDate(matDateInstall),
                                            typeLibelle: matType,
                                            contratId: contratId!.Length != 0 ? Int32.Parse(contratId) : null,
                                            contratDateSign: contratDateSign!.Length != 0 ? Util.CreerDate(contratDateSign) : null,
                                            contratDateRenouv: contratDateRenouv!.Length != 0 ? Util.CreerDate(contratDateRenouv) : null);
                    materiels.Add(materiel);
                }
                return materiels;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.GetType().Name} : {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

        }

        public List<Client> ChargerLesClients() // liste des clients 
        {
            List<Client> clients = new();

            try
            {
                conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = "select * from client;";

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.FieldCount != 13)
                    {
                        throw new ArgumentOutOfRangeException("Un client n'est pas conforme");
                    }
                    string? idClient = reader[0].ToString();
                    string? raisonSociale = reader[1].ToString();
                    string? siren = reader[2].ToString();
                    string? ape = reader[3].ToString();
                    string? adresse = reader[4].ToString();
                    string? codePostal = reader[5].ToString();
                    string? ville = reader[6].ToString();
                    string? pays = reader[7].ToString();
                    string? telephone = reader[8].ToString();
                    string? email = reader[9].ToString();
                    string? distanceKm = reader[10].ToString();
                    string? dureeDeplacement = reader[11].ToString();
                    string? codeAgence = reader[12].ToString();

                    if (idClient == null || raisonSociale == null || siren == null || ape == null || adresse == null ||
                        codePostal == null || ville == null || pays == null || telephone == null ||
                        email == null || distanceKm == null || dureeDeplacement == null || codeAgence == null)
                    {
                        throw new ArgumentNullException("Un client n'est pas conforme (valeur nulle)");
                    }

                    Client temp = new(Int32.Parse(idClient),
                        raisonSociale,
                        siren,
                        ape,
                        adresse,
                        codePostal,
                        ville,
                        pays,
                        telephone,
                        email,
                        float.Parse(distanceKm),
                        float.Parse(dureeDeplacement),
                        codeAgence);

                    clients.Add(temp);

                }
                return clients;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public Contrat? ChargerLeContrat(int id) // le contrat correspondant à l'id 
        {
            Contrat contrat;
            try
            {
                conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"SELECT *
                                 FROM contrat
                                 WHERE id = @ContratID;";

                cmd.Parameters.AddWithValue("@ClientID", id);

                using var reader = cmd.ExecuteReader();
                reader.Read();

                if (reader.FieldCount != 4)
                    throw new ArgumentOutOfRangeException("Un contrat n'est pas conforme (nombre d'arguments)");

                string? contratId = reader[0].ToString();
                string? contratDateSign = reader[1].ToString();
                string? contratDateRenou = reader[2].ToString();
                string? clientId = reader[3].ToString();


                if (contratId == null || contratDateSign == null || contratDateRenou == null || clientId == null)
                    throw new ArgumentNullException("Un matériel n'est pas conforme (valeur nulle)");


                contrat = new(Int32.Parse(contratId),
                                Util.CreerDate(contratDateSign),
                                Util.CreerDate(contratDateRenou),
                                Int32.Parse(clientId));
                return contrat;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Contrat> ChargerLesContratsDuClient(int idClient) // liste des contrats d'un client 
        {
            List<Contrat> contrats = new();

            try
            {
                conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"SELECT *
                                 FROM contrat
                                 WHERE contrat.id_client = @ClientID;";

                cmd.Parameters.AddWithValue("@ClientID", idClient);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.FieldCount != 4)
                        throw new ArgumentOutOfRangeException("Un contrat n'est pas conforme (nombre d'arguments)");

                    string? contratId = reader[0].ToString();
                    string? contratDateSign = reader[1].ToString();
                    string? contratDateRenouv = reader[2].ToString();
                    string? clientId = reader[3].ToString();


                    if (contratId == null || contratDateSign == null || contratDateRenouv == null || clientId == null)
                        throw new ArgumentNullException("Un matériel n'est pas conforme (valeur nulle)");


                    Contrat contrat = new(Int32.Parse(contratId),
                                          Util.CreerDate(contratDateSign),
                                          Util.CreerDate(contratDateRenouv),
                                          Int32.Parse(clientId));

                    contrats.Add(contrat);
                }
                return contrats;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        public List<Materiel> ChargerLesMaterielsDuContrat(int idContrat) // liste des matériels affectés à un contrat 
        {
            List<Materiel> materiels = new();

            try
            {
                conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"SELECT n_serie, date_vente, date_installation, prix_vente, emplacement, libelle
                                 FROM materiel, type
                                 WHERE materiel.reference = type.reference
                                 AND materiel.id_contrat = @ContratID;";

                cmd.Parameters.AddWithValue("@ContratID", idContrat);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.FieldCount != 6)
                        throw new ArgumentOutOfRangeException("Un contrat n'est pas conforme (nombre d'arguments)");

                    string? n_serie = reader[0].ToString();
                    string? date_vente = reader[1].ToString();
                    string? date_installation = reader[2].ToString();
                    string? prix_vente = reader[3].ToString();
                    string? emplacement = reader[4].ToString();
                    string? libelle = reader[5].ToString();


                    if (n_serie == null || date_vente == null || date_installation == null ||
                        prix_vente == null || emplacement == null || libelle == null)
                        throw new ArgumentNullException("Un matériel n'est pas conforme (valeur nulle)");

                    Materiel materiel = new(numSerie: n_serie,
                                            dateVente: Util.CreerDate(date_vente),
                                            dateInstall: Util.CreerDate(date_installation),
                                            prixVente: float.Parse(prix_vente),
                                            emplacement: emplacement,
                                            typeLibelle: libelle);

                    materiels.Add(materiel);
                }
                return materiels;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        public string ChargerLesMaterielsEnXML(int idClient)
        {
            // les matériels sous contrat doivent être apparaitre en premier dans le xml
            Stack<Materiel> horsContrat = new();

            try
            {
                conn.Open();

                #region requête sql
                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"SELECT materiel.n_serie, type.reference, type.libelle, materiel.date_vente,
                                        materiel.date_installation, materiel.prix_vente, materiel.emplacement,
                                        contrat.date_renouvellement
                                     FROM type, materiel LEFT JOIN contrat ON materiel.id_contrat = contrat.id
                                     WHERE materiel.reference = type.reference
                                     AND materiel.id_client = @ClientID;";

                cmd.Parameters.AddWithValue("@ClientID", idClient);
                using var reponseSQL = cmd.ExecuteReader();
                #endregion

                StringBuilder xmlMateriels = new("<sousContrat>");
                while (reponseSQL.Read())
                {
                    #region traitement de la ligne sql
                    if (reponseSQL.FieldCount != 8)
                        throw new ArgumentOutOfRangeException("Une ligne n'est pas conforme (nombre d'arguments)");

                    string? n_serie = reponseSQL[0].ToString();
                    string? reference = reponseSQL[1].ToString();
                    string? libelle = reponseSQL[2].ToString();
                    string? date_vente = reponseSQL[3].ToString();
                    string? date_installation = reponseSQL[4].ToString();
                    string? prix_vente = reponseSQL[5].ToString();
                    string? emplacement = reponseSQL[6].ToString();
                    string? date_renouvellement = reponseSQL[7].ToString();

                    if (n_serie == null || reference == null || libelle == null || date_vente == null ||
                        date_installation == null || prix_vente == null || emplacement == null || date_renouvellement == null)
                        throw new ArgumentNullException("Une ligne n'est pas conforme (valeur nulle)");
                    #endregion

                    #region instancier le matériel
                    Materiel materiel = new(numSerie: n_serie,
                                               typeReference: reference,
                                               typeLibelle: libelle,
                                               dateVente: Util.CreerDate(date_vente),
                                               dateInstall: Util.CreerDate(date_installation),
                                               prixVente: float.Parse(prix_vente),
                                               emplacement: emplacement,
                                               contratDateRenouv: date_renouvellement!.Length != 0 ? Util.CreerDate(date_renouvellement) : null);
                    #endregion

                    if (materiel.JoursRestants > 0)
                    {
                        // si le matériel est sous contrat, l'ajouter au xml
                        xmlMateriels.Append(materiel.XmlMateriel());
                    }
                    else
                    {
                        // sinon l'ajouter à la pile des matériels hors contrat
                        horsContrat.Push(materiel);
                    }
                }
                xmlMateriels.Append("</sousContrat>");

                // traiter la pile des matériels hors contrat
                xmlMateriels.Append("<horsContrat>");
                while (horsContrat.Count > 0)
                {
                    xmlMateriels.Append(horsContrat.Pop().XmlMateriel());
                }
                xmlMateriels.Append("</horsContrat>");

                return xmlMateriels.ToString();
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }

}
