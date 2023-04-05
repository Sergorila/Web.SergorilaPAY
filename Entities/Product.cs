namespace Entities;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; }
    public double Price { get; set; }
    public string? Img { get; set; }
    public string? Description { get; set; }
    
    public IEnumerable<Img> Images { get; set; }
    public IEnumerable<Category> Categories { get; set; }
    public IEnumerable<Order> Orders { get; set; }
}