using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteApi.Models.Data;
using NoteApi.Models.Request;
using NoteApi.Services.Interfaces;

namespace NoteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public AuthController(IMapper mapper, IAuthService authService)
        {
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthRequest request)
        {
            var data = _mapper.Map<AuthData>(request);
            return Ok(await _authService.AuthenticateAsync(data));
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration(AuthRequest request)
        {
            var data = _mapper.Map<AuthData>(request);
            await _authService.RegistrationAsync(data);

            return Ok(new { message = "Successfully" });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var data = _mapper.Map<RefreshTokenData>(request);
            return Ok(await _authService.RefreshTokenAsync(data));
        }
    }
}
