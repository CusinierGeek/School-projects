using KanbanDonnees.Entities;

namespace KanbanDonnees.DAO.Interfaces;

/// <summary>
/// Interface des opérations disponibles pour les <see cref="Liste"/> auprès d'une solution de persistance.
/// </summary>
public interface IListeDao
{
    /// <summary>
    /// Retrouve une <see cref="Liste"/> incluant ses <see cref="Liste.Cartes"/>.
    /// </summary>
    /// <param name="id">L'id de la liste à sélectionner</param>
    /// <returns>La <see cref="Liste"/>. Null si aucun id correspond à une <see cref="Liste"/>.</returns>
    public Liste? Select(int id);
    
    /// <summary>
    /// Retrouve toutes les <see cref="Liste"/> d'un <see cref="Tableau"/> incluant ses <see cref="Liste.Cartes"/>.
    /// Les <see cref="Liste"/> sont retournées en ordre croissant selon <see cref="Liste.Ordre"/>.    
    /// </summary>
    /// <param name="tableauId">L'id du tableau pour lequel retrouver les <see cref="Liste"/></param>
    /// <returns>Collection de <see cref="Liste"/> de type <see cref="List{T}"/>. Vide si rien n'est trouvé.</returns>
    public List<Liste> SelectAllByTableauId(int tableauId);
    
    /// <summary>
    /// Ajoute une nouvelle <see cref="Liste"/>. Toutefois, elle N'ajoute PAS les <see cref="Liste.Cartes"/>.
    /// Génère un <see cref="Liste.Id"/> unique. 
    /// </summary>
    /// <param name="liste">La liste <see cref="Liste"/> à ajouter</param>
    /// <returns>La <see cref="Liste"/> fournie en paramètre avec un <see cref="Liste.Id"/> unique.</returns>
    public Liste Insert(Liste liste);

    /// <summary>
    /// Met à jour une <see cref="Liste"/>. Les champs mis à jour sont :
    /// <list type="bullet">
    /// <item><see cref="Liste.Nom"/></item>
    /// <item><see cref="Liste.Ordre"/></item>
    /// </list>
    /// </summary>
    /// <param name="liste">La <see cref="Liste"/> à mettre à jour</param>
    /// <returns>La <see cref="Liste"/> fournie en paramètre</returns>
    public Liste Update(Liste liste);

    /// <summary>
    /// Supprime une <see cref="Liste"/>.
    /// </summary>
    /// <param name="id">L'id de la <see cref="Liste"/> à supprimer</param>
    /// <returns>True si une suppression a été faite. False dans le cas contraire.</returns>
    public bool Delete(int id);
}