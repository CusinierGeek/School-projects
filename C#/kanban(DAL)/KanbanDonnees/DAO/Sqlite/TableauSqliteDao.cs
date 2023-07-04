using System.Data;
using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.Entities;
using Microsoft.Data.Sqlite;

namespace KanbanDonnees.DAO.Sqlite;

public class TableauSqliteDao : SqliteBaseDao, ITableauDao
{
    private readonly ListeSqliteDao _listelDao;

    public TableauSqliteDao(string chaineDeConnexion, ListeSqliteDao listelDao) : base(chaineDeConnexion)
    {
        _listelDao = listelDao;
    }

    public Tableau? Select(int id)
    {
        using SqliteConnection connexion = OuvrirConnexion();
        Tableau? tableau = null;
        string requete = "SELECT * FROM tableau WHERE id = @id";
        using SqliteCommand commande = new SqliteCommand(requete, connexion);
        commande.Parameters.AddWithValue("@id", id);
        commande.Prepare();
        using SqliteDataReader lecteur = commande.ExecuteReader();

        if (lecteur.Read())
        {
            tableau = ConstruireTableau(lecteur);
        }

        return tableau;
    }

    public List<Tableau> SelectAll()
    {
        List<Tableau> tableaux = new List<Tableau>();
        Tableau? tableau = null;
        using SqliteConnection connexion = OuvrirConnexion();
        string requete = "SELECT * FROM tableau ";
        using SqliteCommand commande = new SqliteCommand(requete, connexion);
        commande.Prepare();
        using SqliteDataReader lecteur = commande.ExecuteReader();
        while (lecteur.Read())
        {
            tableau = ConstruireTableau(lecteur);
            tableaux.Add(tableau);
        }

        return tableaux;
    }

    public Tableau Insert(Tableau tableau)
    {
        using SqliteConnection connexion = OuvrirConnexion();

        string requete = "INSERT INTO tableau (nom) " +
                         "VALUES (@nom)";

        using SqliteCommand commande = new SqliteCommand(requete, connexion);
        commande.Parameters.AddWithValue("@nom", tableau.Nom);
        commande.Prepare();
        int nbLignesAffectees = commande.ExecuteNonQuery();
        if (nbLignesAffectees != 1) throw new InvalidOperationException("Impossible d'insérer l'article.");
        tableau.Id = LastInsertId(connexion);
        return tableau;
    }

    public Tableau Update(Tableau tableau)
    {
        using SqliteConnection connexion = OuvrirConnexion();

        using SqliteCommand commande =
            new SqliteCommand("UPDATE tableau SET nom = @nom  WHERE id = @id ", connexion);


        commande.Parameters.AddWithValue("@nom", tableau.Nom);
        commande.Parameters.AddWithValue("@id", tableau.Id);
        commande.Prepare();

        int nbLignesAffectees = commande.ExecuteNonQuery();
        if (nbLignesAffectees != 1) throw new InvalidOperationException("Impossible d'insérer l'article.");
        return tableau;
    }

    public bool Delete(int id)
    {
        using SqliteConnection connexion = OuvrirConnexion();

        using SqliteCommand commande = new SqliteCommand("DELETE FROM tableau WHERE id = @id", connexion);
        commande.Parameters.AddWithValue("@id", id);
        commande.Prepare();
        int nbLignesAffectees = commande.ExecuteNonQuery();
        return nbLignesAffectees == 1;
    }

    private Tableau ConstruireTableau(SqliteDataReader lecteur)
    {
        Tableau tableau = new Tableau(
            lecteur.GetInt32("id"),
            lecteur.GetString("nom"));
        tableau.Listes = _listelDao.SelectAllByTableauId(tableau.Id);

        return tableau;
    }
}