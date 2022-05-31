using MovieSuggestion.Core.Clients;
using MovieSuggestion.Core.Models;
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
            var query = _unitOfWork.Movies.Find(_ => _.Status == EntityStatus.Values.ACTIVE);
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

        public void GetMovieListFromClient()
        {
            var totalPages = 100;
            for (var page = 1; page <= totalPages; page++)
            {
                var input = new Dictionary<string, string>();
                input.Add("page", page.ToString());
                var movieList =  _unitOfWork.MovieClients.ListNowPlaying(input);
                if (movieList == null)
                    break;
                if (movieList.TotalPages < totalPages)
                    totalPages = movieList.TotalPages;
                 SaveMovieListToDB(movieList.Results);
            }

        }

        private void SaveMovieListToDB(List<MovieClient.MovieList> results)
        {
            var movieList = results.Select(m => new Movie()
            {
                Adult = m.Adult,
                BackdropPath = m.BackdropPath,
                OriginalLanguage = m.OriginalLanguage,
                OriginalTitle = m.OriginalTitle,
                Overview = m.Overview,
                Popularity = m.Popularity,
                PosterPath = m.PosterPath,
                Score = m.Score,
                SourceId = m.Id,
                Status = EntityStatus.Values.ACTIVE,
                Title = m.Title,
                Video = m.Video
            }).ToList();

            foreach (var movie in movieList)
            {
                try
                {
                    var mv = _unitOfWork.Movies.Find(m => m.SourceId == movie.SourceId).FirstOrDefault();
                    if (mv != null)
                         _unitOfWork.Movies.Update(movie);
                    else
                         _unitOfWork.Movies.AddAsync(movie);

                    _unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }
             
            }

           
        }

        public bool SuggestMovie(string mailTo, string subject, string body)
        {
            try
            {
                MailUtils.SendMail(subject, body, mailTo);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
          
        }
    }
}
