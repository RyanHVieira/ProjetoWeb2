namespace backend.DTOs.Equipment;

public class EquipmentResponseDTO{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public EquipmentTypeDTO? Tipo { get; set; }
}