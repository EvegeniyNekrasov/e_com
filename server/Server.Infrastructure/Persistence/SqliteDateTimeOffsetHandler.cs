
using System.Data;
using Dapper;

namespace Server.Infrastructure.Persistence;
public class SqliteDateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
{
    public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
       => parameter.Value = value.ToString("O");

    public override DateTimeOffset Parse(object value)
       => DateTimeOffset.Parse(value.ToString()!);
}
