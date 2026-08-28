using FluentValidation;

namespace DiaryPortfolio.Application.Features.User.Authentication.ResetPassword
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Token)
                .NotEmpty();

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit");

            RuleFor(x => x.NewPasswordConfirmation)
                .Equal(x => x.NewPassword).WithMessage("Password and confirmation do not match");
        }
    }
}
