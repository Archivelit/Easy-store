using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Data.EntityConfigurations;

internal class UserEntityConfig : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name);
        builder.Property(u => u.Email);
        builder.Property(u => u.SubscriptionType);
    }

}
