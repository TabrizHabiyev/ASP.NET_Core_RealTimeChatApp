


namespace Alfaex.az_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChatController : BaseControllerHelper
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }


        [HttpGet]
         public async Task<IActionResult> GetPrivateChats()
        {
            var chats = await _chatService.GetPrivateChats(Guid.Parse(GetUserId()));
            return Ok(chats);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserJoinededRoom()
        {
            var rooms = await _chatService.GetUserJoinededRoom(Guid.Parse(GetUserId()));
            return Ok(rooms);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetRoomsAsync() 
        {
            var rooms = await _chatService.GetRoomsAsync(Guid.Parse(GetUserId()));
            return Ok(rooms);
        }

        [HttpGet]
        public async Task<IActionResult> GetChatAsync(Guid id)
        {
            var chat = await _chatService.GetChatAsync(id, Guid.Parse(GetUserId()));
            return Ok(chat);
        }

        [HttpPost]
         public async Task<IActionResult> CreatePrivateChat(Guid targetId)
        {
            var chat = await _chatService.CreatePrivateChat(Guid.Parse(GetUserId()), targetId);
            return Ok(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            await _chatService.CreateRoom(name, Guid.Parse(GetUserId()));
            return Ok();
        }


        [HttpDelete]
        public async Task<bool> DeleteChatAsync(Guid chatId)
        {
            var chat = await  _chatService.DeleteChatAsync(chatId, Guid.Parse(GetUserId()));
            return chat;
        }

       [HttpGet]
        public async Task<IActionResult> GetMessagesIdAsync(Guid id)
        {
           var message = await _chatService.GetMessagesIdAsync(id);
            return Ok(message);
        }

        [HttpPost]
         public async Task<IActionResult> CreateMessageAsync([FromForm] CreateMessageDto createMessage)
         {
            var message = await _chatService.CreateMessageAsync(createMessage, Guid.Parse(GetUserId()));
            return Ok();
         }

        [HttpPut]
         public async Task<IActionResult> UpdateMessageAsync([FromForm] CreateMessageDto createMessage,Guid messageId)
         {
            var message = await _chatService.UpdateMessageAsync(createMessage, Guid.Parse(GetUserId()), messageId);
            return Ok();
         }

         [HttpPost]
         public async Task JoinRoom(ChatJoinRoomDto chatJoinRoomDto)
         {
            await _chatService.JoinRoom(chatJoinRoomDto);
         }

         [HttpDelete]
          public async Task<bool> DeleteMessageAsync(Guid id)
          {
            bool result = await _chatService.DeleteMessageAsync(id, Guid.Parse(GetUserId()));
            return result;
          }

    }
}