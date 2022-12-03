using Microsoft.AspNetCore.Mvc;
using RealTimeChatApp.Application.Common.Interfaces.Services;
using RealTimeChatApp.Application.DTOs.Chat;
using RealTimeChatApp.Presentation.Helpers;

namespace Alfaex.az_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ChatController : BaseControllerHelper
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatDto>>> GetAllChats()
        {
            var chats = await _chatService.GetAllChatsAsync();
            return Ok(chats);
        }

    }
}