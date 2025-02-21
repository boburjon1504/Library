namespace Library.Models.Common;

public class FilterModel
{
    public int MaxYear { get; set; } = 9999;

    public int MinYear { get; set; } = 0;

    public bool IncludeDeletedBooks { get; set; }
}
