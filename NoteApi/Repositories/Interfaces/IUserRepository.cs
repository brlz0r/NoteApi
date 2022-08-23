using NoteApi.Models.Data;

namespace NoteApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(UserData data);
        Task UpdateUserAsync(UserData data);
        Task RemoveUserAsync(UserData data);
        Task<UserData> GetUserByIdAsync(Guid id);
        Task<UserData> GetUserByUsernameAsync(string username);
        Task<UserData> GetUserByRefreshTokenAsync(string refreshToken);
    }
}
