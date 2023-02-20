using OnlineStore.Data;
using OnlineStore.Data.UnitOfWork;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.Services;

public class CartService
{
    private readonly IUnitOfWork _unitOfWork;

    public CartService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task AddItem(Guid accountId, Product product, CancellationToken ctsToken, double quantity = 1d)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        if(quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        var cart = await _unitOfWork.CartRepository.GetCartByAccountId(accountId,ctsToken);
        await AddItem(cart, product,ctsToken, quantity);
    }

    private async Task AddItem(Cart cart, Product product, CancellationToken ctsToken, double quantity = 1d)
    {
        if (cart == null) throw new ArgumentNullException(nameof(cart));
        if (product == null) throw new ArgumentNullException(nameof(product));
        if(quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        
        var existedItem = cart.Items.SingleOrDefault(it => it.ProductId == product.Id);
        if (existedItem is not null)
        {
            existedItem.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(new CartItem()
            {
                ProductId = product.Id, Quantity = quantity, Price = product.Price
            });
        }

        await _unitOfWork.CartRepository.Update(cart,ctsToken);
        await _unitOfWork.CommitAsync(ctsToken);
    }


    public virtual async Task<IEnumerable<Cart>> GetCarts( CancellationToken ctsToken = default)
    {
        var cart = await _unitOfWork.CartRepository.GetAll(ctsToken);
        return cart;
    }

    public virtual async Task<Cart> GetCartByAccount(Guid account ,CancellationToken ctsToken)
    {
        return await _unitOfWork.CartRepository.GetCartByAccountId(account, ctsToken);
    }
}