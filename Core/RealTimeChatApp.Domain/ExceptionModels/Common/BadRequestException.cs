

namespace RealTimeChatApp.Domain.ExceptionModels.Common;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {

    }
}
