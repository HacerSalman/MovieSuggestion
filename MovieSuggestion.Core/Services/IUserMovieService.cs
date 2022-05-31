using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieSuggestion.Core.Models;
using MovieSuggestion.Data.Entities;

namespace UserMovieSuggestion.Core.Services
{
    public interface IUserMovieService
    {
        Task<PagedResponse<UserMovie>> GetAllUserMovies(int? Skip, int? Take, ulong UserId);
        Task<PagedResponse<UserMovie>> GetActiveUserMovies(int? Skip, int? Take, ulong UserId);
        Task<UserMovie> GetUserMovieByMovieId(ulong UserId,ulong movieId);
        Task<UserMovie> CreateUserMovie(UserMovie newUserMovie);
        Task<UserMovie> UpdateUserMovie(UserMovie userMovie);
        Task<UserMovie> DeleteUserMovie(UserMovie userMovie);
    }
}
