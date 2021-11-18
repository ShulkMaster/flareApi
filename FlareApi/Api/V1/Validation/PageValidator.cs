using FlareApi.Api.V1.Request;
using FlareApi.Entities;
using FluentValidation;

namespace FlareApi.Api.V1.Validation
{
    public class PageValidator : AbstractValidator<PaginationRequest>
    {
        public PageValidator()
        {
            RuleFor(p => p.Page)
                .GreaterThanOrEqualTo(1);

            RuleFor(p => p.Size)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(100);
        }
    }

    public class ZeroPageValidator : AbstractValidator<ZeroPageRequest>
    {
        public ZeroPageValidator()
        {
            RuleFor(p => p.Page)
                .GreaterThanOrEqualTo(1);

            RuleFor(p => p.Size)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);
        }
    }

    public class UserPageValidator : AbstractValidator<UserPagination>
    {
        public UserPageValidator()
        {
            Include(new PageValidator());

            RuleFor(p => p.Filter.NameContains)
                .MaximumLength(User.NameLenght);

            RuleFor(u => u.Filter.DepartmentId)
                .GreaterThan(0);

            RuleFor(u => u.Filter.RoleId)
                .MaximumLength(Role.NameLenght);
        }
    }

    public class DepartmentPageValidator : AbstractValidator<DepartmentPagination>
    {
        public DepartmentPageValidator()
        {
            Include(new ZeroPageValidator());

            RuleFor(p => p.Filter.NameContains)
                .MaximumLength(Department.NameLenght);
        }
    }
}