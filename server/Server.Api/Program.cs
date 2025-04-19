using Serilog;
using Server.Application.Abstractions;
using Server.Application.Users.Commands;
using Server.Infrastructure.Persistence;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Server.Domain.Contracts;
using Server.Infrastructure.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

var jwtSettings = builder.Configuration.GetSection("Jwt");

var services = builder.Services;

services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>());
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IJwtProvider, JwtProvider>();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
        };
    });

services.AddCors(c => c.AddDefaultPolicy(p =>
        p.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()));

var app = builder.Build();

app.UseCors();


SqlMapper.AddTypeHandler(new SqliteGuidTypeHandler());
SqlMapper.AddTypeHandler(new SqliteDateTimeOffsetHandler());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
