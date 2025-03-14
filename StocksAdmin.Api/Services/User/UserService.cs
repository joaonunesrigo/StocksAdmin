using Microsoft.EntityFrameworkCore;
using StocksAdmin.Api.DataBase;
using StocksAdmin.Communication.Requests.User;
using StocksAdmin.Communication.Responses.Users;
using Claims = System.Security.Claims;

namespace StocksAdmin.Api.Services.User
{
    public class UserService
    {
        private readonly DataBaseContext _dbContext;

        public UserService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserResponse> Register(UserRegisterRequest userRegisterRequest)
        {
            if (await _dbContext.Users.AnyAsync(u => u.Email == userRegisterRequest.Email))
            {
                throw new Exception("Usuário já existe.");
            }

            var hashedPassword = GenerateHashPassword(userRegisterRequest.Password);

            var newUser = new Entities.User
            {
                Nome = userRegisterRequest.UserName,
                Password = hashedPassword,
                Email = userRegisterRequest.Email
            };

            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return new UserResponse()
            {
                UserName = newUser.Nome,
                UserId = newUser.Id
            };
        }

        public long ValidateLogin(UserLoginRequest userLoginRequest)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == userLoginRequest.Email);

            if (user == null)
            {
                return 0;
            }

            var userRequestPasswordHash = GenerateHashPassword(userLoginRequest.Password);

            if (userRequestPasswordHash.Equals(user.Password))
            {
                return user.Id;
            }

            return 0;
        }

        public string GenerateHashPassword(string password)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = System.Security.Cryptography.SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }

        public Entities.User GetUserById(long userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            return user;
        }

        public long GetIdUserAuthenticated(Claims.ClaimsPrincipal user)
        {
            var userId = Convert.ToInt64(user.FindFirst(Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (userId == 0)
            {
                throw new Exception("Não foi possível achar o ID do usuário logado");
            }

            return userId;
        }
    }
}
