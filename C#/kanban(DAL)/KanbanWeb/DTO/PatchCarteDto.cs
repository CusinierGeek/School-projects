using System.ComponentModel.DataAnnotations;

namespace KanbanWeb.DTO;

public class PatchCarteDto
{
    [Required(ErrorMessage = "L'ordre de la carte est obligatoire")]
    [Range(0, int.MaxValue, ErrorMessage = "L'ordre d'une carte doit être plus grand ou égal à 0.")]
    public int Ordre { get; set; }
    
    [Required(ErrorMessage = "L'id de la liste de la carte est obligatoire")]
    public int ListeId { get; set; }
}