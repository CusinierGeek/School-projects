using KanbanDonnees.Entities;

namespace KanbanDonnees.DAO.Interfaces;

/// <summary>
/// Interface des opérations disponibles pour les <see cref="Utilisateur"/> auprès d'une solution de persistance.
/// </summary>
public interface IUtilisateurDao
{
    /// <summary>
    /// Retrouve tous les <see cref="Utilisateur"/> 
    /// </summary>
    /// <returns>Collection de tous <see cref="Utilisateur"/> de type <see cref="List{T}"/>. Vide si rien n'est trouvé.</returns>
    public List<Utilisateur> SelectAll();
}