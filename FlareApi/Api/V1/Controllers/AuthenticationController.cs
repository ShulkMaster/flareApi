using System;
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
        private readonly ISessionRepository _repo;
        private readonly IMapper _mapper;

        public AuthenticationController(ISessionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        [ActionName("")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] SessionRequest request)
        {
            var (user, ex) = await _repo.FindUserAsync(request.Uen, request.Password);

            if (user is null)
            {
                return NotFound($"User with UEN {request.Uen} not found");
            }

            if (ex is not null)
            {
                return Unauthorized(ex.Message);
            }

            var (token, refresh) = await _repo.CreateSessionAsync(user);
            var session = new UserSession(token, refresh, _mapper.Map<UserInfo>(user));
            return Ok(new ApiResponse(session));
        }

        [HttpPatch]
        [ActionName("")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            var session = await _repo.FindSessionAsync(request.refreshToken);
            if (session is null)
            {
                return NotFound("Session not found");
            }

            if (session.Expiration <= DateTime.Now)
            {
                return Unauthorized("The session has already expired");
            }

            var (token, refresh) = await _repo.RefreshAsync(session);
            var newSession = new UserSession(token, refresh, _mapper.Map<UserInfo>(session.User));
            return Ok(new ApiResponse(newSession));
        }

        [HttpDelete]
        [ActionName("")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Revoke([FromBody] RefreshRequest request)
        {
            await _repo.RevokeSessionAsync(request.refreshToken);
            return Ok(new ApiResponse("Session terminated"));
        }
    }
}