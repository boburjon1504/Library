using Library.DataAccess.Services.Interfaces;
using Library.Models.Entities;

namespace Library.DataAccess.Services;

public class AuthService(IUserService userService, ITokenGeneratorService tokenGenerator) : IAuthService
{
    public async ValueTask<bool> RegisterAsync(User user, CancellationToken cancellationToken = default)
    {
        var result = await userService.CreateAsync(user, cancellationToken);

        return result is not null;
    }

    public async ValueTask<string> LoginAsync(string username, CancellationToken cancellationToken = default)
    {
        var user = await userService.GetByUsernameAsync(username, cancellationToken);

        var token = tokenGenerator.GenerateToken(user);

        return token;
    }
}
