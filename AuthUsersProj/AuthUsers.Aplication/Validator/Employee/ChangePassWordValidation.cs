using AuthUsers.Aplication.Commands.Employee;
using FluentValidation;

namespace AuthUsers.Aplication.Validator.Employee;

public class ChangePassWordValidation : AbstractValidator<ChangePasswordCommands>
{
    public ChangePassWordValidation()
    {
        RuleFor(e => e.Password)
            .NotEmpty().WithMessage("É obrigatorio passar uma senha nova para o funcionario")
            .MinimumLength(10).WithMessage("A senha deve ter no mínimo 10 caracteres.")
            .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
            .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
            .Matches(@"\d").WithMessage("A senha deve conter pelo menos um número.")
            .Matches(@"[\!\?\*\.\@\#\$\%\^\&\+\=]").WithMessage("A senha deve conter pelo menos um caractere especial (! ? * . @ # $ % ^ & + =).");
    }
}
