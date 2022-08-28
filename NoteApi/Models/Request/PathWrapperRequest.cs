namespace NoteApi.Models.Request
{
    public class PathWrapperRequest
    {
        public float StrokeWidth { get; set; }
        public long StrokeColor { get; set; }
        public long Alpha { get; set; }
        public ICollection<PointRequest> Points { get; set; }
    }
}
