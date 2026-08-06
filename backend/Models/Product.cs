using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models{
    [Table("Products")]
    public class Product{
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("image_url")]
        public string ImageUrl { get; set; }
    }
}