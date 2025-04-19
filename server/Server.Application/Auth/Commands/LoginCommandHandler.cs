
using MediatR;
using Server.Application.Abstractions;
using Server.Domain.Contracts;



namespace Server.Application.Auth.Commands;
public class LoginCommandHandler(
    IUserRepository userRepository,
    IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<string> Handle(LoginCommand request, CancellationToken ct)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.LoginDto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.LoginDto.Password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        return _jwtProvider.GenerateToken(user);

    }
}
