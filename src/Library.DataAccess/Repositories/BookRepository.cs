using Library.DataAccess.DataContext;
using Library.DataAccess.Extensions;
using Library.DataAccess.Repositories.Interfaces;
using Library.Models.Common;
using Library.Models.Common.ForEntity;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.DataAccess.Repositories;

public class BookRepository(AppDbContext dbContext) : IBookRepository
{
    public IQueryable<Book> Get(Expression<Func<Book, bool>> expression = default!)
    {
        return expression is null ? dbContext.Books.AsQueryable() :  dbContext.Books.Where(expression).AsQueryable();
    }

    public async ValueTask<IList<string>> GetAsync(FilterModel filterModel, BookSortingModel sortingModel,
        PaginationModel paginationModel, CancellationToken cancellationToken = default)
    {
        return await Get()
                                       .ApplyFiltering(filterModel)
                                       .ApplySorting(sortingModel)
                                       .ApplyPagination(paginationModel)
                                       .Select(b => b.Title)
                                       .ToListAsync(cancellationToken);
    }

    public async ValueTask<Book?> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return await Get(b => b.Title.Equals(title)).FirstOrDefaultAsync(cancellationToken);
    }

    public async ValueTask<Book> CreateAsync(Book book, CancellationToken cancellationToken = default)
    {
        var created = await dbContext.Books.AddAsync(book, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return book;
    }

    public async ValueTask<Book> UpdateAsync(Book book, CancellationToken cancellationToken = default)
    {
        dbContext.Books.Update(book);

        await dbContext.SaveChangesAsync(cancellationToken);

        return book;
    }

    public async ValueTask<Book> DeleteAsync(Book book, CancellationToken cancellationToken = default)
    {
        book.IsDeleted = true;

        await dbContext.SaveChangesAsync(cancellationToken);

        return book;
    }

    public async ValueTask<int> BulkDeleteAsync(IList<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await Get()
            .Where(b => ids.Any(id => id == b.Id))
            .ExecuteUpdateAsync(x => x.SetProperty(b => b.IsDeleted, true), cancellationToken);
    }

}
