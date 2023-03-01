using OnlineStore.Data.UnitOfWork;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.Services;

public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    
    
    
    public virtual async Task<IReadOnlyList<Product>> GetProducts(CancellationToken ctsToken = default)
    {
        var product = await _unitOfWork.ProductRepository.GetAll(ctsToken);
        return product;
    }

    
    public virtual async Task<Product> GetProduct(Guid id , CancellationToken ctsToken = default)
    {
        var product = await _unitOfWork.ProductRepository.GetById(id, ctsToken);
        return product;
    }

    
    public virtual async Task AddProduct(Product product, CancellationToken ctsToken = default)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        await _unitOfWork.ProductRepository.Add(product, ctsToken);
        await _unitOfWork.CommitAsync(ctsToken);
    }

    
    public virtual async Task<Product> UpdateProduct(Product product, CancellationToken ctsToken = default)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        await _unitOfWork.ProductRepository.Update(product, ctsToken);
        await _unitOfWork.CommitAsync(ctsToken);
        return product;
    }

    
    public virtual async Task<IReadOnlyList<Product>> GetByName(string name, CancellationToken ctsToken = default)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        var accountName = await _unitOfWork.ProductRepository.FindByName(name, ctsToken);
        return accountName;
    }


    
    public virtual async Task<Product> DeleteProduct(Guid id, CancellationToken ctsToken)
    {
        var deleteProduct = await _unitOfWork.ProductRepository.DeleteById(id, ctsToken);
        await _unitOfWork.CommitAsync(ctsToken);
        return deleteProduct;
    }
}