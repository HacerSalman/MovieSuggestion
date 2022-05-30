﻿using MovieSuggestion.Core.Models;
using MovieSuggestion.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.Services
{
    public interface IUserMovieNoteService
    {
        Task<PagedResponse<UserMovieNote>> GetAllUserMovieNotes(int? Skip, int? Take, ulong UserId);
        Task<PagedResponse<UserMovieNote>> GetActiveUserMovieNotes(int? Skip, int? Take, ulong UserId);
        Task<List<string>> GetUserMovieNotesByMovieId(ulong movieId);
        Task<UserMovieNote> CreateUserMovieNote(UserMovieNote newUserMovieNote);
        Task<UserMovieNote> UpdateUserMovieNote(UserMovieNote userMovie);
        Task<UserMovieNote> DeleteUserMovieNote(UserMovieNote userMovie);
    }
}
