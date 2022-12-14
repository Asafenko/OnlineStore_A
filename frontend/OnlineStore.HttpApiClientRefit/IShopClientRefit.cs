namespace OnlineStore.HttpApiClientRefit;

// public interface IShopClientRefit
// {
//     // GET ALL PRODUCTS
//     [Get("/products")]
//     public Task<IReadOnlyList<Product>> GetProducts();
//     
//     // GET PRODUCT BY ID
//     [Get("/product/{id}")]
//     public Task<Product> GetProduct(Guid id);
//
//     // ADD PRODUCT
//     [Post("/add")]
//     public Task AddProduct(Product product);
//
//     // UPDATE PRODUCT BY ID
//     [Put("/product/{id}")]
//     public Task UpdateProduct(Guid id, Product product);
//     
//     // DELETE ALL PRODUCT
//     [Delete("/delete")]
//     public Task Delete();
//     
//     // DELETE PRODUCT BY ID
//     [Delete("/product/{id}")]
//     public Task DeleteProduct(Guid id);
//}