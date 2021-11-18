using System.Linq;
using System.Threading.Tasks;
using FlareApi.Api.V1.Request;
using FlareApi.Config;
using FlareApi.Data;
using FlareApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlareApi.Api.V1.Controllers
{
    [ApiController]
    [Route(Routes.DepartmentRouteV1)]
    [Authorize(Policy = nameof(FlarePolicy))]
    public class DepartmentController : FlareController
    {

        private readonly FlareContext _context;

        public DepartmentController(FlareContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ApiMetaResponse<Department>> Index([FromQuery] DepartmentPagination pagination)
        {
            IQueryable<Department> queryable = _context.Departments;
            var name = pagination.Filter.NameContains;
            if (name is not null)
            {
                queryable = queryable.Where(d => EF.Functions.Like(d.Name, $"%{name}%"));
            }

            var departments = pagination.Sort switch
            {
                DepartmentSort.Id => queryable.OrderBy(d => d.Id),
                DepartmentSort.IdDesc => queryable.OrderByDescending(d => d.Id),
                DepartmentSort.Name => queryable.OrderBy(d => d.Name),
                DepartmentSort.NameDesc => queryable.OrderByDescending(d => d.Name),
                _ => queryable.OrderBy(d => d.Name)
            };
            var (list, meta) = await Paginate(departments, pagination);
            return new ApiMetaResponse<Department>(list, meta, pagination.Filter);
        }
    }
}