using System.ComponentModel.DataAnnotations;
using backend.DTOs.Equipment;

namespace Backend.DTOs;

public class EquipmentUpdateDTO{
    [MinLength(3)]
    [MaxLength(50)]
    public string? Nome { get; set; }

    public EquipmentTypeDTO? Tipo { get; set; }
}