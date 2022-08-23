using NoteApi.Models.Data;

namespace NoteApi.Utils.interfaces
{
    public interface ITokens
    {
        string GenerateJwtToken(UserData user);
        Guid DecryptToken(string token);
        RefreshTokenData GenerateRefreshToken();
    }
}
