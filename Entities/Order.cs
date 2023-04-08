using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;
public enum Status
{
    Comlete, Working
}

public class Order
{
    
    public int Id { get; set; }
    
    [NotMapped]
    public IEnumerable<Product> Products { get; set; }
    
    [Required]
    public int Sum { get; set; }
    [Required]
    public Status Status { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public int UserId { get; set; }
    
    [NotMapped]
    public User User { get; set; }
}