namespace OnlineStore.Domain.Entities;


public record Product : IEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get;set;}
    public string ImgUri { get; set; }
    public Guid Id { get; init; }
    
    
    public Product() {}
    public Product(string name, string description,decimal price, string img )
    {
        Name = name;
        Description = description;
        Price = price;
        ImgUri = img;
    }
    public Product(string name,string description,decimal price,string img,Guid id)
    {
        Name = name;
        Description = description;
        Price = price;
        ImgUri = img;
        Id = id;
    }
}