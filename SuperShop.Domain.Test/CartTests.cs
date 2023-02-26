using OnlineStore.Data;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Services;

namespace SuperShop.Domain.Test;

public class CartTests
{
    [Fact]
    public void New_item_is_added_to_cart()
    {
        // Arange
        var cart = new Cart()
        {
            Id = Guid.Empty,
            AccountId = Guid.Empty,
            Items = new List<CartItem>(),
        };
        
        // Act
        var product = new Product("something","description",45m,"/",Guid.Empty);
        cart.Add(product);
        
        // Assert
        Assert.Single(cart.Items);
        var cartItem = cart.Items[0];
        Assert.NotNull(cartItem);
        Assert.Equal(product.Id,cartItem.ProductId);
        Assert.Equal(1d,cartItem.Quantity);
    }
    [Fact]
    public void Adding_empty_product_is_impossible()
    {
        // Arange
        var cart = new Cart()
        {
            Id = Guid.Empty,
            AccountId = Guid.Empty,
            Items = new List<CartItem>()
        };

        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => cart.Add(null));
    }

    [Fact]
    public void Adding_existed_product_to_cart_changes_quantity()
    {
        // Arrange
        var cart = new Cart()
        {
            Id = Guid.Empty,
            AccountId = Guid.Empty,
            Items = new List<CartItem>()
        };
        
        // Act and  Assert
        var product = new Product("something","description",45m,"/",Guid.Empty);
        //var product2 = new Product("something2","description",45m,"/",Guid.Empty);
        
        cart.Add(product);
        
        Assert.NotNull(cart);
        
        Assert.Single(cart.Items);
        
        var cartItem = cart.Items[0];
        
        cart.Add(product);
        
        Assert.NotNull(cart);
        
        Assert.Equal(2d,cartItem.Quantity);
    }

    [Fact]
    public void Addition_more_than_1000_is_impossible()
    {
        // Arrange
        var cart = new Cart()
        {
            Id = Guid.Empty,
            AccountId = Guid.Empty,
            Items = new List<CartItem>()
        };
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

    [Fact]
    public void Price_is_not_negative()
    {
        // Arrange
        var cart = new Cart()
        {
            Id = Guid.Empty,
            AccountId = Guid.Empty,
            Items = new List<CartItem>()
        };
        // Act
        var product = new Product("something","description",0m,"/",Guid.Empty);
        Assert.Throws<InvalidOperationException>(() =>cart.Add(product) );
        
        
    }
    
    
}