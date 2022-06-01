using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieSuggestion.Core.Clients;
using MovieSuggestion.Core.Services;
using MovieSuggestion.Core.UnitOfWorks;
using MovieSuggestion.Data.Contexts;
using MovieSuggestion.Data.Utils.MovieSuggestion.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMovieSuggestion.Core.Services;

namespace MovieSuggestion.Core.Utils
{
    public static class DIRegister
    {
        public static void AddDIRegister(this IServiceCollection services)
        {
            var connString = EnvironmentVariable.GetConfiguration().DbConnection;
            ServerVersion sv = ServerVersion.AutoDetect(connString);
            services.AddDbContext<MovieDbContext>(options => options.UseMySql(connString, sv), ServiceLifetime.Scoped);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserMovieService, UserMovieService>();
            services.AddScoped<IUserMovieNoteService, UserMovieNoteService>();
            services.AddHttpClient<MovieClient>();
        }
    }
}
