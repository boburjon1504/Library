using Library.Models.Common;
using Library.Models.Common.Enums;
using Library.Models.Common.ForEntity;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Extensions;

public static class EfExtensions
{
    public static IQueryable<TEntity> ApplyPagination<TEntity>(this IQueryable<TEntity> entities, PaginationModel page)
    {
        return entities.Skip((page.CurrentPage - 1) * page.PageSize).Take(page.PageSize);
    }

    public static IQueryable<Book> ApplyFiltering(this IQueryable<Book> books, FilterModel filter)
    {
        var query = books;

        query = filter.IncludeDeletedBooks ? query : query.Where(x => !x.IsDeleted);

        query.Where(x => x.PublishedYear > filter.MinYear && x.PublishedYear < filter.MaxYear);

        if(!string.IsNullOrEmpty(filter.Title)) query.Where(x => x.Title.Contains(filter.Title));
        if(!string.IsNullOrEmpty(filter.Author)) query.Where(x => x.Author.Contains(filter.Author));

        return query;

    }

    public static IQueryable<Book> ApplySorting(this IQueryable<Book> books, BookSortingModel sortingModel)
    {

        IOrderedQueryable<Book> order = books.OrderBy(x => x.Id);
        
        if(sortingModel.SortBy != BookSortBy.Default)
        {
            order = sortingModel.IsAscending ? 
                                        order.OrderBy(b => EF.Property<Book>(b, nameof(sortingModel.SortBy))) : 
                                        order.OrderByDescending(b => EF.Property<Book>(b, nameof(sortingModel.SortBy)));
        }

        return order.AsQueryable();

    }
}
