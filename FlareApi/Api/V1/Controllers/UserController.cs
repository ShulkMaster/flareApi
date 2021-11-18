using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using FlareApi.Api.V1.DataAccess;
using FlareApi.Api.V1.Request;
using FlareApi.Api.V1.Responses;
using FlareApi.Config;
using FlareApi.Entities;
using FlareApi.Service.Driver;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlareApi.Api.V1.Controllers
{
    [ApiController]
    [Route(Routes.UserRouteV1)]
    [Authorize(Policy = nameof(FlarePolicy))]
    public class UserController : FlareController
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiMetaResponse<UserInfo>), StatusCodes.Status200OK)]
        public async Task<ApiMetaResponse<UserInfo>> Index([FromQuery] UserPagination pagination)
        {
            var users = _repo.FindUsers(pagination);

            if (_mapper.ProjectTo<UserInfo>(users) is not IOrderedQueryable<UserInfo> project)
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

        [HttpGet("{uen}")]
        public async Task<ActionResult> Find(string uen)
        {
            var user = await _repo.FindUserAsync(uen);
            if (user is null) return NotFound("User not found");
            return Ok(new ApiResponse(user));
        }
        
        [HttpPost]
        [Authorize(Policy = nameof(FlarePolicy), Roles = Role.Admin)]
        public async Task<ActionResult<UserInfo>> Index(
            [FromBody] CreateUserRequest request,
            [FromServices] IPasswordService password)
        {
            var department = await _repo.FindDepartmentAsync(request.DepartmentId);
            if (department is null)
            {
                return NotFound($"El departamento con el id {request.DepartmentId} no se encontro");
            }

            var user = _mapper.Map<User>(request);
            user.Active = true;
            var result = password.GeneratePassword(user);
            user.Password = result.Hashed;
            var savedUser = await _repo.SaveUserAsync(user);
            if (savedUser is null)
            {
                return new StatusCodeResult(500);
            }

            savedUser.Password = result.PlainText;
            var userInfo = _mapper.Map<UserWithPassword>(savedUser);
            return Ok(new ApiResponse(userInfo));
        }
    }
}