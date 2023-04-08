using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Category
{
    public int Id { get; set; }
    public string? Title { get; set; }
    
    [NotMapped]
    public IEnumerable<Product>? Elems { get; set; }
}