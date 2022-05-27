using MovieSuggestion.Core.Resources;
using MovieSuggestion.Core.UnitOfWork;
using MovieSuggestion.Core.Utils;
using MovieSuggestion.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> CreateUser(User newUser)
        {
            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.CommitAsync();
            return newUser;
        }

        public async Task<User> DeleteUser(User User)
        {
            _unitOfWork.Users.Remove(User);
            await _unitOfWork.CommitAsync();
            return User;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        public async Task<User> GetUserById(ulong id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }

        public async Task<string> Login(string Email, string password)
        {
          
            //Find the user
            var user = await Task.FromResult(_unitOfWork.Users.Find(_ => _.Email == Email ).FirstOrDefault());
            if (user == null)
                return null;

            //Verify password
            if (!ClaimPrincipal.VerifyPassword(password, user.Password))
                throw new InvalidOperationException(Resource.INCORRECT_PASSWORD);

            //Get user permissions
            var userPermissions = _unitOfWork.UserPermissions.Find(_ => _.UserId == user.Id).Select(_ => _.Permission.ToString()).ToList();

            //Get user jwt token
            return await Task.FromResult(ClaimPrincipal.GenerateToken(user.Id.ToString(), userPermissions));

        }

        public async Task<User> UpdateUser(User User)
        {
            await _unitOfWork.Users.Update(User);
            await _unitOfWork.CommitAsync();
            return User;
        }
    }
}
