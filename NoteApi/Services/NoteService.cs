using NoteApi.Models.Data;
using NoteApi.Models.HttpException;
using NoteApi.Repositories.Interfaces;
using NoteApi.Services.Interfaces;

namespace NoteApi.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task AddNoteAsync(Guid userId, NoteData data)
        {
            data.UserId = userId;
            await _noteRepository.AddNoteAsync(data);
        }

        public async Task UpdateNoteAsync(Guid userId, Guid id, NoteData data)
        {
            var note = await _noteRepository.GetNoteByIdAsync(id);

            if (note == null || note.UserId != userId)
            {
                throw new NotFoundException("Note not found");
            }

            data.UserId = userId;
            await _noteRepository.UpdateNoteAsync(note.Id, data);
        }

        public async Task DeleteNoteAsync(Guid userId, Guid id)
        {
            var note = await _noteRepository.GetNoteByIdAsync(id);

            if (note == null || note.UserId != userId)
            {
                throw new NotFoundException("Note not found");
            }

            await _noteRepository.DeleteNoteAsync(note.Id);
        }

        public async Task<NoteData> GetNoteByIdAsync(Guid userId, Guid id)
        {
            var note = await _noteRepository.GetNoteByIdAsync(id);

            if (note == null || note.UserId != userId)
            {
                throw new NotFoundException("Note not found");
            }

            return note;
        }

        public async Task<ICollection<NoteData>> GetNotesByUserIdAsync(Guid userId)
        {
            return await _noteRepository.GetNotesByUserIdAsync(userId);
        }
    }
}
