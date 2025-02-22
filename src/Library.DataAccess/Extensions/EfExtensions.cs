using Library.Models.Common;
using Library.Models.Common.Enums;
using Library.Models.Common.ForEntity;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.DataAccess.Extensions;

public static class EfExtensions
{
    public static IQueryable<TEntity> ApplyPagination<TEntity>(this IQueryable<TEntity> entities, PaginationModel page)
    {
        return entities.Skip((page.CurrentPage - 1) * PaginationModel.PageSize).Take(PaginationModel.PageSize);
    }

    public static IQueryable<Book> ApplyFiltering(this IQueryable<Book> books, FilterModel filter)
    {
        var query = books;

        query = filter.IncludeDeletedBooks ?  query : query.Where(x => !x.IsDeleted);

        query.Where(x => x.PublishedYear >= filter.MinYear && x.PublishedYear <= filter.MaxYear);

        return query;

    }

    public static IQueryable<Book> ApplySorting(this IQueryable<Book> books, BookSortingModel sortingModel)
    {

        IOrderedQueryable<Book> order = books.OrderBy(x => x.Id);


        if (sortingModel.SortBy == BookSortBy.Popularity)
        {
            Expression<Func<Book, double>> expression = (book) => book.ViewsCount * 0.5 + (DateTime.Now.Year - book.PublishedYear) * 2;

            order = sortingModel.IsAscending ? order.OrderBy(expression): 
                                               order.OrderByDescending(expression);
        }else
        if (sortingModel.SortBy != BookSortBy.Default)
        {
            Expression<Func<Book, double>> expression = (book) => book.ViewsCount * 0.5 + (DateTime.Now.Year - book.PublishedYear) * 2;
            order = sortingModel.IsAscending ?
                                        order.OrderBy(b => EF.Property<Book>(b, sortingModel.SortBy.ToString())) :
                                        order.OrderByDescending(b => EF.Property<Book>(b, sortingModel.SortBy.ToString()));
        }

        return order.AsQueryable();

    }
}
