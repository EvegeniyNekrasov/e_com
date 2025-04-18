
using System.Data;
using Dapper;
namespace Server.Infrastructure.Persistence;
public class SqliteGuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(IDbDataParameter parameter, Guid value)
      => parameter.Value = value.ToString();

    public override Guid Parse(object value)
        => Guid.Parse(value.ToString()!);
}
