namespace Store.Infrastructure.Data.EntityConfigurations;

internal class ItemEntityConfig : IEntityTypeConfiguration<ItemEntity>
{
    public void Configure(EntityTypeBuilder<ItemEntity> builder)
    {
        builder.ToTable("Items");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Title);
        builder.Property(i => i.Description);
        builder.Property(i => i.Price);
        builder.Property(i => i.QuantityInStock);
        builder.Property(i => i.UserId);
        builder.Property(i => i.CreatedAt);
        builder.Property(i => i.UpdatedAt);
    }
}