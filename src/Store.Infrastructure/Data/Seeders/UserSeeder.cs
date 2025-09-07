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

        await context.SaveChangesAsync(cancellationToken);
    }

    private void AddUsersIfNotExists(AppDbContext context)
    {
        if (!context.Users.Any(u => u.Email.Trim() == "alice.johnson@example.com"))
        {
            context.Users.Add(SeedModels.User1);
        }

        if (!context.Users.Any(u => u.Email.Trim() == "bob.smith@example.com"))
        {
            context.Users.Add(SeedModels.User2);
        }
    }
}