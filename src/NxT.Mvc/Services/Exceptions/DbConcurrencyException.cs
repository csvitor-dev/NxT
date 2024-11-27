namespace NxT.Mvc.Services.Exceptions
{
    public class DbConcurrencyException(string? message) : ApplicationException(message);
}
