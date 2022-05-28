using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MovieSuggestion.Data.Utils.MovieSuggestion.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Data.Contexts
{
    public class MovieDbContextFactory : IDesignTimeDbContextFactory<MovieDbContext>
    {
        public MovieDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MovieDbContext>();
            var connString = EnvironmentVariable.GetConfiguration().DbConnection;
            ServerVersion sv = ServerVersion.AutoDetect(connString);
            optionsBuilder.UseMySql(connString, sv);
            optionsBuilder.EnableSensitiveDataLogging();

            return new MovieDbContext(optionsBuilder.Options);
        }
    }
}
