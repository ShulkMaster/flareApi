namespace FlareApi.Api.V1.Request
{
    public class UserPagination : PaginationRequest
    {
        public UserSort? Sort { get; set; } = UserSort.Name;

        public UserFilter Filter { get; set; } = new();

        public class UserFilter
        {
            public bool? Active { get; set; }

            public string? NameContains { get; set; }

            public string? UenContains { get; set; }

            public int? DepartmentId { get; set; }

            public string? RoleId { get; set; }
        }
    }

    public class DepartmentPagination : ZeroPageRequest
    {
        public DepartmentFilter Filter { get; set; } = new();

        public DepartmentSort? Sort = DepartmentSort.Name;
        
        public class DepartmentFilter
        {
            public string? NameContains { get; set; }
            
        }
    }
}