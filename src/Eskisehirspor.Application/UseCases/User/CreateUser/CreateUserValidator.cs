using FluentValidation;

namespace Eskisehirspor.Application.UseCases.User.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(m => m.Email)
                .NotNull().WithMessage("E-posta adresi boş geçilemez")
                .Must(m => Domain.Entities.User.IsValidEmail(m)).WithMessage("Geçerli bir e-posta adresi girin.")
                .MaximumLength(Domain.Entities.User.EMAIL_MAX_LENGTH);

            RuleFor(m => m.Username)
                .NotNull().WithMessage("Kullanıcı adı boş geçilemez.")
                .Must(m => Domain.Entities.User.IsValidUsername(m)).WithMessage("Geçersiz kullanıcı adı.");

            RuleFor(m => m.DisplayName)
                .NotNull().WithMessage("Görünen ad boş geçilemez.")
                .Must(m=> Domain.Entities.User.IsValidDisplayName(m)).WithMessage($"Görünen ad {Domain.Entities.User.DISPLAYNAME_MIN_LENGTH} ile {Domain.Entities.User.DISPLAYNAME_MAX_LENGTH} karakter aralığında olmalıdır.");
            
            RuleFor(m => m)
                .Must(m => Domain.Entities.User.IsValidPassword(m.Password, m.PasswordConfirm)).WithMessage( $"Geçersiz parola. {string.Format(Domain.Entities.User.PASSWORD_RULE_MESSAGE, Domain.Entities.User.PASSWORD_MIN_LENGTH)}");
        }
    }
}
