using System.ComponentModel.DataAnnotations;

namespace KanbanWeb.DTO;

public class PatchListeDto
{
    [Required(ErrorMessage = "Le nom de la liste est obligatoire")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "La longueur du nom de la liste doit être entre 1 et 50")]
    public string Nom { get; set; } = null!;
    
    [Required(ErrorMessage = "L'ordre de la liste est obligatoire")]
    [Range(0, int.MaxValue, ErrorMessage = "L'ordre d'une liste doit être plus grand ou égal à 0.")]
    public int Ordre { get; set; }
}