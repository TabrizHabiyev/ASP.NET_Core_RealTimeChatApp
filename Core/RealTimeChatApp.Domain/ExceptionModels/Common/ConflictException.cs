
namespace RealTimeChatApp.Domain.ExceptionModels.Common;

public class ConflictException : Exception
{
	public ConflictException(string message) : base(message)
    {

	}
}
