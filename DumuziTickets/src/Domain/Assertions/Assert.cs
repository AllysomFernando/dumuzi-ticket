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

        throw new BusinessExecption(message);
    }

    public static void IsNotNull(object? value, string message)
    {
        if (value is not null)
        {
            return;
        }

        throw new BusinessExecption(message);
    }

    public static void CpfIsValid(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            throw new AssertException("CPF não pode ser vazio.");

        cpf = cpf.Trim().Replace(".", "").Replace("-", "");

        if (!cpf.All(char.IsDigit))
            throw new AssertException("CPF deve conter apenas números.");

        if (cpf.Length != 11)
            throw new AssertException("CPF deve ter 11 dígitos.");

        if (cpf.All(c => c == cpf[0]))
            throw new AssertException("CPF inválido.");

        string[] cpfsInvalidos = {
            "00000000000", "11111111111", "22222222222", "33333333333",
            "44444444444", "55555555555", "66666666666", "77777777777",
            "88888888888", "99999999999"
        };

        if (cpfsInvalidos.Contains(cpf))
            throw new AssertException("CPF inválido.");

        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += (tempCpf[i] - '0') * multiplicador1[i];

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        tempCpf += digito1;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += (tempCpf[i] - '0') * multiplicador2[i];

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        if (digito1 != (cpf[9] - '0') || digito2 != (cpf[10] - '0'))
            throw new AssertException("CPF inválido.");
    }


    public static void IsFalse(bool condition, string message)
    {
        if (!condition)
        {
            return;
        }

        throw new AssertException(message);
    }

    public static void IsGreaterThan<T>(T value, T compareTo, string message) where T : IComparable<T>
    {
        if (value.CompareTo(compareTo) >= 0)
        {
            return;
        }

        throw new AssertException(message);
    }

}
