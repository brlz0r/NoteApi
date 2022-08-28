using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NoteApi.Models.Data;
using NoteApi.Models.Entities;
using NoteApi.Repositories.Interfaces;

namespace NoteApi.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDatabaseContext _ctx;
        private readonly IMapper _mapper;
        public NoteRepository(AppDatabaseContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task AddNoteAsync(NoteData data)
        {
            var note = _mapper.Map<Note>(data);
            await _ctx.AddAsync(note);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(Guid id)
        {
            var note = await GetNoteEntityByIdAsync(id);
            await Task.Run(() => _ctx.Notes.Remove(note));
            await _ctx.SaveChangesAsync();
        }

        public async Task<NoteData> GetNoteByIdAsync(Guid id)
        {
            var note = await _ctx.Notes
              .Include(n => n.PathWrappers)
              .ThenInclude(p => p.Points)
              .FirstOrDefaultAsync(n => n.Id == id);
            return _mapper.Map<NoteData>(note);
        }

        public async Task<ICollection<NoteData>> GetNotesByUserIdAsync(Guid userId)
        {
            var notes = await _ctx.Notes
              .Include(n => n.PathWrappers)
              .ThenInclude(p => p.Points)
              .Where(p => userId == p.UserId)
              .ToListAsync();
            return _mapper.Map<ICollection<NoteData>>(notes);
        }

        public async Task UpdateNoteAsync(Guid id, NoteData data)
        {
            var note = await GetNoteEntityByIdAsync(id);
            var pathWrappers = _mapper.Map<ICollection<PathWrapper>>(data.PathWrappers);

            note.Title = data.Title;
            note.Content = data.Content;
            note.Timestamp = data.Timestamp;
            note.PathWrappers = pathWrappers;

            await _ctx.SaveChangesAsync();
        }

        private async Task<Note> GetNoteEntityByIdAsync(Guid id)
        {
            return await _ctx.Notes
              .Include(n => n.PathWrappers)
              .ThenInclude(p => p.Points)
              .FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}
