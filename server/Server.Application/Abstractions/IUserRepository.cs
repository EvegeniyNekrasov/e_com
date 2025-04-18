using Server.Domain.Entities;

namespace Server.Application.Abstractions;
public interface IUserRepository
{
    Task<User?> GetAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<User>> ListAsync(CancellationToken ct);
    Task AddAsync(User user, CancellationToken ct);
}
