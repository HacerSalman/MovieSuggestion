using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieSuggestion.Core.Clients;
using MovieSuggestion.Core.Repositories;
using MovieSuggestion.Core.Utils;
using MovieSuggestion.Data.Contexts;
using MovieSuggestion.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieDbContext _context;
        private MovieClient _movieClient;
        private Repository<User> _userRepository;
        private Repository<Movie> _movieRepository;
        private Repository<UserPermission> _userPermissionRepository;
        private Repository<UserMovie> _userMovieRepository;
        private Repository<UserMovieNote> _userMovieNoteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitOfWork(MovieDbContext context, MovieClient movieClient, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _movieClient = movieClient;
            _httpContextAccessor = httpContextAccessor;
        }
        public IRepository<User> Users => _userRepository = _userRepository ?? new Repository<User>(_context, _httpContextAccessor);
        public IRepository<Movie> Movies => _movieRepository = _movieRepository ?? new Repository<Movie>(_context, _httpContextAccessor);
        public MovieClient MovieClients => _movieClient = _movieClient ?? new MovieClient(new System.Net.Http.HttpClient());
        public IRepository<UserPermission> UserPermissions => _userPermissionRepository = _userPermissionRepository ?? new Repository<UserPermission>(_context, _httpContextAccessor);

        public IRepository<UserMovie> UserMovies => _userMovieRepository = _userMovieRepository ?? new Repository<UserMovie>(_context, _httpContextAccessor);

        public IRepository<UserMovieNote> UserMovieNotes => _userMovieNoteRepository = _userMovieNoteRepository ?? new Repository<UserMovieNote>(_context, _httpContextAccessor);
        public async Task<int> CommitAsync()
        {          
         
            return await _context.SaveChangesAsync();
        }
 
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
