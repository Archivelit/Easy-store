
namespace Store.Infrastructure.Data.EntityConfigurations;

internal class UserCredentialsEntityConfig : IEntityTypeConfiguration<UserCredentialsEntity>
{
    public void Configure(EntityTypeBuilder<UserCredentialsEntity> builder)
    {
        builder.ToTable("UserCredentials");

        builder.HasKey(uc => uc.Id);

        builder.Property(uc => uc.Role)
            .IsRequired();

        builder.Property(uc => uc.PasswordHash)
            .IsRequired();
    }
}
