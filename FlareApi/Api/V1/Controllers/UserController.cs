using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlareApi.Api.V1.DataAccess;
using FlareApi.Api.V1.Request;
using FlareApi.Api.V1.Responses;
using FlareApi.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlareApi.Api.V1.Controllers
{
    [ApiController]
    [Route(Routes.UserRouteV1)]
    public class UserController : FlareController
    {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiMetaResponse<UserInfo>), StatusCodes.Status200OK)]
        public async Task<ApiMetaResponse<UserInfo>> Index(
            [FromQuery] UserPagination pagination,
            [FromServices] IMapper mapper)
        {
            var users = _repo.FindUsers(pagination);

            if (mapper.ProjectTo<UserInfo>(users) is not IOrderedQueryable<UserInfo> project)
            {
                return new ApiMetaResponse<UserInfo>(
                    StatusCodes.Status500InternalServerError, 
                    "Can't project Users",
                    pagination.Filter
                    );
            }

            var (list, meta) = await Paginate(project, pagination);
            return new ApiMetaResponse<UserInfo>(list, meta, pagination.Filter);
        }
    }
}