using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities;

namespace SergorilaPAY2._0.Views;

public class UserView
{
    public int Id { get; set; }
    
    [Required]
    public string Login { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string? FIO { get; set; }
    
    [Required]
    public string? Phone { get; set; }

    [Required] 
    public string TelegramID { get; set; }
}