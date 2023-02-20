using OnlineStore.Data;
using OnlineStore.Data.UnitOfWork;

namespace OnlineStore.Domain.Services;

public class CartService
{
    private readonly IUnitOfWork _unitOfWork;

    public CartService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }



    public virtual async Task<IEnumerable<Cart>> GetCarts( CancellationToken ctsToken = default)
    {
        var cart = await _unitOfWork.CartRepository.GetAll(ctsToken);
        return cart;
    }
}