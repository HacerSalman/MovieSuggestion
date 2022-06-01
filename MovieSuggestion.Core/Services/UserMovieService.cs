using MovieSuggestion.Core.Models;
using MovieSuggestion.Core.UnitOfWorks;
using MovieSuggestion.Data.Entities;
using MovieSuggestion.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMovieSuggestion.Core.Services;

namespace MovieSuggestion.Core.Services
{
    public class UserMovieService : IUserMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserMovieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<UserMovie> CreateUserMovie(UserMovie newUserMovie)
        {
            await _unitOfWork.UserMovies.AddAsync(newUserMovie);
            _unitOfWork.Commit();
            return newUserMovie;
        }

        public async Task<UserMovie> DeleteUserMovie(UserMovie userMovie)
        {
            userMovie.Status = EntityStatus.Values.DELETED;
            await _unitOfWork.UserMovies.Update(userMovie);
            _unitOfWork.Commit();
            return userMovie;
        }

        public async Task<PagedResponse<UserMovie>> GetActiveUserMovies(int? Skip, int? Take, ulong UserId)
        {
            var query = _unitOfWork.UserMovies.Find(_ => _.Status == EntityStatus.Values.ACTIVE && _.UserId == UserId);
            return new PagedResponse<UserMovie>()
            {
                Result = await Task.FromResult(query.Skip(Skip ?? 0).Take(Take ?? 20).ToList()),
                Paging = new Paging() { Limit = Take ?? 20, Offset = Skip ?? 0, Total = await Task.FromResult(query.Count()) }
            };
        }

        public async Task<PagedResponse<UserMovie>> GetAllUserMovies(int? Skip, int? Take, ulong UserId)
        {
            var query = _unitOfWork.UserMovies.Find(_ => _.UserId == UserId);
            return new PagedResponse<UserMovie>()
            {
                Result = await Task.FromResult(query.Skip(Skip ?? 0).Take(Take ?? 20).ToList()),
                Paging = new Paging() { Limit = Take ?? 20, Offset = Skip ?? 0, Total = await Task.FromResult(query.Count()) }
            };
        }

        public async Task<UserMovie> GetUserMovieByMovieId(ulong userId, ulong movieId)
        {
            return await Task.FromResult(_unitOfWork.UserMovies.Find(_ => _.MovieId == movieId && _.UserId == userId).FirstOrDefault());
        }

        public async Task<UserMovie> UpdateUserMovie(UserMovie userMovie)
        {
            await _unitOfWork.UserMovies.Update(userMovie);
            _unitOfWork.Commit();
            return userMovie;
        }
    }
}
