using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class EquipmentCreateDTO{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    public int TipoId { get; set; }
}