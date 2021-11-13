namespace FlareApi.Api.V1.Request
{
    public class SessionRequest
    {
        public string Uen { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}