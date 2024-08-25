using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserManagementApp.Models;
using UserManagementApp.Repositories;

namespace UserManagementApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            user.PasswordHash = HashPassword(user.PasswordHash);
            await _userRepository.CreateUserAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            user.PasswordHash = HashPassword(user.PasswordHash);
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteUserAsync(id);
        }

        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            // Хэшируем введенный пароль
            var hashedInput = HashPassword(inputPassword);
            return hashedInput == storedHash;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Преобразуем строку пароля в массив байтов
                var byteArray = Encoding.UTF8.GetBytes(password);
                // Вычисляем хэш
                var hashBytes = sha256.ComputeHash(byteArray);
                // Преобразуем байтовый массив в строку в формате Base64
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
