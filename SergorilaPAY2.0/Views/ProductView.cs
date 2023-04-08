using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SergorilaPAY2._0.Views;

public class ProductView
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public double Price { get; set; }
    
    [Required]
    public string? Img { get; set; }
    
    [Required]
    public string? Description { get; set; }
}