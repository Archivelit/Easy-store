namespace Store.Core.Models.Domain;

public class UserCredentials : IUserCredentials
{
    [Required] public Guid Id { get; }
    [Required] public Role Role { get; }
    [Required] public string PasswordHash { get; }

    [JsonConstructor]
    public UserCredentials(Guid id, Role role, string passwordHash)
    {
        Id = id;
        Role = role;
        PasswordHash = passwordHash;
    }

    public UserCredentials(IUserCredentials userCredentials)
        : this(userCredentials.Id, userCredentials.Role, userCredentials.PasswordHash) { }
}
