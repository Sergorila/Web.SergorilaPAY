using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities;

namespace SergorilaPAY2._0.Views;

public class OrderView
{
    public int Id { get; set; }

    [Required]
    public int Sum { get; set; }
    
    [Required]
    public Status Status { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
}