namespace Topic.Application.Exceptions;

public sealed class AlreadyExistsException(string message) : ApplicationException(message)
{
}