namespace FlareApi.Config
{
    public static class FlarePolicy
    {
        public const string Regular = "Regular";
        public const string Admin = "Admin";
        public const string FlareIdentity = nameof(FlareIdentity);

        public const string AllRoles = Regular + "," + Admin;

        public static readonly string[] Roles = { Regular, Admin };
    }
}