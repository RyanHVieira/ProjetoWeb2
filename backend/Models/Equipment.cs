using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Models;

[Table("Equipments")]
public class Equipment{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int TypeID { get; set; }

    [ForeignKey(nameof(TypeID))]
    public EquipType Type { get; set; } = null!;
}