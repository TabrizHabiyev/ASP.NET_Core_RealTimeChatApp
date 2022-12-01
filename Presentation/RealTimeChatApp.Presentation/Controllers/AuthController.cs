using Microsoft.AspNetCore.Mvc;
using RealTimeChatApp.Application.DTOs.User;
using RealTimeChatApp.Application.UnitOfWork;
using RealTimeChatApp.Presentation.Helpers;

namespace Alfaex.az_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : BaseControllerHelper
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserRequestDto loginUserRequestDto)
        {
            if (ModelState.IsValid) { }
            var result = await _unitOfWork.AuthService.LoginAsync(loginUserRequestDto);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromForm] CreateUserDto createUserDto)
        {
            if (ModelState.IsValid) { }
            var result = await _unitOfWork.UserService.CreateAsync(createUserDto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PasswordReset([FromForm] ResetUserPasswordRequestDto resetUserPasswordRequestDto)
        {
            if (ModelState.IsValid) { }
            var result = await _unitOfWork.AuthService.PasswordResetdByEmailAsync(resetUserPasswordRequestDto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword([FromForm] UpdateUserPasswordRequestDto updateUserPasswordRequestDto)
        {
            if (ModelState.IsValid) { }
            var result = await _unitOfWork.AuthService.UpdateUserPassword(updateUserPasswordRequestDto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirimEmail([FromForm] ConfirmUserEmailRequestDto confirmUserEmailRequestDto)
        {
            if (ModelState.IsValid) { }
            var result = await _unitOfWork.AuthService.ConfirmEmail(confirmUserEmailRequestDto);
            return Ok(result);
        }
    }
}