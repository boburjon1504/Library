using Library.Models.Common.ForEntity;
using Library.Models.Common;
using Library.Models.Entities;
using System.Linq.Expressions;

namespace Library.DataAccess.Repositories.Interfaces;

public interface IUserRepository
{
    IQueryable<User> Get(Expression<Func<User, bool>> expression);

    ValueTask<IList<string>> GetAsync(PaginationModel paginationModel, CancellationToken cancellationToken = default);

    ValueTask<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

    ValueTask<User> CreateAsync(User user, CancellationToken cancellationToken = default);

    ValueTask<User> UpdateAsync(User user, CancellationToken cancellationToken = default);

    ValueTask<User> DeleteAsync(User user, CancellationToken cancellationToken = default);

    ValueTask<int> BulkDeleteAsync(IList<string> usernames, CancellationToken cancellationToken = default);
}
