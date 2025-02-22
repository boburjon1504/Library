using Library.Models.Common.ForEntity;
using Library.Models.Entities;
using System.Linq.Expressions;

namespace Library.API.Services.Interfaces;

public interface IUserService
{
    IQueryable<User> Get(Expression<Func<User, bool>> expression = default!);

    ValueTask<IList<string>> GetAsync(PaginationModel paginationModel, CancellationToken cancellationToken = default);

    ValueTask<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

    ValueTask<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<User> CreateAsync(User user, CancellationToken cancellationToken = default);

    ValueTask<User> UpdateAsync(User user, CancellationToken cancellationToken = default);

    ValueTask<User> DeleteAsync(string user, CancellationToken cancellationToken = default);

    ValueTask<int> BulkDeleteAsync(IList<Guid> ids, CancellationToken cancellationToken = default);
}
