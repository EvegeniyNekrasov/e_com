using MediatR;
using Server.Application.Abstractions;
using Server.Domain.Entities;

namespace Server.Application.Users.Queries;
public sealed class GetUserHandler(IUserRepository repo) : IRequestHandler<GetUserQuery, IEnumerable<User>>
{
    public async Task<IEnumerable<User>> Handle(GetUserQuery q, CancellationToken ct) => await repo.ListAsync(ct);
}
