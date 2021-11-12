namespace FlareApi.Api
{
    public record ApiError(string Name, string Message, ApiError? Cause);
}