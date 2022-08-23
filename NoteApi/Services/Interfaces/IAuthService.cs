using NoteApi.Models.Data;

namespace NoteApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegistrationAsync(AuthData data);
        Task<AuthTokenData> AuthenticateAsync(AuthData data);
        Task<AuthTokenData> RefreshTokenAsync(RefreshTokenData data);
    }
}
