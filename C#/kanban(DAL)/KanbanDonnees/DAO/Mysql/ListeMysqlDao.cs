using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.Entities;
using MySql.Data.MySqlClient;

namespace KanbanDonnees.DAO.Mysql;

public class ListeMysqlDao : MysqlBaseDao, IListeDao
{
    private readonly CarteMysqlDao _carteDao;

    public ListeMysqlDao(string chaineDeConnexion, CarteMysqlDao carteDao) : base(chaineDeConnexion)
    {
        _carteDao = carteDao;
    }

    public Liste? Select(int id)
    {
        using MySqlConnection connexion = OuvrirConnexion();
        Liste? liste = null;
        string requete = "SELECT * FROM liste WHERE id = @id";
        using MySqlCommand commande = new MySqlCommand(requete, connexion);
        commande.Parameters.AddWithValue("@id", id);
        commande.Prepare();
        using MySqlDataReader lecteur = commande.ExecuteReader();
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
        using MySqlConnection connexion = OuvrirConnexion();
        string requete = "SELECT * FROM liste where tableau_id = @tableau_id";
        using MySqlCommand commande = new MySqlCommand(requete, connexion);
        commande.Parameters.AddWithValue("@tableau_id", tableauId);
        commande.Prepare();
        using MySqlDataReader lecteur = commande.ExecuteReader();
        while (lecteur.Read())
        {
            listes.Add(ConstruireListe(lecteur));
        }

        return listes;
    }

    public Liste Insert(Liste liste)
    {
        using MySqlConnection connexion = OuvrirConnexion();

        string requete = "INSERT INTO liste (nom, ordre,tableau_id) " +
                         "VALUES (@nom, @ordre,@tableau_id)";

        using MySqlCommand commande = new MySqlCommand(requete, connexion);
        commande.Parameters.AddWithValue("@nom", liste.Nom);
        commande.Parameters.AddWithValue("@ordre", liste.Ordre);
        commande.Parameters.AddWithValue("@tableau_id", liste.TableauId);
        commande.Prepare();

        int nbLignesAffectees = commande.ExecuteNonQuery();
        if (nbLignesAffectees != 1) throw new InvalidOperationException("Impossible d'insérer l'article.");
        liste.Id = (int)commande.LastInsertedId;
        return liste;
    }

    public Liste Update(Liste liste)
    {
        using MySqlConnection connexion = OuvrirConnexion();

        using MySqlCommand commande =
            new MySqlCommand("UPDATE liste SET nom = @nom , ordre = @ordre WHERE id = @id ", connexion);


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
        using MySqlConnection connexion = OuvrirConnexion();

        using MySqlCommand commande = new MySqlCommand("DELETE FROM liste WHERE id = @id", connexion);
        commande.Parameters.AddWithValue("@id", id);
        commande.Prepare();
        int nbLignesAffectees = commande.ExecuteNonQuery();
        return nbLignesAffectees == 1;
    }

    private Liste ConstruireListe(MySqlDataReader lecteur)
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