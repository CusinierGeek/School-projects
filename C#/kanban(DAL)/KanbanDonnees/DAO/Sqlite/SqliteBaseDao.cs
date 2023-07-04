using Microsoft.Data.Sqlite;

namespace KanbanDonnees.DAO.Sqlite;

public abstract class SqliteBaseDao
{
    protected string _chainedeconnexion;

    protected SqliteBaseDao(string cheminBd)
    {
        _chainedeconnexion = $"Data Source={cheminBd};Cache=Shared";
        using SqliteConnection test = OuvrirConnexion();
    }

    protected SqliteConnection OuvrirConnexion()
    {
        SqliteConnection connexion = new SqliteConnection(_chainedeconnexion);
        connexion.Open();
        return connexion;
    }

    protected int LastInsertId(SqliteConnection conmexion, SqliteTransaction? transaction = null)
    {
        string requete = "SELECT last_insert_rowid()";
        using SqliteCommand commande = transaction != null
            ? new SqliteCommand(requete, conmexion, transaction)
            : new SqliteCommand(requete, conmexion);
        return (int)(long)(commande.ExecuteScalar() ?? 0);
    }
}