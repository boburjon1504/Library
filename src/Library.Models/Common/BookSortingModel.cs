using Library.Models.Common.Enums;

namespace Library.Models.Common;

public class BookSortingModel
{
    public BookSortBy SortBy { get; set; }

    public bool IsAscending { get; set; }
}
