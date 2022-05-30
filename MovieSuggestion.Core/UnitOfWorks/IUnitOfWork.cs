using MovieSuggestion.Core.Clients;
using MovieSuggestion.Core.Repositories;
using MovieSuggestion.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Movie> Movies { get; }
        IRepository<UserPermission> UserPermissions { get; }
        IRepository<UserMovie> UserMovies { get; }
        IRepository<UserMovieNote> UserMovieNotes { get; }
        MovieClient MovieClients { get; }
        Task<int> CommitAsync();
    }
}
