namespace Store.Core.Models.Entities;

public class UserCredentialsEntity : IUserCredentials
{
    [Key] [Required] public Guid Id { get; }
    [Required] public Role Role { get; }
    [Required] public string PasswordHash { get; }

    [JsonConstructor]
    public UserCredentialsEntity(Guid id, Role role, string passwordHash)
    {
        Id = id;
        Role = role;
        PasswordHash = passwordHash;
    }

    public UserCredentialsEntity(IUserCredentials userCredentials) 
        : this(userCredentials.Id, userCredentials.Role, userCredentials.PasswordHash) { }

    // For ef core only
#pragma warning disable CS8618
    public UserCredentialsEntity() { }
}
