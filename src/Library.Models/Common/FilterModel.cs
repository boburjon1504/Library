namespace Library.Models.Common;

public class FilterModel
{
    public bool IsAscending { get; set; }

    public string? Author { get; set; }

    public string? Title { get; set; }

    public int MaxYear { get; set; }

    public int MinYear { get; set; }

    public bool IncludeDeletedBooks { get; set; }
}
