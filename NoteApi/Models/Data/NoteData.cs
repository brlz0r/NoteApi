namespace NoteApi.Models.Data
{
    public class NoteData
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public long Timestamp { get; set; }
        public ICollection<PathWrapperData> PathWrappers { get; set; }
        public Guid UserId { get; set; }
    }
}
