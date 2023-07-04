using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.Entities;
using MySql.Data.MySqlClient;

namespace KanbanDonnees.DAO.Mysql;

public class TableauMysqlDao : MysqlBaseDao, ITableauDao
{
    private readonly ListeMysqlDao _listelDao;

    public TableauMysqlDao(string chaineDeConnexion, ListeMysqlDao listelDao) : base(chaineDeConnexion)
    {
        _listelDao = listelDao;
    }

    public Tableau? Select(int id)
    {
        using MySqlConnection connexion = OuvrirConnexion();
        Tableau? tableau = null;
        string requete = "SELECT * FROM tableau WHERE id = @id";
        using MySqlCommand commande = new MySqlCommand(requete, connexion);
        commande.Parameters.AddWithValue("@id", id);
        commande.Prepare();
        using MySqlDataReader lecteur = commande.ExecuteReader();

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
        using MySqlConnection connexion = OuvrirConnexion();
        string requete = "SELECT * FROM tableau ";
        using MySqlCommand commande = new MySqlCommand(requete, connexion);
        commande.Prepare();
        using MySqlDataReader lecteur = commande.ExecuteReader();
        while (lecteur.Read())
        {
            tableau = ConstruireTableau(lecteur);
            tableaux.Add(tableau);
        }

        return tableaux;
    }

    public Tableau Insert(Tableau tableau)
    {
        using MySqlConnection connexion = OuvrirConnexion();

        string requete = "INSERT INTO tableau (nom) " +
                         "VALUES (@nom)";

        using MySqlCommand commande = new MySqlCommand(requete, connexion);
        commande.Parameters.AddWithValue("@nom", tableau.Nom);
        commande.Prepare();
        int nbLignesAffectees = commande.ExecuteNonQuery();
        if (nbLignesAffectees != 1) throw new InvalidOperationException("Impossible d'insérer l'article.");
        tableau.Id = (int)commande.LastInsertedId;
        return tableau;
    }

    public Tableau Update(Tableau tableau)
    {
        using MySqlConnection connexion = OuvrirConnexion();

        using MySqlCommand commande =
            new MySqlCommand("UPDATE tableau SET nom = @nom  WHERE id = @id ", connexion);


        commande.Parameters.AddWithValue("@nom", tableau.Nom);
        commande.Parameters.AddWithValue("@id", tableau.Id);
        commande.Prepare();

        int nbLignesAffectees = commande.ExecuteNonQuery();
        if (nbLignesAffectees != 1) throw new InvalidOperationException("Impossible d'insérer l'article.");
        return tableau;
    }

    public bool Delete(int id)
    {
        using MySqlConnection connexion = OuvrirConnexion();

        using MySqlCommand commande = new MySqlCommand("DELETE FROM tableau WHERE id = @id", connexion);
        commande.Parameters.AddWithValue("@id", id);
        commande.Prepare();
        int nbLignesAffectees = commande.ExecuteNonQuery();
        return nbLignesAffectees == 1;
    }

    private Tableau ConstruireTableau(MySqlDataReader lecteur)
    {
        Tableau tableau = new Tableau(
            lecteur.GetInt32("id"),
            lecteur.GetString("nom"));
        tableau.Listes = _listelDao.SelectAllByTableauId(tableau.Id);

        return tableau;
    }
}