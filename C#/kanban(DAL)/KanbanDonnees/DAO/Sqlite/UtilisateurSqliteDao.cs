using System.Data;
using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.Entities;
using Microsoft.Data.Sqlite;

namespace KanbanDonnees.DAO.Sqlite;

public class UtilisateurSqliteDao : SqliteBaseDao, IUtilisateurDao
{
    public UtilisateurSqliteDao(string cheminBd) : base(cheminBd)
    {
    }

    public List<Utilisateur> SelectAll()
    {
        List<Utilisateur> utilisateurs = new List<Utilisateur>();
        using SqliteConnection connexion = OuvrirConnexion();
        string requete = "SELECT * FROM utilisateur";
        using SqliteCommand commande = new SqliteCommand(requete, connexion);
        commande.Prepare();
        using SqliteDataReader lecteur = commande.ExecuteReader();
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