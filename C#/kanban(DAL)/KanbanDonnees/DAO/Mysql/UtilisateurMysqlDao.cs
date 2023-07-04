using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.Entities;
using MySql.Data.MySqlClient;

namespace KanbanDonnees.DAO.Mysql;

public class UtilisateurMysqlDao : MysqlBaseDao, IUtilisateurDao
{
    public UtilisateurMysqlDao(string chaineDeConnexion) : base(chaineDeConnexion)
    {
    }

    public List<Utilisateur> SelectAll()
    {
        List<Utilisateur> utilisateurs = new List<Utilisateur>();
        using MySqlConnection connexion = OuvrirConnexion();
        string requete = "SELECT * FROM utilisateur";
        using MySqlCommand commande = new MySqlCommand(requete, connexion);
        commande.Prepare();
        using MySqlDataReader lecteur = commande.ExecuteReader();
        while (lecteur.Read())
        {
            Utilisateur utilisateur = new Utilisateur(
                lecteur.GetInt32("id"),
                lecteur.GetString("nom_complet")
            );
            utilisateurs.Add(utilisateur);
        }

        return utilisateurs;
    }
}