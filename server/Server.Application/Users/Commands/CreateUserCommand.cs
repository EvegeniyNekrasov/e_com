using MediatR;
namespace Server.Application.Users.Commands;
public sealed record CreateUserCommand(string FirstName, string LastName, string Email, string Password) : IRequest<Guid>;