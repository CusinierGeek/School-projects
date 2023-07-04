using System.Reflection;
using KanbanDonnees.DAO.Interfaces;
using MySql.Data.MySqlClient;

namespace KanbanDonnees.DAO.Mysql;

public class GestionPersistanceMysql : IGestionPersistance
{
    private string _chaineDeConnexion;

    public GestionPersistanceMysql(string chaineDeConnexion)
    {
        _chaineDeConnexion = chaineDeConnexion;
    }

    public void CreerPersistanceEtInsererDonnees()
    {
        using MySqlConnection connexion = new MySqlConnection(_chaineDeConnexion);
        connexion.Open();
        var assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream("bd-mysql.sql");
        using StreamReader r = new StreamReader(stream!);
        string contenuFichier = r.ReadToEnd();
        using MySqlCommand commande = new MySqlCommand(contenuFichier, connexion);
        commande.ExecuteNonQuery();
    }
}