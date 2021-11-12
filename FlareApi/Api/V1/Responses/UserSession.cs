namespace FlareApi.Api.V1.Responses
{
    public record UserSession
    (
        string AccessToken,
        string RefreshToken,
        UserInfo User
    );
}