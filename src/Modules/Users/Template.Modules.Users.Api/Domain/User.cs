using Template.Shared.Types;

namespace Template.Modules.Users.Api.Domain;

public sealed class User
{
    private readonly List<UserActivationToken> _activationTokens = [];
    private readonly List<UserRefreshToken> _refreshTokens = [];

    private User()
    {
    }

    private User(
        string name,
        string surname,
        Email email,
        Phone phone,
        Password password,
        UserStatus status,
        UserRole role)
    {
        Id = Guid.CreateVersion7();
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
        Password = password;
        Status = status;
        Role = role;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public UserId Id { get; } = null!;
    public string Name { get; private set; } = null!;
    public string Surname { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Phone Phone { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public UserStatus Status { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }
    public IReadOnlyList<UserActivationToken> ActivationTokens => _activationTokens.AsReadOnly();
    public IReadOnlyList<UserRefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    public static User CreateAdmin()
    {
        return new User();
    }

    public static User CreateUser()
    {
        return new User();
    }
}