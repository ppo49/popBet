using BetWebApp_2.Models;
using Microsoft.AspNetCore.Identity;

namespace BetWebApp_2.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<User> _hasher = new();

        public string Hash(User user, string password)
            => _hasher.HashPassword(user, password);

        public bool Verify(User user, string password)
        {
            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result != PasswordVerificationResult.Failed;
        } 
    }
}
