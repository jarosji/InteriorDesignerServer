using InteriorDesigner.Models;
using Microsoft.AspNetCore.Identity;

namespace InteriorDesigner.Services
{
    public interface IUserService
    {
        Task<UserModel?> GetUserByIdAsync(string id);
        Task<UserModel?> GetUserByUsernameAsync(string username);

        Task<IdentityResult> RegisterAsync(LoginUserModel userToRegister);
        Task<bool> LoginAsync(LoginUserModel userToLogin);

        string GenerateTokenString(LoginUserModel userToLogin);
    }
}
