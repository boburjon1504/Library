﻿using Library.DataAccess.DataContext;
using Library.DataAccess.Extensions;
using Library.DataAccess.Repositories.Interfaces;
using Library.Models.Common;
using Library.Models.Common.ForEntity;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.DataAccess.Repositories;

public class BookRepository(AppDbContext dbContext) : BaseRepository<Book>(dbContext), IBookRepository
{
    public new IQueryable<Book> Get(Expression<Func<Book, bool>> expression = default!)
    {
        return base.Get(expression);
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
    public new ValueTask<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return base.GetByIdAsync(id, cancellationToken);
    }

    public async ValueTask<Book?> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return await Get(b => b.Title.Equals(title)).FirstOrDefaultAsync(cancellationToken);
    }

    public new ValueTask<Book> CreateAsync(Book book, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(book, cancellationToken);
    }

    public new ValueTask<Book> UpdateAsync(Book book, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(book, cancellationToken);
    }

    public new ValueTask<Book> DeleteAsync(Book book, CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(book, cancellationToken);
    }

    public new ValueTask<int> BulkDeleteAsync(IList<Guid> ids, CancellationToken cancellationToken = default)
    {
        return base.BulkDeleteAsync(ids, cancellationToken);
    }

    public async ValueTask<int> BulkDeleteAsync(IList<string> titles, CancellationToken cancellationToken = default)
    {
        return await Get(x => titles.Any(t => x.Title.Equals(t)))
           .ExecuteUpdateAsync(book => book.SetProperty(b => b.IsDeleted, true), cancellationToken);
    }
}
