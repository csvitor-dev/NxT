namespace SalesApp.Services.Exceptions
{
    public class DbConcurrencyException(string? message) : ApplicationException(message);
}
