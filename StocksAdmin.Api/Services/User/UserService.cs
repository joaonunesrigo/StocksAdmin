using Microsoft.EntityFrameworkCore;
using ProductClientHub.Api.DataBase;
using ProjectClientHub.Communication.Requests.User;
using ProjectClientHub.Communication.Responses.Users;

namespace ProductClientHub.Api.Services.User
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

        public bool ValidateLogin(UserLoginRequest userLoginRequest)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == userLoginRequest.Email);

            if (user == null)
            {
                return false;
            }

            var userRequestPasswordHash = GenerateHashPassword(userLoginRequest.Password);

            if (userRequestPasswordHash.Equals(user.Password))
            {
                return true;
            }

            return false;
        }

        public string GenerateHashPassword(string password)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = System.Security.Cryptography.SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
