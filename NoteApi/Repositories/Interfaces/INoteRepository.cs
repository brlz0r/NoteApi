using NoteApi.Models.Data;

namespace NoteApi.Repositories.Interfaces
{
    public interface INoteRepository
    {
        Task AddNoteAsync(NoteData data);
        Task UpdateNoteAsync(Guid id, NoteData data);
        Task DeleteNoteAsync(Guid id);
        Task<NoteData> GetNoteByIdAsync(Guid id);
        Task<ICollection<NoteData>> GetNotesByUserIdAsync(Guid userId);
    }
}
