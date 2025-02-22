namespace Library.API.DTOs.Book;

public class BookDTO
{
    public string Title { get; set; } = default!;

    public int PublishedYear { get; set; }

    public string Author { get; set; } = default!;
}
