namespace NoteApi.Models.Data
{
    public class RefreshTokenData
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public bool IsActive => !IsExpired;
    }
}
