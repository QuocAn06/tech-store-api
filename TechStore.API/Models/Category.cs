using System.ComponentModel.DataAnnotations;

namespace TechStore.API.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        // A one-to-many relationship: One Category has many Products.
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
