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

        [HttpGet("{id}")]
        public async Task<ActionResult<ChatDto>> GetChatById(Guid id)
        {
            var chat = await _chatService.GetChatByIdAsync(id);
            return Ok(chat);
        }

        [HttpPost]
        public async Task<ActionResult<ChatDto>> CreateChat(ChatDto chatDto)
        {
            var chat = await _chatService.CreateChatAsync(chatDto);
            return Ok(chat);
        }

        [HttpPut]
        public async Task<ActionResult<ChatDto>> UpdateChat(ChatDto chatDto)
        {
            var chat = await _chatService.UpdateChatAsync(chatDto);
            return Ok(chat);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChat(Guid id)
        {
            await _chatService.DeleteChatAsync(id);
            return Ok();
        }

        [HttpGet("{id}/messages")]
        public async Task<ActionResult<List<ChatMessageDto>>> GetMessagesByChatId(Guid id)
        {
            var messages = await _chatService.GetMessagesByChatIdAsync(id);
            return Ok(messages);
        }

        [HttpPost("{id}/messages")]
        public async Task<ActionResult<ChatMessageDto>> CreateMessage(Guid id, ChatMessageDto messageDto)
        {
            messageDto.Chat.Id = id;
            var message = await _chatService.CreateMessageAsync(messageDto);
            return Ok(message);
        }

        [HttpPut("{id}/messages")]
        public async Task<ActionResult<ChatMessageDto>> UpdateMessage(Guid id, ChatMessageDto messageDto)
        {
            messageDto.Chat.Id = id;
            var message = await _chatService.UpdateMessageAsync(messageDto);
            return Ok(message);
        }

        [HttpDelete("{id}/messages")]
        public async Task<ActionResult> DeleteMessage(Guid id)
        {
            await _chatService.DeleteMessageAsync(id);
            return Ok();
        }





    }
}