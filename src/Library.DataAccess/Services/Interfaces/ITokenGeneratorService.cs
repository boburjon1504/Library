using Library.Models.Entities;

namespace Library.DataAccess.Services.Interfaces;

public interface ITokenGeneratorService
{
    string GenerateToken(User user);
}
