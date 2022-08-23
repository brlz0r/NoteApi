namespace NoteApi.Models.Data
{
    public class AuthTokenData
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
