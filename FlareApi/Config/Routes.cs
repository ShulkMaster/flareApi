using System.Collections.Generic;

namespace FlareApi.Config
{
    public static class Routes
    {
        private const string AuthRoute = "/auth/[action]";
        private const string UserRoute = "/user";
        private const string DepartmentRoute = "/department";

    #region V1

        private const string V1 = "/api/v1";
        public const string AuthRouteV1 = V1 + AuthRoute;
        public const string UserRouteV1 = V1 + UserRoute;
        public const string DepartmentRouteV1 = V1 + DepartmentRoute;

    #endregion

        public static readonly string[] PermittedExtensions = { ".png", ".jpeg", ".jpg" };

        public static readonly Dictionary<string, byte[][]> Signatures = new()
        {
            {
                ".jpeg", new[]
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 }, //JPEG IMAGE
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 }, //CANNON EOS JPEG FILE
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 }, //SAMSUNG D500 JPEG FILE
                }
            },
            {
                ".jpg", new[]
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 }, //JPEG IMAGE
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 }, //CANNON EOS JPEG FILE
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 }, //SAMSUNG D500 JPEG FILE
                }
            },
            {
                ".png", new[]
                {
                    new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }, //PNG image
                }
            }
        };
    }
}