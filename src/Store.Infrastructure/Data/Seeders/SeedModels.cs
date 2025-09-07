namespace Store.Infrastructure.Data.Seeders;

public static class SeedModels
{
    private static readonly DateTime ItemCreatedAt1 = new DateTime(2025, 09, 07, 20, 01, 54, DateTimeKind.Utc);
    private static readonly DateTime ItemCreatedAt2 = new DateTime(2025, 09, 07, 20, 01, 54, DateTimeKind.Utc);
    
    public static ItemEntity Item1 { get; } = new(
        id: Guid.Parse("11111111-1111-1111-1111-111111111111"),
        title: "Gaming Laptop",
        price: 1999.99m,
        quantityInStock: 5,
        userId: Guid.Parse("11111111-1111-1111-1111-111111111111"),
        description: "High-end gaming laptop with RTX 4090 and 32GB RAM",
        createdAt: ItemCreatedAt1,
        updatedAt: null
    );

    public static ItemEntity Item2 { get; } = new(
        id: Guid.Parse("22222222-2222-2222-2222-222222222222"),
        title: "Mechanical Keyboard",
        price: 129.50m,
        quantityInStock: 20,
        userId: Guid.Parse("22222222-2222-2222-2222-222222222222"),
        description: "RGB mechanical keyboard with blue switches",
        createdAt: ItemCreatedAt2,
        updatedAt: null
    );
    public static UserEntity User1 { get; } = new(
        Guid.Parse("11111111-1111-1111-1111-111111111111"),
        "Alice Johnson",
        "alice.johnson@example.com",
        Subscription.StorePlus
    );
    public static UserEntity User2 { get; } = new(
        Guid.Parse("22222222-2222-2222-2222-222222222222"),
        "Bob Smith",
        "bob.smith@example.com",
        Subscription.None
    );
}
