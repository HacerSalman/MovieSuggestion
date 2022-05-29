using MovieSuggestion.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSuggestion.Job.Jobs
{
    public class MovieListJob
    {
        private readonly IMovieService _movieService;
        public MovieListJob(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public void Process()
        {
            try
            {
                Console.WriteLine(@"👌 Job Started MovieListJob : " + DateTime.Now);
                //Get movie list
                _movieService.GetMovieListFromClient();

                Console.WriteLine(@"👌 Job Finished MovieListJob : " + DateTime.Now);
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"🤔 Job Exception MovieListJob " + ex);
                throw;
            }
        }
    }
}
