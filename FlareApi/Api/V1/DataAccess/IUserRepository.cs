using System;
using System.Threading.Tasks;
using FlareApi.Entities;

namespace FlareApi.Api.V1.DataAccess
{
    public interface IUserRepository
    {
        Task<User?> FindUserAsync(string uen);
        Task AddFailedAttemptAsync(User user);
        Task<(string, Guid)> CreateSessionAsync(User user);
    }
}