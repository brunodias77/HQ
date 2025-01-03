using System.Net;

namespace HQ.Application.Exceptions;

public class ValidationErrorException : ExceptionBase
{
    private readonly IList<string> _errorMessages;

    public ValidationErrorException(IList<string> errorMessages) : base(string.Empty)
    {
        _errorMessages = errorMessages;

    }
    
    public override IList<string> GetErrorMessages() => _errorMessages;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}