using Library.Models.Common.Enums;
using Library.Models.Common.ForEntity;

namespace Library.Models.Entities;

public class User : Auditable
{
    public string Username { get; set; } = default!;

    public string Password { get; set; } = default!;

    public Role Role { get; set; } = Role.User;

    public ICollection<Book> Books { get; set; }
}
