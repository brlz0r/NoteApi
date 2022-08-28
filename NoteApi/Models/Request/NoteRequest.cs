namespace NoteApi.Models.Request
{
    public class NoteRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public long Timestamp { get; set; }
        public ICollection<PathWrapperRequest> PathWrappers { get; set; }
    }
}
