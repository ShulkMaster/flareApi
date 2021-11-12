using System.Collections.Generic;
using System.Threading.Tasks;
using FlareApi.Api.V1.Request;
using FlareApi.Api.V1.Responses;
using FlareApi.Config;
using FlareApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlareApi.Api.V1.Controllers
{
    [ApiController]
    [Route(Routes.AuthRouteV1)]
    public class AuthenticationController : FlareController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<UserSession>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<ApiError>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserSession>> Login([FromBody] SessionRequest request)
        {
            var user = new UserInfo(
                Nue: request.Nue,
                Name: "Jhon",
                LastName: "Doe",
                Active: true,
                Department: new Department
                {
                    Id = 1,
                    Name = "Sales"
                },
                Role: new Role
                {
                    Name = FlarePolicy.Regular,
                    AccessLevel = Role.Regular
                }
            );

            return Ok(new UserSession("myApiToken", "myRefreshToken", user));
        }
    }
}