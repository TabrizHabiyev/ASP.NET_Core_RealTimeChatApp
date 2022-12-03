

namespace RealTimeChatApp.Presentation.Helpers
{
    public class BaseControllerHelper : ControllerBase
    {
        protected string GetUserId()
        {
            return User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
        }
    }
}
