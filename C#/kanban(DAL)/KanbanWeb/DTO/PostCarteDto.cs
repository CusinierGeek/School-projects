using System.ComponentModel.DataAnnotations;
using KanbanDonnees.Entities;

namespace KanbanWeb.DTO;

public class PostCarteDto
{
    [Required(ErrorMessage = "Le titre de la carte est obligatoire")]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "La longueur du titre de la carte doit être entre 1 et 30")]
    public string Titre { get; set; } = null!;
    
    [Required(ErrorMessage = "La description de la carte est obligatoire")]
    public string Description { get; set; } = null!;

    public DateTime? Echeance { get; set; } = null!;

    [Required(ErrorMessage = "Les responsables sont obligatoires")]
    public List<Utilisateur> Responsables { get; set; } = null!;
}