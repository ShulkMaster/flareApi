using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FlareApi.Config;
using FlareApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlareApi.Api
{
    public class FlareController : ControllerBase
    {
        [NonAction]
        public int GetAccessLevel()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            return claim switch
            {
                FlarePolicy.Regular => Role.RegularLevel,
                FlarePolicy.Admin => Role.AdminLevel,
                _ => Role.RegularLevel
            };
        }
        
        public static async Task<(IEnumerable<T>, Metadata)> Paginate<T>(
            IOrderedQueryable<T> queryable,
            PaginationRequest pagination)
        {
            if (pagination.Size == 0)
            {
                var all = await queryable.ToListAsync();
                return (all, new Metadata(pagination, all.Count));
            }

            var total = queryable.Count();
            var enumerable = queryable.Skip(pagination.Size * (pagination.Page - 1)).Take(pagination.Size);
            var list = await enumerable.ToArrayAsync();
            return (list, new Metadata(pagination, total));
        }
    }
}