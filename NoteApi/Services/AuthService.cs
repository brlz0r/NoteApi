using NoteApi.Models.Data;
using NoteApi.Models.HttpException;
using NoteApi.Repositories.Interfaces;
using NoteApi.Services.Interfaces;
using NoteApi.Utils.interfaces;
using NoteApi.Utils.Interfaces;

namespace NoteApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHesher;
        private readonly ITokens _tokens;

        public AuthService(IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokens tokens)
        {
            _userRepository = userRepository;
            _passwordHesher = passwordHasher;
            _tokens = tokens;
        }
        public async Task<AuthTokenData> AuthenticateAsync(AuthData data)
        {
            var user = await _userRepository.GetUserByUsernameAsync(data.Username);
            
            if (user == null) {
                throw new NotFoundException("User not found");
            }

            var verifiedPassword = _passwordHesher.Verify(user.Password, data.Password);

            if (!verifiedPassword)
            {
                throw new NotFoundException("Username or password incorrect");
            }

            var refreshToken = await UpdateRefreshTokenAsync(user);

            var tokenData = new AuthTokenData()
            {
                AccessToken = _tokens.GenerateJwtToken(user),
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.Expires,
            };

            return tokenData;

        }

        public async Task<Guid> GetUserIdByTokenAsync(string token)
        {
            return await Task.Run(() => _tokens.DecryptToken(token));
        }

        public async Task<AuthTokenData> RefreshTokenAsync(RefreshTokenData data)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(data.Token);

            if (user == null)
            {
                throw new NotFoundException("Refresh token not found");
            }

            if (user.RefreshToken is not { IsActive: true })
            {
                throw new BadHttpRequestException("Refresh Token is not active");
            }

            var newRefreshToken = await UpdateRefreshTokenAsync(user);

            var tokenData = new AuthTokenData()
            {
                AccessToken = _tokens.GenerateJwtToken(user),
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.Expires,
            };

            return tokenData;
        }

        public async Task RegistrationAsync(AuthData data)
        {
            var existUser = await _userRepository.GetUserByUsernameAsync(data.Username);

            if (existUser != null)
            {
                throw new BadHttpRequestException("User is exist");
            }

            await _userRepository.AddUserAsync(new UserData
            {
                UserName = data.Username,
                Password = _passwordHesher.Generate(data.Password), 
                RefreshToken = _tokens.GenerateRefreshToken()
            });
        }

        private async Task<RefreshTokenData> UpdateRefreshTokenAsync(UserData data)
        {
            var id = data.RefreshToken.Id;
            data.RefreshToken = _tokens.GenerateRefreshToken();
            data.RefreshToken.Id = id;
            await _userRepository.UpdateUserAsync(data);
            return data.RefreshToken;
        }
    }
}
