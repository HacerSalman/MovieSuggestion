using Microsoft.Extensions.DependencyInjection;
using MovieSuggestion.Core.Clients;
using MovieSuggestion.Core.Services;
using MovieSuggestion.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.Utils
{
    public static class DIRegister
    {
        public static void AddDIRegister(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IUserService, UserService>();
            services.AddHttpClient<MovieClient>();
        }
    }
}
