namespace NoteApi.Models.Entities
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public long Timestamp { get; set; }
        public ICollection<PathWrapper> PathWrappers { get; set; }
        public Guid UserId { get; set; }
    }
}
