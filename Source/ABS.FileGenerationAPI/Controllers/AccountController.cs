using ABS.FileGenerationAPI.Data;
using ABS.FileGenerationAPI.Models;
using ABS.FileGenerationAPI.Models.Requests;
using ABS.FileGenerationAPI.Models.Responses;
using ABS.FileGenerationAPI.Services.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace ABS.FileGenerationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(ITokenService tokenService, DataContext dataContext) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationRequest loginDto)
        {            
            var user = dataContext.Set<AppUser>().FirstOrDefault(item => item.UserName == loginDto.UserName);
            if (user == null) return Unauthorized("Invalid username");

            return new AuthenticationResponse
            {
                UserName = user.UserName,
                Token = tokenService.GenerateToken(user)
            };
        }
    }
}
