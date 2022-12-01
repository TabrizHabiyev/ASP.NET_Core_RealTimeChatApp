

namespace RealTimeChatApp.Domain.ExceptionModels.Common;

public class NotFoundException : Exception
{
	public NotFoundException(string message) : base(message)
    {

	}
}
