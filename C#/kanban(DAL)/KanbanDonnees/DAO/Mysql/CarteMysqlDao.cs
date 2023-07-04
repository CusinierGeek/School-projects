using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.Entities;
using MySql.Data.MySqlClient;

namespace KanbanDonnees.DAO.Mysql;

public class CarteMysqlDao : MysqlBaseDao, ICarteDao
{
    private readonly UtilisateurMysqlDao _utilisateurDao;

    public CarteMysqlDao(string chaineDeConnexion, UtilisateurMysqlDao utilisateurDao) : base(chaineDeConnexion)
    {
        _utilisateurDao = utilisateurDao;
    }


    public Carte? Select(int id)
    {
        using MySqlConnection connexion = OuvrirConnexion();
        Carte? carte = null;
        string requete = "SELECT * FROM carte WHERE id = @id";
        using MySqlCommand commande = new MySqlCommand(requete, connexion);
        commande.Parameters.AddWithValue("@id", id);
        commande.Prepare();
        using MySqlDataReader lecteur = commande.ExecuteReader();
        if (lecteur.Read())
        {
            carte = ConstruireCarte(lecteur);
        }

        return carte;
    }

    public List<Carte> SelectAllByListeId(int listeId)
    {
        List<Carte> cartes = new List<Carte>();
        Carte? carte = null;
        using MySqlConnection connexion = OuvrirConnexion();
        string requete = "SELECT * FROM carte where liste_id = @liste_id";
        using MySqlCommand commande = new MySqlCommand(requete, connexion);
        commande.Parameters.AddWithValue("@liste_id", listeId);
        commande.Prepare();
        using MySqlDataReader lecteur = commande.ExecuteReader();
        while (lecteur.Read())
        {
            cartes.Add(ConstruireCarte(lecteur));
        }

        return cartes;
    }

    public Carte Insert(Carte carte)
    {
        using MySqlConnection connexion = OuvrirConnexion();

        using MySqlTransaction transaction = connexion.BeginTransaction();
        try
        {
            string requete =
                "INSERT INTO carte (titre, description, echeance, ordre ,liste_id) " +
                "VALUES (@titre, @description, @echeance, @ordre, @liste_id)";
            using MySqlCommand commande = new MySqlCommand(requete, connexion, transaction);
            commande.Parameters.AddWithValue("@titre", carte.Titre);
            commande.Parameters.AddWithValue("@description", carte.Description);
            commande.Parameters.AddWithValue("@echeance", carte.Echeance);
            commande.Parameters.AddWithValue("@ordre", carte.Ordre);
            commande.Parameters.AddWithValue("@liste_id", carte.ListeId);
            commande.Prepare();
            int nbLignesAffectees = commande.ExecuteNonQuery();
            if (nbLignesAffectees != 1) throw new InvalidOperationException("Impossible d'insérer l'article.");
            carte.Id = (int)commande.LastInsertedId;
            UpdateCarteUtilisateur(carte, connexion, transaction);

            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }

        return carte;
    }

    public Carte Update(Carte carte)
    {
        using MySqlConnection connexion = OuvrirConnexion();
        using MySqlTransaction transaction = connexion.BeginTransaction();
        try
        {
            using MySqlCommand commande = new MySqlCommand(
                "UPDATE carte SET titre = @titre, description = @description, " +
                "echeance = @echeance, ordre = @ordre, liste_id = @liste_id " +
                "WHERE id = @id", connexion, transaction);
            commande.Parameters.AddWithValue("@id", carte.Id);
            commande.Parameters.AddWithValue("@titre", carte.Titre);
            commande.Parameters.AddWithValue("@description", carte.Description);
            // commande.Parameters.AddWithValue("@echeance", (object)carte.Echeance ?? DBNull.Value);
            commande.Parameters.AddWithValue("@echeance", carte.Echeance);
            commande.Parameters.AddWithValue("@ordre", carte.Ordre);
            commande.Parameters.AddWithValue("@liste_id", carte.ListeId);
            commande.Prepare();
            int nbLignesAffectees = commande.ExecuteNonQuery();
            if (nbLignesAffectees != 1) throw new InvalidOperationException("Impossible de mettre à jour l'article.");
            UpdateCarteUtilisateur(carte, connexion, transaction);
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }

        return carte;
    }


    public bool Delete(int id)
    {
        using MySqlConnection connexion = OuvrirConnexion();
        using MySqlCommand commande = new MySqlCommand("DELETE FROM carte WHERE id = @id", connexion);
        commande.Parameters.AddWithValue("@id", id);
        commande.Prepare();
        int nbLignesAffectees = commande.ExecuteNonQuery();
        return nbLignesAffectees == 1;
    }


    private void UpdateCarteUtilisateur(Carte carte, MySqlConnection connexion, MySqlTransaction transaction)
    {
        //Il faut supprimer tous les carte utisateur associés avec l'article
        string requete = "DELETE FROM carte_utilisateur WHERE carte_id = @carte_id;";
        using MySqlCommand commandeSuppression = new MySqlCommand(requete, connexion, transaction);
        commandeSuppression.Parameters.AddWithValue("carte_id", carte.Id);
        commandeSuppression.Prepare();
        commandeSuppression.ExecuteNonQuery();

        // Insérer les nouveaux carte-utilsiateur

        requete = "insert into carte_utilisateur (utilisateur_id, carte_id) values (@utilisateur_id, @carte_id)";

        using MySqlCommand commandeInsertion = new MySqlCommand(requete, connexion, transaction);

        foreach (Utilisateur u in carte.Responsables)
        {
            commandeInsertion.Parameters.Clear();
            commandeInsertion.Parameters.AddWithValue("@utilisateur_id", u.Id);
            commandeInsertion.Parameters.AddWithValue("@carte_id", carte.Id);
            commandeInsertion.Prepare();
            commandeInsertion.ExecuteNonQuery();
        }
    }

    private Carte ConstruireCarte(MySqlDataReader lecteur)
    {
        int echeanceIndex = lecteur.GetOrdinal("echeance");
        Carte carte = new Carte(
            lecteur.GetInt32("id"),
            lecteur.GetString("titre"),
            lecteur.GetString("description"),
            ((lecteur.IsDBNull(echeanceIndex) ? null : lecteur.GetDateTime("echeance"))!),
            lecteur.GetInt32("ordre"),
            lecteur.GetInt32("liste_id")
        );

        InclureUtilisateur(carte);

        return carte;
    }

    private void InclureUtilisateur(Carte carte)
    {
        using MySqlConnection connexion = OuvrirConnexion();
        string requete = " select id, nom_complet from utilisateur INNER join carte_utilisateur " +
                         " on utilisateur.id = carte_utilisateur.utilisateur_id where carte_id = @carte_id;";
        using MySqlCommand commande = new MySqlCommand(requete, connexion);
        commande.Parameters.AddWithValue("@carte_id", carte.Id);
        using MySqlDataReader lecteur = commande.ExecuteReader();
        while (lecteur.Read())
        {
            carte.Responsables.Add(new Utilisateur(lecteur.GetInt32("id"), lecteur.GetString("nom_complet")));
        }
    }
}