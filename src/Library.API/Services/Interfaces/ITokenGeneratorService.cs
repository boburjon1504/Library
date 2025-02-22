using Library.Models.Entities;

namespace Library.API.Services.Interfaces;

public interface ITokenGeneratorService
{
    string GenerateToken(User user);
}
