using MovieSuggestion.Core.Models;
using MovieSuggestion.Core.UnitOfWork;
using MovieSuggestion.Data.Entities;
using MovieSuggestion.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MovieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Movie> CreateMovie(Movie newMovie)
        {
            await _unitOfWork.Movies.AddAsync(newMovie);
            await _unitOfWork.CommitAsync();
            return newMovie;
        }

        public async Task<Movie> DeleteMovie(Movie movie)
        {
            movie.Status = EntityStatus.Values.DELETED;
            await _unitOfWork.Movies.Update(movie);
            await _unitOfWork.CommitAsync();
            return movie;
        }

        public async Task<PagedResponse<Movie>> GetActiveMovies(int? Skip, int? Take)
        {
            var query =  _unitOfWork.Movies.Find(_ => _.Status == EntityStatus.Values.ACTIVE);
            return new PagedResponse<Movie>()
            {
                Result = await Task.FromResult(query.Skip(Skip ?? 0).Take(Take ?? 20).ToList()),
                Paging = new Paging() { Limit = Take ?? 20, Offset = Skip ?? 0, Total = await Task.FromResult(query.Count()) }
            };
        }

        public async Task<PagedResponse<Movie>> GetAllMovies(int? Skip, int? Take)
        {
            var query = await _unitOfWork.Movies.GetAllAsync();
            return new PagedResponse<Movie>()
            {
                Result = query.Skip(Skip ?? 0).Take(Take ?? 20).ToList(),
                Paging = new Paging() { Limit = Take ?? 20, Offset = Skip ?? 0, Total = query.Count() }
            };
        }

        public async Task<Movie> GetMovieById(ulong id)
        {
            return await _unitOfWork.Movies.GetByIdAsync(id);
        }

        public async Task<PagedResponse<Movie>> GetMoviesByTitle(string title, int? Skip, int? Take)
        {
            var query = _unitOfWork.Movies.Find(_ => _.Title.Contains(title));
            return new PagedResponse<Movie>()
            {
                Result = await Task.FromResult(query.Skip(Skip ?? 0).Take(Take ?? 20).ToList()),
                Paging = new Paging() { Limit = Take ?? 20, Offset = Skip ?? 0, Total = await Task.FromResult(query.Count()) }
            };
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            await _unitOfWork.Movies.Update(movie);
            await _unitOfWork.CommitAsync();
            return movie;
        }
    }
}
