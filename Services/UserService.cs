using InteriorDesigner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InteriorDesigner.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserModel> userManager;
        private readonly IConfiguration configuration;

        public UserService(UserManager<UserModel> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public string GenerateTokenString(LoginUserModel userToLogin)
        {
            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userToLogin.UserName),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("Jwt:Key").Value));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: configuration.GetSection("Jwt:Issuer").Value,
                audience: configuration.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return tokenString;
        }

        public async Task<UserModel?> GetUserByIdAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<UserModel?> GetUserByUsernameAsync(string username)
        {
            return await userManager.FindByNameAsync(username);
        }

        public async Task<bool> LoginAsync(LoginUserModel userToLogin)
        {
            UserModel user = await userManager.FindByNameAsync(userToLogin.UserName);
            return await userManager.CheckPasswordAsync(user, userToLogin.Password);
        }

        public async Task<IdentityResult> RegisterAsync(LoginUserModel userToRegister)
        {
            UserModel newUser = new UserModel
            {
                UserName = userToRegister.UserName,
                Email = userToRegister.UserName
            };
            return await userManager.CreateAsync(newUser, userToRegister.Password);
        }
    }
}
