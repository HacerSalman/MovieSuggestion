using MovieSuggestion.Core.Models;
using MovieSuggestion.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.Services
{
    public interface IMovieService
    {
        Task<PagedResponse<Movie>> GetAllMovies(int? Skip, int? Take);
        Task<PagedResponse<Movie>> GetActiveMovies(int? Skip, int? Take);
        Task<PagedResponse<Movie>> GetMoviesByTitle(string name, int? Skip, int? Take);
        Task<Movie> GetMovieById(ulong id);
        Task<Movie> CreateMovie(Movie newMovie);
        Task<Movie> UpdateMovie(Movie movie);
        Task<Movie> DeleteMovie(Movie movie);
    }
}
