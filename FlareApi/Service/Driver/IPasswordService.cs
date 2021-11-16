using System.Threading.Tasks;
using FlareApi.Entities;
using Microsoft.AspNetCore.Identity;

namespace FlareApi.Service.Driver
{
    public interface IPasswordService : IPasswordHasher<User>
    {
        public class HashedResult
        {
            public string PlainText { get; init; } = string.Empty;
            public string Hashed { get; init; } = string.Empty;
        }
        
        public HashedResult GeneratePassword(User user);
    }
}