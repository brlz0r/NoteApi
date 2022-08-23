namespace NoteApi.Models.Entities
{
    public class PathWrapper
    {
        public Guid Id { get; set; }
        public float StrokeWidth { get; set; }
        public long StrokeColor { get; set; }
        public long Alpha { get; set; }
        public ICollection<Point> Points {get; set;}
        public User User { get; set; }
    }
}
