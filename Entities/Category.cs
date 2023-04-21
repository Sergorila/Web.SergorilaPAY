using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Category
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string? Title { get; set; }
    
    public IEnumerable<Product>? Elems { get; set; }
}