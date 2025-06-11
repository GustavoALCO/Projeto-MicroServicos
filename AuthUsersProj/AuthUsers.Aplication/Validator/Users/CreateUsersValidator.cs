using AuthUsers.Aplication.Commands.Users;
using FluentValidation;
using System.Security.Cryptography.X509Certificates;

namespace AuthUsers.Aplication.Validator.Users;

public class CreateUsersValidator : AbstractValidator<CreateUsersCommands>
{
    public CreateUsersValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("É obrigatorio colocar o seu nome para criar uma conta")
            .MaximumLength(50).WithMessage("O seu nome nao pode ser maior que 50 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("É obrigatorio colocar o seu email para criar uma conta")
            .EmailAddress().WithMessage("Não foi possivel criar uma conta.\n Email invalido");
        
        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("É obrigatorio colocar um cpf para criar uma conta")
            .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$").WithMessage("O CPF deve estar no formato 000.000.000-00");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("É necessario passar um numero de contato")
            .Matches(@"^(\d{2}) \d{5}-\d{4}$").WithMessage("O formato para envidar o número deve ser \n (00) 00000-0000");

        RuleFor(x => x.HashPassword)
            .NotEmpty().WithMessage("É obrigatorio passar uma senha")
            .MinimumLength(12).WithMessage("A senha deve conter no minimo 12 caracteres")
            .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
            .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
            .Matches(@"\d").WithMessage("A senha deve conter pelo menos um número.")
            .Matches(@"[\!\?\*\.\@\#\$\%\^\&\+\=]").WithMessage("A senha deve conter pelo menos um caractere especial");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("É obrigatorio passar o sobrenome para criar uma conta")
            .MaximumLength(150).WithMessage("O numereo maximo de caracteres é de 150");

    }
}
