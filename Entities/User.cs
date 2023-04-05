namespace Entities;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string? FIO { get; set; }
    public string? Phone { get; set; }
    public string TelegramID { get; set; }
    
    public IEnumerable<Order> Orders { get; set; }
}