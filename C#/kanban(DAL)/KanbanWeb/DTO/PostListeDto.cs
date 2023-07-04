using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace KanbanWeb.DTO;

public class PostListeDto
{
    [Required(ErrorMessage = "Le nom de la liste est obligatoire")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "La longueur du nom de la liste doit être entre 1 et 50.")]
    public string Nom { get; set; } = null!;
}