using OnlineStore.Domain.Entities;

namespace OnlineStore.Data;

public record Cart : IEntity
{
    
    public int ItemCount => _items.Count;
    public Guid AccountId { get; set; }
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
    
    private readonly List<CartItem> _items ;
    public Guid Id { get; init; }

    
    protected Cart()
    {
        _items = new List<CartItem>();
    }

    public Cart(Guid id, Guid accountId, List<CartItem> items)
    {
        Id = id;
        AccountId = accountId;
        _items = items;
    }
    
    
    
    
    public decimal GetTotalPrice()
    {
        return _items.Sum(it => it.Price);
    }
    
    public CartItem Add(Product product, double quantity = 1d)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        if(quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

        var cartItem = Items.SingleOrDefault(it => it.ProductId == product.Id);
        if (cartItem is not null)
        {
            var newQty = cartItem.Quantity + quantity;
            if (newQty > 1000 )
            {
                throw new InvalidOperationException("Quantity cannot be greater than 1000");
            }

            cartItem.Quantity = newQty;
        }
        else
        {
            cartItem = new CartItem(Guid.Empty, product.Id, quantity, product.Price);
            _items.Add(cartItem);
        }
        return cartItem;
    }
    

}
