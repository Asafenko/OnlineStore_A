using OnlineStore.Data;

namespace OnlineStore.Domain.Entities;

public record CartItem(Guid Id, Guid ProductId, double Quantity, decimal Price)
{
    public Guid Id { get; set; } = Id;
    public Guid ProductId { get; set; } = ProductId;
    public decimal Price { get; set; } = Price;
    public double Quantity { get; set; } = Quantity;

    public Guid CartId { get; set; }
    public Cart Cart { get; set; } = null!;
}