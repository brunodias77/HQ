using System.Net;

namespace HQ.Application.Exceptions;

public abstract class ExceptionBase : SystemException
{
    public abstract IList<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
    
    protected ExceptionBase(string message) : base(message)
    {
    }
}