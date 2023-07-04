using KanbanDonnees.Entities;

namespace KanbanDonnees.DAO.Interfaces;

/// <summary>
/// Interface des opérations disponibles pour les <see cref="Carte"/> auprès d'une solution de persistance.
/// </summary>
public interface ICarteDao
{
    /// <summary>
    /// Retrouve une <see cref="Carte"/> incluant ses <see cref="Carte.Responsables"/>.
    /// </summary>
    /// <param name="id">L'id de la carte à sélectionner</param>
    /// <returns>La <see cref="Carte"/>. Null si aucun id ne correspond à une <see cref="Carte"/>.</returns>
    public Carte? Select(int id);

    /// <summary>
    /// Retrouve toutes les <see cref="Carte"/> d'une <see cref="Liste"/> incluant ses <see cref="Carte.Responsables"/>.
    /// Les <see cref="Carte"/> sont retournées en ordre croissant selon <see cref="Carte.Ordre"/>.    
    /// </summary>
    /// <param name="listeId">L'id de la liste pour laquelle retrouver les <see cref="Carte"/></param>
    /// <returns>Collection de <see cref="Carte"/> de type <see cref="List{T}"/>. Vide si rien n'est trouvé.</returns>
    public List<Carte> SelectAllByListeId(int listeId);

    /// <summary>
    /// Ajoute une nouvelle <see cref="Carte"/> incluant les <see cref="Carte.Responsables"/>.
    /// Génère un <see cref="Carte.Id"/> unique. 
    /// </summary>
    /// <param name="carte">La carte <see cref="Carte"/> à ajouter</param>
    /// <returns>La <see cref="Carte"/> fournie en paramètre avec un <see cref="Carte.Id"/> unique.</returns>
    public Carte Insert(Carte carte);

    /// <summary>
    /// Met à jour une <see cref="Carte"/>. Les champs mis à jour sont :
    /// <list type="bullet">
    /// <item><see cref="Carte.Titre"/></item>
    /// <item><see cref="Carte.Description"/></item>
    /// <item><see cref="Carte.Echeance"/></item>
    /// <item><see cref="Carte.Ordre"/></item>
    /// <item><see cref="Carte.ListeId"/></item>
    /// <item><see cref="Carte.Responsables"/></item>
    /// </list>
    /// </summary>
    /// <param name="carte">La <see cref="Carte"/> à mettre à jour</param>
    /// <returns>La <see cref="Carte"/> fournie en paramètre</returns>
    public Carte Update(Carte carte);

    /// <summary>
    /// Supprime une <see cref="Carte"/>.
    /// </summary>
    /// <param name="id">L'id de la <see cref="Carte"/> à supprimer</param>
    /// <returns>True si une suppression a été faite. False dans le cas contraire.</returns>
    public bool Delete(int id);
}