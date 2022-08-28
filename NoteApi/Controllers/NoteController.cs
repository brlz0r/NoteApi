using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NoteApi.Models.Data;
using NoteApi.Models.Request;
using NoteApi.Services.Interfaces;

namespace NoteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly INoteService _noteService;

        public NoteController(IMapper mapper, IAuthService authService, INoteService noteService)
        {
            _mapper = mapper;
            _authService = authService;
            _noteService = noteService;
        }

        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> AddNote(NoteRequest request)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].FirstOrDefault()?.Split(" ").Last();
            var userId = await _authService.GetUserIdByTokenAsync(accessToken);
            
            var data = _mapper.Map<NoteData>(request);

            await _noteService.AddNoteAsync(userId, data);
            return Ok(new { message = "Successfully" });
        }

        [Authorize]
        [HttpPut()]
        public async Task<IActionResult> UpdateNote([FromQuery(Name = "id")] Guid id, NoteRequest request)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].FirstOrDefault()?.Split(" ").Last();
            var userId = await _authService.GetUserIdByTokenAsync(accessToken);
            
            var data = _mapper.Map<NoteData>(request);

            await _noteService.UpdateNoteAsync(userId, id, data);
            return Ok(new { message = "Successfully" });
        }


        [Authorize]
        [HttpDelete()]
        public async Task<IActionResult> DeleteNote([FromQuery(Name = "id")] Guid id)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].FirstOrDefault()?.Split(" ").Last();
            var userId = await _authService.GetUserIdByTokenAsync(accessToken);

            await _noteService.DeleteNoteAsync(userId, id);
            return Ok(new { message = "Successfully" });
        }

        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetNote([FromQuery(Name = "id")] Guid id)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].FirstOrDefault()?.Split(" ").Last();
            var userId = await _authService.GetUserIdByTokenAsync(accessToken);

            return Ok(await _noteService.GetNoteByIdAsync(userId, id));
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserNotes()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].FirstOrDefault()?.Split(" ").Last();
            var userId = await _authService.GetUserIdByTokenAsync(accessToken);

            return Ok(await _noteService.GetNotesByUserIdAsync(userId));
        }
    }
}
