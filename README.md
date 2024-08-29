# Wild Code School - Checkpoint #2

## En résumé

L'objectif est de créer une librairie en ligne en utilisant ASP.NET Core MVC.
L'application permettra aux utilisateurs de naviguer, rechercher et acheter des
livres. Elle inclura également des fonctionnalités telles qu'un panier d'achat,
l'authentification des utilisateurs, l'historique des commandes, et un panneau
d'administration pour la gestion des stocks.

## Livrable Attendus

- Une application ASP.NET Core MVC entièrement fonctionnelle avec un design propre et réactif.
- Un code structuré correctement avec une séparation claire des préoccupations (modèles, vues, contrôleurs).
- Une base de données SQL Server intégrée avec Entity Framework Core pour le stockage des données.
- Une documentation comprenant des instructions d'installation, un guide utilisateur, et un guide administrateur.

## Installation

Pour installer la base de données initiale, exécutez les commandes suivantes dans la console de gestionnaire de packages NuGet :

- `add-migration InitDb`
- `update-database`

Ensuite, pour exécuter l'application, procédez comme suit dans le terminal :

1. Placez-vous dans le répertoire du projet.
2. Exécutez la commande `dotnet run`.
3. Ouvrez un navigateur internet et accédez à la page [https://localhost:5222/](https://localhost:5222/).

## Guide d'utilisation

### Liste des livres

- Vous pouvez parcourir la liste des livres présents dans l'application.
- Il est possible de voir les détails d'un livre en cliquant sur le bouton "Détails".
- Vous pouvez ajouter un livre au panier en cliquant sur le bouton "Ajouter au panier".

### Panier

- Le panier vous permet de commander les livres sélectionnés.
- Il est possible d'ajouter ou de supprimer des quantités.
- Vous pouvez supprimer complètement un livre du panier.
- Le montant total s'affiche en bas de la page.

## Guide d'administration

- En cours de développement
