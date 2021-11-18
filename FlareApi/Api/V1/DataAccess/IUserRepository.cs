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
        Task<Department?> FindDepartmentAsync(int departmentId);
        public Task<User?> SaveUserAsync(User user);
        public Task<User?> UpdateUserAsync(User user);
    }
}