using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models{
    [Table("Users")]
    public class User{
        [Column("id")]
        public int Id { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("password_hash")]
        public string PasswordHash { get; set; }
         [Column("role")]
        public string Role { get; set; } = "user";
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}