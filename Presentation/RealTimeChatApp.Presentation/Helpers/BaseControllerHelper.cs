using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RealTimeChatApp.Presentation.Helpers
{
    public class BaseControllerHelper : ControllerBase
    {
        protected string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
