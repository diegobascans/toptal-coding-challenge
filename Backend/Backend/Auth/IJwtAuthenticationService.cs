using Backend.Models;

namespace Backend.Auth
{
    public interface IJwtAuthenticationService
    {
        string Authenticate(User user);
    }
}
