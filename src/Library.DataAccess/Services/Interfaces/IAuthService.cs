using Library.Models.Entities;

namespace Library.DataAccess.Services.Interfaces;

public interface IAuthService
{
    ValueTask<bool> RegisterAsync(User user, CancellationToken cancellationToken = default);

    ValueTask<string> LoginAsync(string username, CancellationToken cancellationToken = default);
}
