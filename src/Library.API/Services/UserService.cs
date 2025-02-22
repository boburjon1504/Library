using Library.API.Services.Interfaces;
using Library.DataAccess.Repositories.Interfaces;
using Library.Models.Common.ForEntity;
using Library.Models.Entities;
using System.Linq.Expressions;

namespace Library.API.Services;

public class UserService(IUserRepository repository) : IUserService
{
    public IQueryable<User> Get(Expression<Func<User, bool>> expression = default!)
    {
        return repository.Get(expression);
    }

    public ValueTask<IList<string>> GetAsync(PaginationModel paginationModel, CancellationToken cancellationToken = default)
    {
        return repository.GetAsync(paginationModel, cancellationToken);
    }
    public async ValueTask<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await repository.GetByIdAsync(id, cancellationToken) ??
                                            throw new ArgumentException("User is not found");

        return user;
    }


    public async ValueTask<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
         var user = await repository.GetByUsernameAsync(username, cancellationToken)  ?? 
                                                throw new ArgumentException("User is not found");

        return user;
    }

    public ValueTask<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        return repository.CreateAsync(user, cancellationToken);
    }

    public ValueTask<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        return repository.UpdateAsync(user, cancellationToken);
    }

    public async ValueTask<User> DeleteAsync(string username, CancellationToken cancellationToken = default)
    {
        var user = await GetByUsernameAsync(username, cancellationToken); 

        return await repository.DeleteAsync(user, cancellationToken);
    }

    public ValueTask<int> BulkDeleteAsync(IList<Guid> ids, CancellationToken cancellationToken = default)
    {
        return repository.BulkDeleteAsync(ids, cancellationToken);
    }

}
