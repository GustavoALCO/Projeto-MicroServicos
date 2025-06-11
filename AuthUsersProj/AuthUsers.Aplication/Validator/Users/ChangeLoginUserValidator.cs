using AuthUsers.Aplication.Commands.Users;
using FluentValidation;

namespace AuthUsers.Aplication.Validator.Users;

public class ChangeLoginUserValidator : AbstractValidator<ChangeLoginUserCommands>
{
    public ChangeLoginUserValidator()
    {
        RuleFor(x => x.ChangeEmail)
            .EmailAddress().WithErrorCode("email em formato invalido");

        RuleFor(x => x.Password)
            .MinimumLength(10).WithMessage("A senha deve ter no mínimo 10 caracteres.")
            .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
            .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
            .Matches(@"\d").WithMessage("A senha deve conter pelo menos um número.")
            .Matches(@"[\!\?\*\.\@\#\$\%\^\&\+\=]").WithMessage("A senha deve conter pelo menos um caractere especial (! ? * . @ # $ % ^ & + =).");
    }
}
