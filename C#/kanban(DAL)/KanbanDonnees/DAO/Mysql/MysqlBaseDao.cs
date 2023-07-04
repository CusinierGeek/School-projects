using MySql.Data.MySqlClient;

namespace KanbanDonnees.DAO.Mysql;

public abstract class MysqlBaseDao
{
    private readonly string _chaineDeConnexion;

    protected MysqlBaseDao(string chaineDeConnexion)
    {
        _chaineDeConnexion = chaineDeConnexion;
        using MySqlConnection test = OuvrirConnexion(); // On teste la connexion
    }

    protected MySqlConnection OuvrirConnexion()
    {
        MySqlConnection connexion = new MySqlConnection(_chaineDeConnexion);
        connexion.Open();
        return connexion;
    }
}