using AuthUsers.Aplication.Commands.Users;
using FluentValidation;

namespace AuthUsers.Aplication.Validator.Users;

public class LoginUserValidator : AbstractValidator<LoginUserCommands>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login não pode ser nulo")
            .EmailAddress().WithMessage("Formato de e-mail inválido");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.");
    }
}
