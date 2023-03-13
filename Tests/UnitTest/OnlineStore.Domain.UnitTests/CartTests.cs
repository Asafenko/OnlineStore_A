 using OnlineStore.Data;
 using OnlineStore.Domain.Entities;


namespace SuperShop.Domain.Test;

 public class CartTests
{
    [Fact]
    public void New_item_is_added_to_cart()
    {
        // Arange
        var cart = new Cart(Guid.Empty, Guid.Empty, new List<CartItem>());
      
        
        // Act
        var product = new Product("something","description",45m,"/",Guid.Empty);
        cart.Add(product);
        
        // Assert
        Assert.Single(cart.Items);
        var cartItem = cart.Items.First();
        Assert.NotNull(cartItem);
        Assert.Equal(product.Id,cartItem.ProductId);
        Assert.Equal(1d,cartItem.Quantity);
    }
    [Fact]
    public void Adding_empty_product_is_impossible()
    {
        // Arange
        var cart = new Cart(Guid.Empty, Guid.Empty, new List<CartItem>());
        
        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => cart.Add(null));
    }

    [Fact]
    public void Adding_existed_product_to_cart_changes_quantity()
    {
        // Arrange
        var cart = new Cart(Guid.Empty, Guid.Empty, new List<CartItem>());
        
        
        // Act and  Assert
        var product = new Product("something","description",45m,"/",Guid.Empty);
        //var product2 = new Product("something2","description",45m,"/",Guid.Empty);
        
        cart.Add(product);
        Assert.Single(cart.Items);
        var cartItem = cart.Items.First();
        cart.Add(product);
        Assert.Equal(2d,cartItem.Quantity);
    }

    [Fact]
    public void Addition_more_than_1000_is_impossible()
    {
        // Arrange
        var cart = new Cart(Guid.Empty, Guid.Empty, new List<CartItem>());
        
        // Act
        var product = new Product("something","description",45m,"/",Guid.Empty);
        
        //Assert
        for (var i = 0; i < 999; i++)
        {
            cart.Add(product);
        }
        Assert.NotNull(cart);
        Assert.Throws<InvalidOperationException>(() =>
        {
            for (var i = 0; i < 2; i++)
            {
                cart.Add(product);
            }
        });
    }
    
    
    
}