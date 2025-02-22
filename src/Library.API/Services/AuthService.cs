using Library.API.Extensions;
using Library.API.Services.Interfaces;
using Library.DataAccess.Extensions;
using Library.Models.Common;
using Library.Models.Entities;

namespace Library.API.Services;

public class AuthService(
    IUserService userService, 
    ITokenGeneratorService tokenGenerator,
    IPasswordHasher passwordHasher) : IAuthService
{
    public async ValueTask<bool> RegisterAsync(User user, CancellationToken cancellationToken = default)
    {
        var foundUser = await userService.GetByUsernameAsync(user.Username).GetResultAsync();

        if(foundUser.IsSuccess)
            throw new ArgumentException($"Username ({user.Username}) is already exist");

        user.Password = passwordHasher.HashPassword(user.Password);

        var result = await userService.CreateAsync(user, cancellationToken);

        return result is not null;
    }

    public async ValueTask<string> LoginAsync(User user, CancellationToken cancellationToken = default)
    {
        var result = await userService.GetByUsernameAsync(user.Username, cancellationToken).GetResultAsync();


        ThrowIfLoginFails(result.IsSuccess);

        var passwordVerified = passwordHasher.VerifyPassword(user.Password, result.Data!.Password);

        ThrowIfLoginFails(passwordVerified);

        var token = tokenGenerator.GenerateToken(result.Data);

        return token;
    }

    private void ThrowIfLoginFails(bool isValidLogin)
    {
        if (!isValidLogin)
            throw new InvalidOperationException("Username or password is wrong");
    }
}
