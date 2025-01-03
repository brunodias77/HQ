using System.Net;

namespace HQ.Application.Exceptions;

public class InvalidLoginException : ExceptionBase
{
    public InvalidLoginException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages()
    {
        return [Message];
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.Unauthorized;
    }
}