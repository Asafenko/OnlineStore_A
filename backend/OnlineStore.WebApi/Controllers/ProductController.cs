using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoriesInterfaces;


namespace OnlineStore.WebApi.Controllers;

[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    //products/get_all
    [HttpGet("get_all")]
    public async Task<IEnumerable<Product>> GetProducts(CancellationToken cts)
    {
        var product = await _productRepository.GetAll(cts);
        return product;
    }

    //products/get_by_id
    [HttpGet("get_by_id/id={id:guid}")]
    public async Task<Product> GetProduct(Guid id, CancellationToken cts)
    {
        var product = await _productRepository.GetById(id, cts);
        return product;
    }

    //products/add
    [HttpPost("add")]
    public async Task AddProduct(Product product, CancellationToken cts)
    {
        await _productRepository.Add(product, cts);
    }

    //products/update
    [HttpPut("update")]
    public async Task UpdateProduct([FromBody] Product product, CancellationToken cts)
    {
        await _productRepository.Update(product, cts);
    }

    //products/get_by_name
    [HttpGet("get_by_name/name={name}")]
    public async Task<IReadOnlyList<Product>> GetByName(string name, CancellationToken cts = default)
    {
        var accountName = await _productRepository.FindByName(name, cts);
        return accountName;
    }


    //products/delete_by_id
    [HttpDelete("delete_by_id/id={id:guid}")]
    public async Task DeleteProduct(Guid id, CancellationToken cts)
    {
        await _productRepository.DeleteById(id, cts);
    }

    //products/delete
    // [HttpDelete("delete")]
    // public async Task Delete(CancellationToken cts)
    // {
    //     await _dbContext.Delete(cts);
    // }
}