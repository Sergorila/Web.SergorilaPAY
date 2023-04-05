namespace Entities;

public class Img
{
    public int? Id { get; set; }
    public byte[]? Image { get; set; }
    public int? ProductId { get; set; }
    public Product? Product { get; set; }
}