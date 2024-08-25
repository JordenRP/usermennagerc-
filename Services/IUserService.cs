using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementApp.Models;

namespace UserManagementApp.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
        bool VerifyPassword(string inputPassword, string storedHash);
    }
}
