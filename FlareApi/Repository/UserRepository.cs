using System;
using System.Data.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FlareApi.Api.V1.DataAccess;
using FlareApi.Api.V1.Request;
using FlareApi.Data;
using FlareApi.Entities;

namespace FlareApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FlareContext _context;

        public UserRepository(FlareContext context)
        {
            _context = context;
        }

        public async Task<User?> FindUserAsync(string uen)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Uen == uen);
        }

        public IOrderedQueryable<User> FindUsers(UserPagination pagination)
        {
            IQueryable<User> users = _context.Users
                .Include(u => u.Role)
                .Include(u => u.Department);

            users = pagination.Filter.Active switch
            {
                true => users.Where(u => u.Active),
                false => users.Where(u => !u.Active),
                null => users,
            };

            var depId = pagination.Filter.DepartmentId;
            users = depId switch
            {
                not null => users.Where(u => u.DepartmentId == depId),
                _ => users
            };

            var roleId = pagination.Filter.RoleId;
            users = roleId switch
            {
                not null => users.Where(u => u.RoleId == roleId),
                _ => users,
            };

            var uen = pagination.Filter.UenContains;
            users = uen switch
            {
                not null => users.Where(u => EF.Functions.Like(u.Uen, $"%{uen}%")),
                _ => users
            };

            return pagination.Sort switch
            {
                UserSort.Name => users.OrderBy(u => u.Name),
                UserSort.NameDesc => users.OrderByDescending(u => u.Name),
                UserSort.Uen => users.OrderBy(u => u.Uen),
                UserSort.UenDesc => users.OrderByDescending(u => u.Uen),
                UserSort.Department => users.OrderBy(u => u.DepartmentId),
                UserSort.DepartmentDesc => users.OrderByDescending(u => u.DepartmentId),
                UserSort.Role => users.OrderBy(u => u.RoleId),
                UserSort.RoleDesc => users.OrderByDescending(u => u.RoleId),
                _ => users.OrderBy(u => u.Uen),
            };
        }

        public async Task<User?> SaveUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateException ex)
            {
                return null;
            }
        }

        public async Task<Department?> FindDepartmentAsync(int departmentId)
        {
            return await _context.Departments.FindAsync(departmentId);
        }
    }
}