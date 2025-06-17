using AuthUsers.Aplication.Interfaces;

namespace AuthUsers.Aplication.Services;

public class ValidateCPF : IValidateCPF
{
    public bool CPFIsValid(string cpf)
    {
        //retorna nulo se o cpf for vazio
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        // Remove pontos e traços
        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
            return false;

        var cpfArray = cpf.Select(c => int.Parse(c.ToString())).ToArray();

        // Valida o primeiro dígito
        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += cpfArray[i] * (10 - i);

        int remainder = sum % 11;
        int firstVerifier = remainder < 2 ? 0 : 11 - remainder;

        if (cpfArray[9] != firstVerifier)
            return false;

        // Valida o segundo dígito
        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += cpfArray[i] * (11 - i);

        remainder = sum % 11;
        int secondVerifier = remainder < 2 ? 0 : 11 - remainder;

        return cpfArray[10] == secondVerifier;
    }
}
