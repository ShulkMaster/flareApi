using FlareApi.Entities;

namespace FlareApi.Api.V1.Responses
{
    public record UserInfo(
        string Name,
        string LastName,
        string UserName,
        bool Active,
        Role Role,
        Department Department
    );
}