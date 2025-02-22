using Library.DataAccess.Repositories.Interfaces;
using Library.DataAccess.Services.Interfaces;
using Library.Models.Common.ForEntity;
using Library.Models.Entities;
using System.Linq.Expressions;

namespace Library.DataAccess.Services;

public class UserService(IUserRepository repository) : IUserService
{
    public IQueryable<User> Get(Expression<Func<User, bool>> expression)
    {
        return repository.Get(expression);
    }

    public ValueTask<IList<string>> GetAsync(PaginationModel paginationModel, CancellationToken cancellationToken = default)
    {
        return repository.GetAsync(paginationModel, cancellationToken);
    }

    public ValueTask<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return repository.GetByUsernameAsync(username, cancellationToken);
    }

    public ValueTask<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        return repository.CreateAsync(user, cancellationToken);
    }

    public ValueTask<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        return repository.UpdateAsync(user, cancellationToken);
    }

    public ValueTask<User> DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        return repository.DeleteAsync(user, cancellationToken);
    }

    public ValueTask<int> BulkDeleteAsync(IList<string> usernames, CancellationToken cancellationToken = default)
    {
        return repository.BulkDeleteAsync(usernames, cancellationToken);
    }
}
