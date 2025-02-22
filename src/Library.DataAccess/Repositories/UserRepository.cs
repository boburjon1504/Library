using Library.DataAccess.Repositories.Interfaces;
using Library.Models.Common.ForEntity;
using Library.Models.Common;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Library.DataAccess.DataContext;
using Library.DataAccess.Extensions;
namespace Library.DataAccess.Repositories;

public class UserRepository(AppDbContext dbContext) : BaseRepository<User>(dbContext), IUserRepository
{
    public new IQueryable<User> Get(Expression<Func<User, bool>> expression = default!)
    {
        return base.Get(expression);
    }

    public async ValueTask<IList<string>> GetAsync(PaginationModel paginationModel, CancellationToken cancellationToken = default)
    {
        return await Get()
                           .ApplyPagination(paginationModel)
                           .Select(b => b.Username)
                           .ToListAsync(cancellationToken);
    }

    public async ValueTask<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await Get(b => b.Username.Equals(username)).FirstOrDefaultAsync(cancellationToken);
    }

    public new ValueTask<User> CreateAsync(User book, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(book, cancellationToken);
    }

    public new ValueTask<User> UpdateAsync(User book, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(book, cancellationToken);
    }

    public new ValueTask<User> DeleteAsync(User book, CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(book, cancellationToken);
    }

    public async ValueTask<int> BulkDeleteAsync(IList<string> usernames, CancellationToken cancellationToken = default)
    {
        return await Get()
            .Where(user => usernames.Any(u => u == user.Username))
            .ExecuteUpdateAsync(x => x.SetProperty(b => b.IsDeleted, true), cancellationToken);
    }
}
