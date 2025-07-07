using AuthUsers.Aplication.Commands.Employee;
using FluentValidation;

namespace AuthUsers.Aplication.Validator.Employee;

public class CreateEmployeeValidation : AbstractValidator<CreateEmployeeCommands>
{
    public CreateEmployeeValidation()
    {
        RuleFor(e => e.Nome)
            .NotEmpty().WithMessage("É Obrigatorio passar um Nome para o Funcionário");

        RuleFor(e => e.Surnames)
            .NotEmpty().WithMessage("É Obrigatorio passar o Sobrenome do Funcionário");

        RuleFor(e => e.Position)
            .NotEmpty().WithMessage("É Obrigatorio passar o cargo para o Funcionário");
            

        RuleFor(e => e.HashPassword)
            .NotEmpty().WithMessage("O Funcionario Deve ter uma Senha")
            .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
            .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
            .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
            .Matches(@"\d").WithMessage("A senha deve conter pelo menos um número.")
            .Matches(@"[\!\?\*\.\@\#\$\%\^\&\+\=]").WithMessage("A senha deve conter pelo menos um caractere especial (! ? * . @ # $ % ^ & + =)."); 

        RuleFor(e => e.CreateById)
            .NotEmpty().WithMessage("Deve Passar o Id de quem Criou");
    }
}
