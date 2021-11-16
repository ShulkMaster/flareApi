using System.Linq;
using System.Text.RegularExpressions;
using FlareApi.Api.V1.Request;
using FlareApi.Entities;
using FluentValidation;

namespace FlareApi.Api.V1.Validation
{
    public class CreateUserRequestValidation : AbstractValidator<CreateUserRequest>
    {
        private readonly Regex _uen = new("en[a-z0-9]+", RegexOptions.IgnoreCase);
        private readonly Regex _onlyLetters = new("[a-zñ]", RegexOptions.IgnoreCase);

        public CreateUserRequestValidation()
        {
            RuleFor(u => u.Uen)
                .NotEmpty()
                .MinimumLength(User.UenMinLenght)
                .MaximumLength(User.UenLenght)
                .Matches(_uen);

            RuleFor(u => u.Name)
                .NotEmpty()
                .MaximumLength(User.NameLenght)
                .MaximumLength(2)
                .Matches(_onlyLetters);

            RuleFor(u => u.LastName)
                .NotEmpty()
                .MaximumLength(User.LastNameLenght)
                .MaximumLength(2)
                .Matches(_onlyLetters);

            RuleFor(u => u.RoleId)
                .Must(x => Role.Roles.Contains(x));

            RuleFor(u => u.DepartmentId)
                .GreaterThanOrEqualTo(1);
        }
    }
}