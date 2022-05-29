using MovieSuggestion.Core.Clients;
using MovieSuggestion.Core.Repositories;
using MovieSuggestion.Data.Contexts;
using MovieSuggestion.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public UnitOfWork(MovieDbContext context, MovieClient movieClient)
        {
            _context = context;
            _movieClient = movieClient;
        }
        public IRepository<User> Users => _userRepository = _userRepository ?? new Repository<User>(_context);
        public IRepository<Movie> Movies => _movieRepository = _movieRepository ?? new Repository<Movie>(_context);
        public MovieClient MovieClients => _movieClient = _movieClient ?? new MovieClient(new System.Net.Http.HttpClient());

        public IRepository<UserPermission> UserPermissions => _userPermissionRepository = _userPermissionRepository ?? new Repository<UserPermission>(_context);

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
