using System;
using System.Threading.Tasks;
using FlareApi.Api.V1.DataAccess;
using FlareApi.Data;
using FlareApi.Entities;
using FlareApi.Service.Driver;
using Microsoft.EntityFrameworkCore;

namespace FlareApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FlareContext _context;
        private readonly ITokenService _tokenService;

        public UserRepository(FlareContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<User?> FindUserAsync(string uen)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u =>u.Department)
                .FirstOrDefaultAsync(u => u.Uen == uen);
        }

        public Task AddFailedAttemptAsync(User user)
        {
            user.FailedAttempts++;
            return _context.SaveChangesAsync();
        }

        public async Task<(string, Guid)> CreateSessionAsync(User user)
        {
            var guid = Guid.NewGuid();
            var token = _tokenService.ExpediteToken(user, guid);
            var session = new Session
            {
                Expiration = DateTime.UtcNow.AddDays(2),
                Id = guid,
                Uen = user.Uen,
            };
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
            return (token, guid);
        }
    }
}