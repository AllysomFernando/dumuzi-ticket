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
            throw new AssertException("CPF inválido, tamanho menor que onze.");

    }

}
