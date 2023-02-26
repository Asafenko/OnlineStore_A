namespace OnlineStore.Domain.Entities;

public record Account : IEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Guid Id { get; init; }
    //public string[] Roles { get; set; }
    

    public Account() { }

    public Account(string name ,string email,string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
    public Account(string name ,string email,string password,Guid id)
    {
        Name = name;
        Email = email;
        Password = password;
        Id = id;
    }
}