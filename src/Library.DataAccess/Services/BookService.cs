using Library.DataAccess.Repositories.Interfaces;
using Library.DataAccess.Services.Interfaces;
using Library.Models.Common;
using Library.Models.Common.ForEntity;
using Library.Models.Entities;
using System.Linq.Expressions;

namespace Library.DataAccess.Services;

public class BookService(IBookRepository repository) : IBookServie
{
    public IQueryable<Book> Get(Expression<Func<Book, bool>> expression) => repository.Get(expression);

    public ValueTask<IList<string>> GetAsync(FilterModel filterModel, BookSortingModel sortingModel, 
        PaginationModel paginationModel, CancellationToken cancellationToken = default)
    {
        return repository.GetAsync(filterModel, sortingModel, paginationModel, cancellationToken);
    }

    public ValueTask<Book?> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return repository.GetByTitleAsync(title, cancellationToken);
    }

    public ValueTask<Book> CreateAsync(Book book, CancellationToken cancellationToken = default)
    {
        return repository.CreateAsync(book, cancellationToken);
    }

    public ValueTask<Book> UpdateAsync(Book book, CancellationToken cancellationToken = default)
    {
        return repository.UpdateAsync(book, cancellationToken);
    }

    public async ValueTask<Book> DeleteAsync(string title, CancellationToken cancellationToken = default)
    {
        var book = await GetByTitleAsync(title, cancellationToken);

        return await repository.DeleteAsync(book);
    }

    public ValueTask<int> BulkDeleteAsync(IList<Guid> ids, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
