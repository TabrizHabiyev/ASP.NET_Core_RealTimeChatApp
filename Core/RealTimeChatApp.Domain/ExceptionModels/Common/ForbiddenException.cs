﻿

namespace RealTimeChatApp.Domain.ExceptionModels.Common;

public class ForbiddenException : Exception
{
	public ForbiddenException(string message) : base(message)
    {

	}
}
