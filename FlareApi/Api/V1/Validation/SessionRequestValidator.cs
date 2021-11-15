using System.Text.RegularExpressions;
using FlareApi.Api.V1.Request;
using FlareApi.Entities;
using FluentValidation;

namespace FlareApi.Api.V1.Validation
{
    public class SessionRequestValidator : AbstractValidator<SessionRequest>
    {
        private readonly Regex _uen = new("en[a-z0-9]+", RegexOptions.IgnoreCase);

        public SessionRequestValidator()
        {
            RuleFor(rq => rq.Uen)
                .NotEmpty()
                .MinimumLength(User.UenMinLenght)
                .MaximumLength(User.UenLenght)
                .Matches(_uen);

            RuleFor(rq => rq.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }


    public class RefreshRequestValidator : AbstractValidator<RefreshRequest>
    {
        public RefreshRequestValidator()
        {
            RuleFor(rq => rq.refreshToken)
                .NotEmpty()
                .Length(Session.GuiLenght);
        }
    }
}