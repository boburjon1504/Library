using Library.Models.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Common;

public class BookSortingModel
{
    [EnumDataType(typeof(BookSortBy))]
    public BookSortBy SortBy { get; set; }

    public bool IsAscending { get; set; }
}
