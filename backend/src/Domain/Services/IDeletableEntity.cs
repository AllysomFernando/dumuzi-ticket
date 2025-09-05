using DumuziTickets.Domain.Exceptions;

namespace DumuziTickets.Domain.Services;

public interface IDeletableEntity
{
    public void validateDeletion(string message)
    {
        throw new BusinessExecption(message);
    } 
}