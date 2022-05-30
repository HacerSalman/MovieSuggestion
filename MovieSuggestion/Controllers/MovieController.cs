using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieSuggestion.Core.DTO;
using MovieSuggestion.Core.Services;
using MovieSuggestion.Core.Utils;
using MovieSuggestion.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMovieSuggestion.Core.Services;
using UP = MovieSuggestion.Data.Enums.Permission.Values;

namespace MovieSuggestion.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IUserMovieService _userMovieService;
        private readonly IUserMovieNoteService _userMovieNoteService;
        private readonly IMapper _mapper;
        public MovieController(IMovieService movieService, IMapper mapper, IUserMovieService userMovieService, IUserMovieNoteService userMovieNoteService)
        {
            _movieService = movieService;
            _mapper = mapper;
            _userMovieService = userMovieService;
            _userMovieNoteService = userMovieNoteService;
        }


        [HttpGet]
        [MovieAuthorize(UP.MOVIE_MANAGE)]
        public async Task<ActionResult<List<MovieDTO>>> GetMovieList(int? Skip, int? Take)
        {
            var movieList = await _movieService.GetAllMovies(Skip, Take);
            return _mapper.Map<IEnumerable<Movie>, List<MovieDTO>>(movieList.Result);
        }

        [HttpGet]
        [MovieAuthorize(UP.MOVIE_MANAGE)]
        public async Task<ActionResult<UserMovieDTO>> GetMovieById(ulong id)
        {
            var userMovie = await _userMovieService.GetUserMovieByMovieId(id);
            userMovie.NoteList = await _userMovieNoteService.GetUserMovieNotesByMovieId(id); 
            return _mapper.Map<UserMovie, UserMovieDTO>(userMovie);
        }


        [HttpPut]
        [MovieAuthorize(UP.MOVIE_MANAGE)]
        public async Task<ActionResult<MovieDTO>> CreateUserMovie([FromBody] UserMovieCreateDTO movieDTO)
        {
            var movie = _mapper.Map<UserMovieCreateDTO, Movie>(movieDTO); //todo
            var newMovie = await _movieService.UpdateMovie(movie);
            return _mapper.Map<Movie, MovieDTO>(newMovie);
        }

   
    }
}
