using FlareApi.Entities;

namespace FlareApi.Api.V1.Responses
{
    public class UserInfo
    {
        public string Name { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Uen { get; init; } = string.Empty;
        public bool Active { get; init; }
        public Role Role { get; init; } = null!;
        public Department Department { get; init; } = null!;
    }
}