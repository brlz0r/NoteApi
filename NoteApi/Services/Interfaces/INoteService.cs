using NoteApi.Models.Data;

namespace NoteApi.Services.Interfaces
{
    public interface INoteService
    {
        Task AddNoteAsync(Guid userId, NoteData data);
        Task UpdateNoteAsync(Guid userId, Guid id, NoteData data);
        Task DeleteNoteAsync(Guid userId, Guid id);
        Task<NoteData> GetNoteByIdAsync(Guid userId, Guid id);
        Task<ICollection<NoteData>> GetNotesByUserIdAsync(Guid userId);
    }
}
