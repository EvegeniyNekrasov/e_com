

using MediatR;
using Server.Application.DTOs;

namespace Server.Application.Auth.Commands;
public record LoginCommand(LoginDto LoginDto) : IRequest<string>;
