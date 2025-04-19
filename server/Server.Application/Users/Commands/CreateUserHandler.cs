
using MediatR;
using Server.Application.Abstractions;
using Server.Domain.Entities;

namespace Server.Application.Users.Commands;
public sealed class CreateUserHandler(IUserRepository repo) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand cmd, CancellationToken ct)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(cmd.Password);
        var user = new User
        {
            FirstName = cmd.FirstName,
            LastName = cmd.LastName,
            Email = cmd.Email,
            Password = hashedPassword
        };
        await repo.AddAsync(user, ct);
        return user.Id;
    }
}
