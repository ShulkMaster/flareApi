using System;
using System.Linq;
using System.Threading.Tasks;
using FlareApi.Api.V1.Request;
using FlareApi.Entities;

namespace FlareApi.Api.V1.DataAccess
{
    public interface IUserRepository
    {
        Task<User?> FindUserAsync(string uen);
        IOrderedQueryable<User> FindUsers(UserPagination pagination);
        Task AddFailedAttemptAsync(User user);
        Task<(string, Guid)> CreateSessionAsync(User user);
    }
}