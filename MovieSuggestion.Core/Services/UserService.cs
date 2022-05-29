using MovieSuggestion.Core.Resources;
using MovieSuggestion.Core.UnitOfWorks;
using MovieSuggestion.Core.Utils;
using MovieSuggestion.Data.Entities;
using MovieSuggestion.Data.Enums;
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
            await _unitOfWork.UserPermissions.AddRangeAsync( new List<UserPermission> 
            {
                new UserPermission()
            {
                UserId = newUser.Id,
                Status = EntityStatus.Values.ACTIVE,
                Permission = Permission.Values.USER_MANAGE
            },
            new UserPermission()
            {
                UserId = newUser.Id,
                Status = EntityStatus.Values.ACTIVE,
                Permission = Permission.Values.MOVIE_MANAGE
            }
            });

            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.CommitAsync();
            return newUser;
        }

        public async Task<User> DeleteUser(User user)
        {
            user.Status = EntityStatus.Values.DELETED;
            await _unitOfWork.Users.Update(user);
            await _unitOfWork.CommitAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        public async Task<User> GetUserById(ulong id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }

        public async Task<string> Login(string email, string password)
        {
          
            //Find the user
            var user = await Task.FromResult(_unitOfWork.Users.Find(_ => _.Email == email ).FirstOrDefault());
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
