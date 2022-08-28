namespace NoteApi.Models.Data
{
    public class PathWrapperData
    {
        public Guid Id { get; set; }
        public float StrokeWidth { get; set; }
        public long StrokeColor { get; set; }
        public long Alpha { get; set; }
        public ICollection<PointData> Points { get; set; }
    }
}
