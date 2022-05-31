using MovieSuggestion.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(ulong id);
        Task<User> CreateUser(User newUser);
        Task<User> UpdateUser(User User);
        Task<User> DeleteUser(User User);

        Task<string> Login(string Email, string password);
        Task<bool> CreateUserPermission(ulong id);
    }
}
