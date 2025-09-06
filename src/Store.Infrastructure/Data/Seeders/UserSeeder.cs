namespace Store.Infrastructure.Data.Seeders;

public sealed class UserSeeder
{
    public void SeedUsers(AppDbContext context)
    {
        AddUsersIfNotExists(context);

        context.SaveChanges();
    }

    public async Task SeedUsersAsync(AppDbContext context, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        AddUsersIfNotExists(context);

        await context.SaveChangesAsync();
    }

    private void AddUsersIfNotExists(AppDbContext context)
    {
        if (!context.Users.Any(u => u.Email.Trim() == "alice.johnson@example.com"))
        {
            context.Users.Add(new UserEntity(
                Guid.Parse("11111111-1111-1111-1111-111111111111"),
                "Alice Johnson",
                "alice.johnson@example.com",
                Subscription.StorePlus
            ));
        }

        if (!context.Users.Any(u => u.Email.Trim() == "bob.smith@example.com"))
        {
            context.Users.Add(new UserEntity(
                Guid.Parse("22222222-2222-2222-2222-222222222222"),
                "Bob Smith",
                "bob.smith@example.com",
                Subscription.None
            ));
        }
    }
}