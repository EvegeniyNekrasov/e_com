
using Server.Domain.Entities;

namespace Server.Domain.Contracts;
public interface IJwtProvider
{
    string GenerateToken(User user);
}