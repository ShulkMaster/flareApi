using System;

namespace FlareApi.Api.V1.Responses
{
    public record UserSession
    (
        string AccessToken,
        Guid RefreshToken,
        UserInfo User
    );
}