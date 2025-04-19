namespace Server.Domain.Entities;
public sealed class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string? Password { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public void UpdateFirstName(string firstName) => FirstName = firstName;
    public void UpdateLastName(string lastName) => LastName = lastName;
    public void UpdateEmail(string email) => Email = email;
}
