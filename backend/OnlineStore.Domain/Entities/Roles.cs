namespace OnlineStore.Domain.Entities;

public static class Roles
{
    public const string Admin = "Admin";
    public const string Buyer = "Buyer";

    public static class Defaults
    {
        public static string[] Buyers { get; } = { Buyer };
        public static string[] Admins { get; } = { Admin };
    }
}