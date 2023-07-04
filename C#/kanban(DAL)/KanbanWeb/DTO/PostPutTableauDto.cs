using System.ComponentModel.DataAnnotations;

namespace KanbanWeb.DTO;

public class PostPutTableauDto
{
    [Required(ErrorMessage = "Le nom du tableau est obligatoire")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "La longueur du nom du tableau doit être entre 1 et 50")]
    public string Nom { get; set; } = null!;
}