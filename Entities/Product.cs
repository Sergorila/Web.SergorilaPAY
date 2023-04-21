using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Product
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public double Price { get; set; }
    
    [Required]
    public string? Img { get; set; }
    
    [Required]
    public string? Description { get; set; }
    
    public int? OrderId { get; set; }
    
    public int? CategoryId { get; set; }
}