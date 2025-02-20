using Library.Models.Common.ForEntity;

namespace Library.Models.Entities;

public class Book : Entity
{
    public string Title { get; set; } = default!;

    public int PublishedYear { get; set; }

    public string Author { get; set; } = default!;

    public int ViewsCount { get; set; }
}
