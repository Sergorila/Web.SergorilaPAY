using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;
public enum Status
{
    Comlete, Working
}

public class Order
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int Sum { get; set; }
    
    [Required]
    public Status Status { get; set; }
    
    [Required]
    public string Address { get; set; }

    [ForeignKey(nameof(User))]
    public int? UserId { get; set; }
    
    public User? User { get; set; }
    public IEnumerable<Product>? Products { get; set; }
    
}