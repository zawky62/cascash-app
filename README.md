Reste à faire :

   C# :
      - Tests unitaires
      - Documentation
   
   BDD :
      - Trigger contrat.id_client <=> client.id <=> materiel.id_client <=> materiel.id_contrat
      - Utilisateurs BDD

On abandonne PostgreSQL sur Linux, car c'est un travail monstrueux :
      - Créer une VM Linux, installer PostgreSQL dessus et accéder au port en localhost
      - Implémenter un nouveau connecteur en C# et en PHP
      - Réécrire tout le script SQL (création, insertion, triggers)
