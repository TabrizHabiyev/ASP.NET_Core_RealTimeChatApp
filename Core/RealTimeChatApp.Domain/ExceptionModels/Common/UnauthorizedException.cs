﻿
namespace RealTimeChatApp.Domain.ExceptionModels.Common;

public class UnauthorizedException : Exception
{
	public UnauthorizedException(string message) : base(message)
    {

	}
}