using FluentValidation;
using Library.API.Extensions;
using Library.API.Services.Interfaces;
using Library.DataAccess.Repositories.Interfaces;
using Library.Models.Common;
using Library.Models.Common.ForEntity;
using Library.Models.Entities;
using System.Linq.Expressions;

namespace Library.API.Services;

public class BookService(IBookRepository repository, IValidator<Book> validator) : IBookServie
{
    public IQueryable<Book> Get(Expression<Func<Book, bool>> expression) => repository.Get(expression);

    public ValueTask<IList<string>> GetAsync(FilterModel filterModel, BookSortingModel sortingModel, 
        PaginationModel paginationModel, CancellationToken cancellationToken = default)
    {
        return repository.GetAsync(filterModel, sortingModel, paginationModel, cancellationToken);
    }

    public async ValueTask<Book> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await repository.GetByIdAsync(id, cancellationToken) ?? 
                                                    throw new ArgumentException($"Book does not exist with id - {id}");

        return book;
    }

    public async ValueTask<Book> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        var book = await repository.GetByTitleAsync(title, cancellationToken) ?? 
                                                    throw new ArgumentException($"Book does not exist with title - {title}");

        return book;
    }

    public async ValueTask<Book> CreateAsync(Book book, CancellationToken cancellationToken = default)
    {
        await validator.ValidateAndThrowAsync(book);

        return await repository.CreateAsync(book, cancellationToken);
    }

    public async ValueTask<Book> UpdateAsync(Book book, CancellationToken cancellationToken = default)
    {
        await validator.ValidateAndThrowAsync(book);

        return await repository.UpdateAsync(book, cancellationToken);
    }

    public async ValueTask<Book> DeleteAsync(string title, CancellationToken cancellationToken = default)
    {
        var book = await GetByTitleAsync(title, cancellationToken);

        return await repository.DeleteAsync(book);
    }

    public ValueTask<int> BulkDeleteAsync(IList<Guid> ids, CancellationToken cancellationToken = default)
    {
        return repository.BulkDeleteAsync(ids, cancellationToken);
    }

    public ValueTask<int> BulkDeleteAsync(IList<string> titles, CancellationToken cancellationToken = default)
    {
        return repository.BulkDeleteAsync(titles, cancellationToken);
    }
}
