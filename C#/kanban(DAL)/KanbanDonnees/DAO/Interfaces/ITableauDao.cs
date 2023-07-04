using KanbanDonnees.Entities;

namespace KanbanDonnees.DAO.Interfaces;

/// <summary>
/// Interface des opérations disponibles pour les <see cref="Tableau"/> auprès d'une solution de persistance.
/// </summary>
public interface ITableauDao
{
    
    /// <summary>
    /// Retrouve un <see cref="Tableau"/> incluant ses <see cref="Tableau.Listes"/>.
    /// </summary>
    /// <param name="id">L'id du tableau à sélectionner</param>
    /// <returns>Le <see cref="Tableau"/>. Null si aucun id ne correspond à un <see cref="Tableau"/>.</returns>
    public Tableau? Select(int id);
    
    /// <summary>
    /// Retrouve tous les <see cref="Tableau"/>. Toutefois, elle NE récupère PAS les <see cref="Tableau.Listes"/>.
    /// </summary>
    /// <returns>Collection de <see cref="Tableau"/> de type <see cref="List{T}"/>. Vide si rien n'est trouvé.</returns>
    public List<Tableau> SelectAll();

    /// <summary>
    /// Ajoute un nouveau <see cref="Tableau"/>. Toutefois, elle N'ajoute PAS les <see cref="Tableau.Listes"/>.
    /// Génère un <see cref="Tableau.Id"/> unique. 
    /// </summary>
    /// <param name="tableau">Le tableau <see cref="Tableau"/> à ajouter</param>
    /// <returns>Le <see cref="Tableau"/> fourni en paramètre avec un <see cref="Tableau.Id"/> unique.</returns>
    public Tableau Insert(Tableau tableau);

    /// <summary>
    /// Met à jour un <see cref="Tableau"/>. Les champs mis à jour sont :
    /// <list type="bullet">
    /// <item><see cref="Tableau.Nom"/></item>
    /// </list>
    /// </summary>
    /// <param name="tableau">Le <see cref="Tableau"/> à mettre à jour</param>
    /// <returns>Le <see cref="Tableau"/> fourni en paramètre</returns>
    public Tableau Update(Tableau tableau);

    /// <summary>
    /// Supprime un <see cref="Tableau"/>.
    /// </summary>
    /// <param name="id">L'id du <see cref="Tableau"/> à supprimer</param>
    /// <returns>True si une suppression a été faite. False dans le cas contraire.</returns>
    public bool Delete(int id);
}