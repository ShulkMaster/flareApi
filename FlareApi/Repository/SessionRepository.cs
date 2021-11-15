﻿using System;
using System.Threading.Tasks;
using FlareApi.Api.V1.DataAccess;
using FlareApi.Data;
using FlareApi.Entities;
using FlareApi.Service.Driver;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FlareApi.Repository
{
    public class SessionRepository : ISessionRepository
    {
        private readonly FlareContext _context;
        private readonly ITokenService _tokenService;
        private readonly ILogger<SessionRepository> _logger;

        public SessionRepository(FlareContext context, ITokenService tokenService, ILogger<SessionRepository> logger)
        {
            _context = context;
            _tokenService = tokenService;
            _logger = logger;
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

        public async Task<(User?, Exception?)> FindUserAsync(string uen, string password)
        {
            var defaultError = new Exception("User or password combination incorrect");
            var user = await _context
                .Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Uen == uen);

            if (user is null)
            {
                return (null, defaultError);
            }

            if (user.FailedAttempts > 5)
            {
                _logger.LogInformation(
                    "Attempt to login to lock user account {uen} with {FailedAttempts} failed attempts",
                    uen,
                    user.FailedAttempts
                );
                user.FailedAttempts++;
                await _context.SaveChangesAsync();
                return (
                    user,
                    new Exception($"User is currently lock with {user.FailedAttempts++} failed login attempts")
                );
            }

            if (user.Password == password)
            {
                user.FailedAttempts = 0;
                await _context.SaveChangesAsync();
                return (user, null);
            }

            user.FailedAttempts++;
            _logger.LogInformation("Attempt to login to user account {uen} with wrong credential", uen);
            await _context.SaveChangesAsync();
            return (user, defaultError);
        }
    }
}