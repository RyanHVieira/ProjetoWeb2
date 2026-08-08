using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("Products")]
public class Product{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(500)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    [Column("price")]
    public decimal Price { get; set; }

    [Required]
    [MinLength(10)]
    [MaxLength(500)]
    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    [Column("image_url")]
    public string ImageUrl { get; set; } = string.Empty;

    [Required]
    [Range(0, int.MaxValue)]
    [Column("quantity")]
    public int Quantity { get; set; }

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}