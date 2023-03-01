namespace OnlineStore.Domain.Entities;

public record Account : IEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Guid Id { get; init; }
    //public string[] Roles { get; set; }
    

    public Account() { }

    
    public Account(string name ,string email,string passwordHash,Guid id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        Id = id;
    }
}