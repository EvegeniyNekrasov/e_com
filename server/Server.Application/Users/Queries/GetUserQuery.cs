using MediatR;
using Server.Domain.Entities;

namespace Server.Application.Users.Queries;
public sealed record GetUserQuery() : IRequest<IEnumerable<User>>;
