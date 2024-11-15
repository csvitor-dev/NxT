namespace SalesApp.Services.Exceptions
{
    public class NotFoundException(string? message) : ApplicationException(message);
}
