using Library.Models.Common.ForEntity;

namespace Library.Models.Entities;

public class Book : Auditable
{
    public string Title { get; set; } = default!;

    public int PublishedYear { get; set; }

    public string Author { get; set; } = default!;

    public int ViewsCount { get; set; }

    public Guid UserId { get; set; }
}
