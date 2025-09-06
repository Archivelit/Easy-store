namespace Store.Infrastructure.Data.Seeders;

public sealed class ItemSeeder
{
    public void SeedItems(AppDbContext context)
    {
        AddItemsIfNotExists(context);

        context.SaveChanges();
    }

    public async Task SeedItemsAsync(AppDbContext context, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        AddItemsIfNotExists(context);

        await context.SaveChangesAsync(cancellationToken);
    }

    private void AddItemsIfNotExists(AppDbContext context)
    {
        var item1 = context.Items.FirstOrDefault(i => i.Id.Equals(Guid.Parse("11111111-1111-1111-1111-111111111111")) &&
            i.Title == "Gaming Laptop");

        if (item1 == null)
        {
            AddItem(context, new("Gaming Laptop",
                1999.99m,
                5,
                Guid.Parse("11111111-1111-1111-1111-111111111111"),
                "High-end gaming laptop with RTX 4090 and 32GB RAM"
            ));
        }

        var item2 = context.Items.FirstOrDefault(i => i.Id.Equals(Guid.Parse("22222222-2222-2222-2222-222222222222")) &&
            i.Title == "Mechanical Keyboard");

        if (item2 == null)
        {
            AddItem(context, new("Mechanical Keyboard",
                129.50m,
                20,
                Guid.Parse("22222222-2222-2222-2222-222222222222"),
                "RGB mechanical keyboard with blue switches"
            ));
        }

    }
    private void AddItem(AppDbContext context, ItemEntity item)
    {
        context.Items.Add(item);
    }
}