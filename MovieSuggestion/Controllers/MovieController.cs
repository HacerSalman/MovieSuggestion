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
            return _mapper.Map<List<Movie>, List<MovieDTO>>(movieList.Result);
        }

        [HttpGet]
        [MovieAuthorize(UP.MOVIE_MANAGE)]
        public async Task<ActionResult<UserMovieDTO>> GetMovieById(ulong id)
        {
            var movie = await _movieService.GetMovieById(id);
            var movieDTO = _mapper.Map<Movie, MovieDTO>(movie);
            var userMovieDTO = new UserMovieDTO() { Movie = movieDTO };
            userMovieDTO.NoteList = await _userMovieNoteService.GetUserMovieNotesByMovieId(id);
            var userMovie = await _userMovieService.GetUserMovieByMovieId(id);
            if (userMovie != null)
                userMovieDTO.UserScore = userMovie.Score;
            return userMovieDTO;
        }


        [HttpPost]
        [MovieAuthorize(UP.MOVIE_MANAGE)]
        public async Task<ActionResult<UserMovieDTO>> CreateUserMovie([FromBody] UserMovieCreateDTO movieDTO)
        {
            var movie = _mapper.Map<UserMovieCreateDTO, UserMovie>(movieDTO);
            var newMovie = await _userMovieService.CreateUserMovie(movie);
            return _mapper.Map<UserMovie, UserMovieDTO>(newMovie);
        }

        [HttpPut]
        [MovieAuthorize(UP.MOVIE_MANAGE)]
        public async Task<ActionResult<UserMovieDTO>> UpdateUserMovie([FromBody] UserMovieUpdateDTO movieDTO)
        {
            var userMovie = _mapper.Map<UserMovieUpdateDTO, UserMovie>(movieDTO);
            var newuserMovie = await _userMovieService.UpdateUserMovie(userMovie);
            return _mapper.Map<UserMovie, UserMovieDTO>(newuserMovie);
        }

        [HttpPost]
        [MovieAuthorize(UP.MOVIE_MANAGE)]
        public async Task<ActionResult<UserMovieNoteDTO>> AddNoteToMovie([FromBody] UserMovieNoteDTO movieDTO)
        {
            var userMovieNote = _mapper.Map<UserMovieNoteDTO, UserMovieNote>(movieDTO);
            var newUserMovieNote = await _userMovieNoteService.CreateUserMovieNote(userMovieNote);
            return _mapper.Map<UserMovieNote, UserMovieNoteDTO>(newUserMovieNote);
        }

        [HttpPost]
        [MovieAuthorize(UP.MOVIE_MANAGE)]
        public ActionResult<bool> SuggestMovie([FromBody] SuggestMovieDTO suggestMovieDTO)
        {
           return  _movieService.SuggestMovie(suggestMovieDTO.MailTo, suggestMovieDTO.Subject, suggestMovieDTO.Body);
       
        }

    }
}
