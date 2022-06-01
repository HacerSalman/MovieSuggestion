using MovieSuggestion.Core.Models;
using MovieSuggestion.Core.UnitOfWorks;
using MovieSuggestion.Data.Entities;
using MovieSuggestion.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.Services
{
    public class UserMovieNoteService : IUserMovieNoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserMovieNoteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<UserMovieNote> CreateUserMovieNote(UserMovieNote newUserMovieNote)
        {
            await _unitOfWork.UserMovieNotes.AddAsync(newUserMovieNote);
            _unitOfWork.Commit();
            return newUserMovieNote;
        }

        public async Task<UserMovieNote> DeleteUserMovieNote(UserMovieNote userMovieNote)
        {
            userMovieNote.Status = EntityStatus.Values.DELETED;
            await _unitOfWork.UserMovieNotes.Update(userMovieNote);
            _unitOfWork.Commit();
            return userMovieNote;
        }

        public async Task<PagedResponse<UserMovieNote>> GetActiveUserMovieNotes(int? Skip, int? Take, ulong UserId)
        {
            var query = _unitOfWork.UserMovieNotes.Find(_ => _.Status == EntityStatus.Values.ACTIVE && _.UserId == UserId);
            return new PagedResponse<UserMovieNote>()
            {
                Result = await Task.FromResult(query.Skip(Skip ?? 0).Take(Take ?? 20).ToList()),
                Paging = new Paging() { Limit = Take ?? 20, Offset = Skip ?? 0, Total = await Task.FromResult(query.Count()) }
            };
        }

        public async Task<PagedResponse<UserMovieNote>> GetAllUserMovieNotes(int? Skip, int? Take, ulong UserId)
        {
            var query = _unitOfWork.UserMovieNotes.Find(_ => _.UserId == UserId);
            return new PagedResponse<UserMovieNote>()
            {
                Result = await Task.FromResult(query.Skip(Skip ?? 0).Take(Take ?? 20).ToList()),
                Paging = new Paging() { Limit = Take ?? 20, Offset = Skip ?? 0, Total = await Task.FromResult(query.Count()) }
            };
        }

        public async Task<List<string>> GetUserMovieNotesByMovieId(ulong userId, ulong movieId)
        {
            return await Task.FromResult(_unitOfWork.UserMovieNotes.Find(_ => _.MovieId == movieId && _.UserId == userId).Select(_=> _.Note).ToList());
        }

        public async Task<UserMovieNote> UpdateUserMovieNote(UserMovieNote userMovieNote)
        {
            await _unitOfWork.UserMovieNotes.Update(userMovieNote);
            _unitOfWork.Commit();
            return userMovieNote;
        }
    }
}
