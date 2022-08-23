using System.Text.Json.Serialization;

namespace NoteApi.Models.Data
{
    public class UserData
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public RefreshTokenData RefreshToken { get; set; }
    }
}
