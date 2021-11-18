using System.Linq;
using System.Text.RegularExpressions;
using FlareApi.Api.V1.Request;
using FlareApi.Entities;
using FluentValidation;

namespace FlareApi.Api.V1.Validation
{
    public class UpdateUserRequestValidation : AbstractValidator<UpdateUserRequest>
    {
        private readonly Regex _onlyLetters = new("[a-zñ ]", RegexOptions.IgnoreCase);

        public UpdateUserRequestValidation()
        {
            Transform(u => u.Name, s => s.Trim())
                .NotEmpty()
                .MaximumLength(User.NameLenght)
                .MinimumLength(2)
                .Matches(_onlyLetters);

            Transform(u => u.LastName, s => s.Trim())
                .NotEmpty()
                .MaximumLength(User.LastNameLenght)
                .MinimumLength(2)
                .Matches(_onlyLetters);

            RuleFor(u => u.RoleId)
                .Must(x => Role.Roles.Contains(x));

            RuleFor(u => u.DepartmentId)
                .GreaterThanOrEqualTo(1);
        }
    }

    public class CreateUserRequestValidation : AbstractValidator<CreateUserRequest>
    {
        private readonly Regex _uen = new("en[a-z0-9]+", RegexOptions.IgnoreCase);


        public CreateUserRequestValidation()
        {
            Include(new UpdateUserRequestValidation());

            Transform(u => u.Uen, s => s.Trim())
                .NotEmpty()
                .MinimumLength(User.UenMinLenght)
                .MaximumLength(User.UenLenght)
                .Matches(_uen);
        }
    }
}