using Library.Models.Common;
using Library.Models.Common.ForEntity;
using Library.Models.Entities;
using System.Linq.Expressions;

namespace Library.DataAccess.Repositories.Interfaces;

public interface IBookRepository
{
    IQueryable<Book> Get(Expression<Func<Book, bool>> expression);

    ValueTask<IList<string>> GetAsync(FilterModel filterModel, BookSortingModel sortingModel, PaginationModel paginationModel, CancellationToken cancellationToken = default);

    ValueTask<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Book?> GetByTitleAsync(string title, CancellationToken cancellationToken = default);

    ValueTask<Book> CreateAsync(Book book, CancellationToken cancellationToken = default);

    ValueTask<Book> UpdateAsync(Book book, CancellationToken cancellationToken = default);

    ValueTask<Book> DeleteAsync(Book book, CancellationToken cancellationToken = default);

    ValueTask<int> BulkDeleteAsync(IList<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<int> BulkDeleteAsync(IList<string> titles, CancellationToken cancellationToken = default);

}
