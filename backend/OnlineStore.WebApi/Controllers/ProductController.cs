using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Services;


namespace OnlineStore.WebApi.Controllers;

[Route("products")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;
    public ProductController(ProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    //products/get_all
    [HttpGet("get_all")]
    public async Task<IReadOnlyList<Product>> GetAllProducts(CancellationToken ctsToken = default)
    {
        var products = await _productService.GetProducts(ctsToken);
        return products;
    }
    
    //products/get_by_id
    [HttpGet("get_by_id/id={id:guid}")]
    public async Task<Product> GetProduct( Guid id, CancellationToken ctsToken = default)
    {
        var product = await _productService.GetProduct(id, ctsToken);
        return product;
    }

    //products/add
    [HttpPost("add")]
    public async Task AddProduct(Product product, CancellationToken ctsToken = default)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        await _productService.AddProduct(product, ctsToken);
    }

    //products/update
    [HttpPut("update")]
    public async Task UpdateProduct(Product product, CancellationToken ctsToken = default)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        await _productService.UpdateProduct(product, ctsToken);
    }
    
    //products/get_by_name
    [HttpGet("get_by_name/name={name}")]
    public async Task<IReadOnlyList<Product>> GetProductByName(string name,CancellationToken ctsToken = default)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        var accountsName = await _productService.GetByName(name, ctsToken);
        return accountsName;
    }
    
    //products/delete_by_id
    [HttpDelete("delete_by_id/id={id:guid}")]
    public async Task DeleteProduct(Guid id,CancellationToken ctsToken = default)
    {
        await _productService.DeleteProduct(id, ctsToken);
    }
}