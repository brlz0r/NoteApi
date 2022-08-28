using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NoteApi.Models.Data;
using NoteApi.Models.Entities;
using NoteApi.Repositories.Interfaces;

namespace NoteApi.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDatabaseContext _ctx;
        private readonly IMapper _mapper;

        public UserRepository(AppDatabaseContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task AddUserAsync(UserData data)
        {
            await _ctx.AddAsync(_mapper.Map<User>(data));
            await _ctx.SaveChangesAsync();
        }

        public async Task<UserData> GetUserByIdAsync(Guid id)
        {
            var data = await _ctx.Users
                .Include(u => u.RefreshToken)
                .FirstOrDefaultAsync(u => u.Id == id);
            return _mapper.Map<UserData>(data);
        }

        public async Task<UserData> GetUserByRefreshTokenAsync(string refreshToken)
        {
            var data = await _ctx.Users
               .Include(u => u.RefreshToken)
               .FirstOrDefaultAsync(u => u.RefreshToken.Token.Equals(refreshToken));
            return _mapper.Map<UserData>(data);
        }
        public async Task<UserData> GetUserByUsernameAsync(string username)
        {
            var data = await _ctx.Users
              .Include(u => u.RefreshToken)
              .FirstOrDefaultAsync(u => u.Username.Equals(username));
            return _mapper.Map<UserData>(data);
        }

        // TODO 
        public async Task RemoveUserAsync(UserData data)
        {
            await Task.Run(() => _ctx.Remove(_mapper.Map<User>(data)));
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserData data)
        {
            var user = await GetUserEntityByIdAsync(data.Id);

            user.Username = data.UserName;
            user.Password = data.Password;
            user.RefreshToken.Created = data.RefreshToken.Created;
            user.RefreshToken.Expires = data.RefreshToken.Expires;
            user.RefreshToken.Token = data.RefreshToken.Token;

            await _ctx.SaveChangesAsync();
        }

        private async Task<User> GetUserEntityByIdAsync(Guid id)
        {
            var data = await _ctx.Users
                .Include(u => u.RefreshToken)
                .FirstOrDefaultAsync(u => u.Id == id);
            return data;
        }
    }
}
