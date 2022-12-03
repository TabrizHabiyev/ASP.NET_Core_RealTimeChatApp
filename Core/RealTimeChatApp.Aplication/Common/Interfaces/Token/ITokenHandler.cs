

namespace RealTimeChatApp.Application.Common.Interfaces.Token;

public interface ITokenHandler
{
    DTOs.Token CreateAccessToken(int expiration, User user, IList<string> role);
}
