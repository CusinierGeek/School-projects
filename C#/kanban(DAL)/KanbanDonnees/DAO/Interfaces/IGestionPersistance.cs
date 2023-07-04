namespace KanbanDonnees.DAO.Interfaces;

/// <summary>
/// Interface des opérations disponibles pour créer et alimenter (seeder) une solution de persistance.
/// </summary>
public interface IGestionPersistance
{
    /// <summary>
    /// Crée ce qu'il faut pour que la solution de persistance puisse être utilisée
    /// et insère des données de départ dans cette dernière.
    /// </summary>
    public void CreerPersistanceEtInsererDonnees();
}