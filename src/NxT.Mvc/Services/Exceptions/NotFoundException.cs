namespace NxT.Mvc.Services.Exceptions
{
    public class NotFoundException(string? message) : ApplicationException(message);
}
