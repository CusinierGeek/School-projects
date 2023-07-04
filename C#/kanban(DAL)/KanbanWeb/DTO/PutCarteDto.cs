using System.ComponentModel.DataAnnotations;
using KanbanDonnees.Entities;

namespace KanbanWeb.DTO;

public class PutCarteDto
{
    [Required(ErrorMessage = "Le titre de la carte est obligatoire")]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "La longueur du titre de la carte doit être entre 1 et 30")]
    public string Titre { get; set; } = null!;
    
    [Required(ErrorMessage = "La description de la carte est obligatoire")]
    public string Description { get; set; } = null!;

    public DateTime? Echeance { get; set; } = null!;

    [Required(ErrorMessage = "L'ordre de la carte est obligatoire")]
    [Range(0, int.MaxValue, ErrorMessage = "L'ordre d'une carte doit être plus grand ou égal à 0.")]
    public int Ordre { get; set; }

    [Required(ErrorMessage = "L'id de la liste de la carte est obligatoire")]
    public int ListeId { get; set; }

    [Required(ErrorMessage = "Les responsables sont obligatoires")]
    public List<Utilisateur> Responsables { get; set; } = null!;
}