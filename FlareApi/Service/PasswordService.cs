using FlareApi.Entities;
using FlareApi.Service.Driver;
using Microsoft.AspNetCore.Identity;
using StringRandomizer;
using StringRandomizer.Options;

namespace FlareApi.Service
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<User> _hasher = new();

        public string HashPassword(User user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyHashedPassword(
            User user,
            string hashedPassword,
            string providedPassword)
        {
            return _hasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        }

        public IPasswordService.HashedResult GeneratePassword(User user)
        {
            string password = new Randomizer(10, new DefaultRandomizerOptions(
                hasSpecialChars: true,
                hasLowerAlphabets: true,
                hasUpperAlphabets: true,
                hasNumbers: true
            )).Next();

            var hashed = _hasher.HashPassword(user, password);
            return new IPasswordService.HashedResult
            {
                Hashed = hashed,
                PlainText = password,
            };
        }
    }
}