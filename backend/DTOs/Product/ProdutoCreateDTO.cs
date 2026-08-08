using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class ProductCreateDTO{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [MaxLength(500)]
    public string ImageUrl { get; set; } = string.Empty;
}