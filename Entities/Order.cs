namespace Entities;
public enum Status
{
    Comlete, Working
}

public class Order
{
    
    public int Id { get; set; }
    public IEnumerable<Product> Products { get; set; }
    public int Sum { get; set; }
    public bool Status { get; set; }
    public Status Address { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}