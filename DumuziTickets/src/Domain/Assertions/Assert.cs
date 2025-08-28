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

}