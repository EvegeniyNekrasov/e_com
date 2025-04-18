using Serilog;
using Server.Application.Abstractions;
using Server.Application.Users.Commands;
using Server.Infrastructure.Persistence;
using Dapper;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

var services = builder.Services;

services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>());
services.AddScoped<IUserRepository, UserRepository>();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();
SqlMapper.AddTypeHandler(new SqliteGuidTypeHandler());
SqlMapper.AddTypeHandler(new SqliteDateTimeOffsetHandler());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
