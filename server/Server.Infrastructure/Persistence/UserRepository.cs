using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Server.Application.Abstractions;
using Server.Domain.Entities;

namespace Server.Infrastructure.Persistence;
public class UserRepository(IConfiguration cfg) : IUserRepository
{
    private IDbConnection Connection => new SqliteConnection(cfg.GetConnectionString("Default"));

    public async Task AddAsync(User user, CancellationToken ct)
    {
        const string sql = """ 
            INSERT INTO Users (Id, FirstName, LastName, Email, CreatedAt)
            VALUES (@Id, @FirstName, @LastName, @Email, @CreatedAt);
        """;

        await Connection.ExecuteAsync(sql, user);
    }

    public async Task<User?> GetAsync(Guid id, CancellationToken ct)
    {
        const string sql = "SELECT * FROM Users WHERE Id = @id";
        return await Connection.QuerySingleOrDefaultAsync<User>(sql, new { id });
    }

    public async Task<IEnumerable<User>> ListAsync(CancellationToken ct) =>
        await Connection.QueryAsync<User>("SELECT * FROM Users");

}
