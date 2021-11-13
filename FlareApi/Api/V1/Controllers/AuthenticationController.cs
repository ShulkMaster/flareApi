using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using FlareApi.Api.V1.DataAccess;
using FlareApi.Api.V1.Request;
using FlareApi.Api.V1.Responses;
using FlareApi.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlareApi.Api.V1.Controllers
{
    [ApiController]
    [Route(Routes.AuthRouteV1)]
    public class AuthenticationController : FlareController
    {
        private readonly IUserRepository _repo;

        public AuthenticationController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] SessionRequest request, [FromServices] IMapper mapper)
        {
            var user = await _repo.FindUserAsync(request.Uen);
            if (user is null)
            {
                return NotFound($"User with UEN {request.Uen} not found");
            }

            if (user.Password != request.Password)
            {
                return Unauthorized($"User name/password combination is not correct");
            }

            var (token, refresh) = await _repo.CreateSessionAsync(user);
            var session = new UserSession(token, refresh.ToString(), mapper.Map<UserInfo>(user));
            return Ok(new ApiResponse(session));
        }
    }
}