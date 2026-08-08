using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("Users")]
public class User{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    [Column("username")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    [MaxLength(50)]
    [Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    [Column("role")]
    public string Role { get; set; } = "user";

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}