using AuthUsers.Aplication.Commands.Users;
using FluentValidation;

namespace AuthUsers.Aplication.Validator.Users;

public class ChangeNumberPhone : AbstractValidator<ChangePhoneNumberCommands>
{
    public ChangeNumberPhone()
    {
        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("É necessario passar um numero de contato")
            .Matches(@"^(\d{2}) \d{5}-\d{4}$").WithMessage("O formato para envidar o número deve ser \n (00) 00000-0000");
    }
}
