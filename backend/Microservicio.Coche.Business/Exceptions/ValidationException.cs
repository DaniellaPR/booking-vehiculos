namespace Microservicios.Coche.Business.Exceptions;

public class ValidationException : Exception
{
    public IReadOnlyCollection<string> Errors { get; }

    public ValidationException(string message) : base(message)
    {
        Errors = new List<string>();
    }

    public ValidationException(string message, IReadOnlyCollection<string> errors) : base(message)
    {
        Errors = errors;
    }
}