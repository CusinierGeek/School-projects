using KanbanDonnees.Entities;

namespace KanbanDonnees.DAO.EnMemoire;

internal class DonneesEnMemoire
{
    internal class CarteUtilisateur
    {
        private int _utilisateurId;
        private int _carteId;

        public CarteUtilisateur(int utilisateurId, int carteId)
        {
            _utilisateurId = utilisateurId;
            _carteId = carteId;
        }

        public int UtilisateurId
        {
            get => _utilisateurId;
            set => _utilisateurId = value;
        }

        public int CarteId
        {
            get => _carteId;
            set => _carteId = value;
        }
    }


    internal static readonly List<Utilisateur> Utilisateurs = new List<Utilisateur>()
    {
        new Utilisateur(1, "Quentin Amar"),
        new Utilisateur(2, "Yves Atrovite"),
        new Utilisateur(3, "Jean-Marc Desbutes"),
        new Utilisateur(4, "Tex Agère"),
        new Utilisateur(5, "Sarah Vigote"),
    };

    internal static readonly List<Carte> Cartes = new List<Carte>()
    {
        new Carte(1, "Tests", "Effectuer les tests des cas d'utilisation", null, 0, 1),
        new Carte(2, "CU1", "Cas d'utilisation : passer une commande", DateTime.Parse("2023-02-28"), 1, 2),
        new Carte(3, "Design", "Html/css du site web", DateTime.Parse("2023-02-06"), 2, 2),
        new Carte(4, "CU2", "Cas d'utilisation : inscription au site", DateTime.Parse("2023-02-10"), 3, 2),
        new Carte(5, "CU3", "Cas d'utilisation : ajouter/modifier/supprimer des items au panier",
            DateTime.Parse("2023-02-15"), 4, 2),
        new Carte(6, "BD", "Création de la base de données", DateTime.Parse("2023-01-31"), 5, 3),
        new Carte(7, "Devis", "Faire le devis du projet", DateTime.Parse("2023-01-01"), 6, 4),
        new Carte(8, "Prototype", "Prototype à montrer au client", DateTime.Parse("2023-01-02"), 7, 4),
        new Carte(9, "Boîtes", "Faire les boîtes", DateTime.Parse("2023-06-30"), 0, 5),
        new Carte(10, "Aide", "Acheter de l'eau et un casse-croûte pour les déménageurs", DateTime.Parse("2023-07-01"),
            1, 6),
        new Carte(11, "Ménage", "Faire le ménage de l'ancien bureau après le départ des déménageurs",
            DateTime.Parse("2023-07-01"), 2, 6),
        new Carte(12, "Adresse internet", "Faire le changement d''adresse auprès de la compagnie d''internet.",
            DateTime.Parse("2023-07-07"), 3, 7),
        new Carte(13, "Adresse Hydro", "Faire le changement d'adresse auprès d'Hydro.", DateTime.Parse("2023-07-07"), 4,
            7),
        new Carte(14, "Réservation", "Réserver un déménageur.", DateTime.Parse("2023-06-01"), 5, 8),
    };

    internal static readonly List<CarteUtilisateur> CarteUtilisateurs = new List<CarteUtilisateur>()
    {
        new CarteUtilisateur(1, 3),
        new CarteUtilisateur(2, 4),
        new CarteUtilisateur(3, 4),
        new CarteUtilisateur(3, 5),
        new CarteUtilisateur(1, 6),
        new CarteUtilisateur(3, 6),
        new CarteUtilisateur(5, 7),
        new CarteUtilisateur(2, 8),
        new CarteUtilisateur(4, 9),
        new CarteUtilisateur(5, 9),
        new CarteUtilisateur(4, 10),
        new CarteUtilisateur(4, 11),
        new CarteUtilisateur(4, 12),
        new CarteUtilisateur(4, 13),
        new CarteUtilisateur(4, 14),
    };

    internal static readonly List<Liste> Listes = new List<Liste>()
    {
        new Liste(1, "À faire", 0, 1),
        new Liste(2, "En cours", 1, 1),
        new Liste(3, "À valider", 2, 1),
        new Liste(4, "Terminé", 3, 1),
        new Liste(5, "Avant déménagement", 0, 2),
        new Liste(6, "Jour déménagement", 1, 2),
        new Liste(7, "Après déménagement", 2, 2),
        new Liste(8, "Terminé", 3, 2),
    };

    internal static readonly List<Tableau> Tableaux = new List<Tableau>()
    {
        new Tableau(1, "Site web Restaurant du coin inc."),
        new Tableau(2, "Déménagement de bureau")
    };
}