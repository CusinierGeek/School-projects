using System.Reflection;
using KanbanDonnees.DAO.Interfaces;
using Microsoft.Data.Sqlite;

namespace KanbanDonnees.DAO.Sqlite;

public class GestionPersistanceSqlite : IGestionPersistance
{
    private readonly string _cheminBD;

    public GestionPersistanceSqlite(string cheminBd)
    {
        _cheminBD = $"Data Source={cheminBd};Cache=Shared";
    }

    public void CreerPersistanceEtInsererDonnees()
    {
        using SqliteConnection connexion = new SqliteConnection(_cheminBD);
        connexion.Open();

        var assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream("bd-sqlite.sql");
        using StreamReader r = new StreamReader(stream);
        string contenuFichier = r.ReadToEnd();

        using SqliteCommand commande = new SqliteCommand(contenuFichier, connexion);
        commande.ExecuteNonQuery();
    }
}