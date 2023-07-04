using System.Data;
using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.DAO.Mysql;
using KanbanDonnees.Entities;
using Microsoft.Data.Sqlite;

namespace KanbanDonnees.DAO.Sqlite;

public class ListeSqliteDao : SqliteBaseDao, IListeDao
{
    private readonly CarteSqliteDao _carteDao;

    public ListeSqliteDao(string chaineDeConnexion, CarteSqliteDao carteDao) : base(chaineDeConnexion)
    {
        _carteDao = carteDao;
    }

    public Liste? Select(int id)
    {
        using SqliteConnection connexion = OuvrirConnexion();
        Liste? liste = null;
        string requete = "SELECT * FROM liste WHERE id = @id";
        using SqliteCommand commande = new SqliteCommand(requete, connexion);
        commande.Parameters.AddWithValue("@id", id);
        commande.Prepare();
        using SqliteDataReader lecteur = commande.ExecuteReader();

        if (lecteur.Read())
        {
            liste = ConstruireListe(lecteur);
        }

        return liste;
    }


    public List<Liste> SelectAllByTableauId(int tableauId)
    {
        List<Liste> listes = new List<Liste>();
        Liste? liste = null;
        using SqliteConnection connexion = OuvrirConnexion();
        string requete = "SELECT * FROM liste where tableau_id = @tableau_id";
        using SqliteCommand commande = new SqliteCommand(requete, connexion);
        commande.Parameters.AddWithValue("@tableau_id", tableauId);
        commande.Prepare();
        using SqliteDataReader lecteur = commande.ExecuteReader();
        while (lecteur.Read())
        {
            listes.Add(ConstruireListe(lecteur));
        }

        return listes;
    }

    public Liste Insert(Liste liste)
    {
        using SqliteConnection connexion = OuvrirConnexion();

        string requete = "INSERT INTO liste (nom, ordre,tableau_id) " +
                         "VALUES (@nom, @ordre,@tableau_id)";

        using SqliteCommand commande = new SqliteCommand(requete, connexion);
        commande.Parameters.AddWithValue("@nom", liste.Nom);
        commande.Parameters.AddWithValue("@ordre", liste.Ordre);
        commande.Parameters.AddWithValue("@tableau_id", liste.TableauId);
        commande.Prepare();

        int nbLignesAffectees = commande.ExecuteNonQuery();
        if (nbLignesAffectees != 1) throw new InvalidOperationException("Impossible d'insérer l'article.");
        liste.Id = LastInsertId(connexion);
        return liste;
    }

    public Liste Update(Liste liste)
    {
        using SqliteConnection connexion = OuvrirConnexion();

        using SqliteCommand commande =
            new SqliteCommand("UPDATE liste SET nom = @nom , ordre = @ordre WHERE id = @id ", connexion);


        commande.Parameters.AddWithValue("@nom", liste.Nom);
        commande.Parameters.AddWithValue("@ordre", liste.Ordre);
        commande.Parameters.AddWithValue("@id", liste.Id);
        commande.Prepare();

        int nbLignesAffectees = commande.ExecuteNonQuery();
        if (nbLignesAffectees != 1) throw new InvalidOperationException("Impossible d'insérer l'article.");

        return liste;
    }

    public bool Delete(int id)
    {
        using SqliteConnection connexion = OuvrirConnexion();

        using SqliteCommand commande = new SqliteCommand("DELETE FROM liste WHERE id = @id", connexion);
        commande.Parameters.AddWithValue("@id", id);
        commande.Prepare();
        int nbLignesAffectees = commande.ExecuteNonQuery();
        return nbLignesAffectees == 1;
    }

    private Liste ConstruireListe(SqliteDataReader lecteur)
    {
        Liste liste = new Liste(
            lecteur.GetInt32("id"),
            lecteur.GetString("nom"),
            lecteur.GetInt32("ordre"),
            lecteur.GetInt32("tableau_id"));
        liste.Cartes = _carteDao.SelectAllByListeId(liste.Id);

        return liste;
    }
}