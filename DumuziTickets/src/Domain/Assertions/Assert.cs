using DumuziTickets.Domain.Exceptions;

namespace DumuziTickets.Domain.Assertions;

public class Assert
{   
    public static void IsNull(object? value, string message)
    {
        if (value is null)
        {
            return;
        }

        throw new AssertException(message);
    }

    public static void IsNotNull(object? value, string message)
    {
        if (value is not null)
        {
            return;
        }

        throw new AssertException(message);
    }

    public static void CpfIsValid(string cpf)
    {
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        cpf = cpf.Trim().Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            throw new AssertException("CPF inválido.");

        for (int j = 0; j < 10; j++)
            if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                return;

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        string digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        if (!cpf.EndsWith(digito))
            throw new AssertException("CPF inválido.");
    }
    
}