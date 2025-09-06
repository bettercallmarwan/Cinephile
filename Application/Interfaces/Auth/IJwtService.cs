using Domain.Entities.Identity;

namespace Application.Interfaces.Auth
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
        bool ValidateToken(string token);
    }
} 