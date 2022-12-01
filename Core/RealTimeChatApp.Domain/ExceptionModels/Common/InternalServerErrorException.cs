
namespace RealTimeChatApp.Domain.ExceptionModels.Common;

public class InternalServerErrorException : Exception
{
	public InternalServerErrorException(string message) : base(message)
    {

	}
}
