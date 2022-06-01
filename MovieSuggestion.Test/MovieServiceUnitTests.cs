using Moq;
using MovieSuggestion.Core.Repositories;
using MovieSuggestion.Core.Services;
using MovieSuggestion.Core.UnitOfWorks;
using MovieSuggestion.Core.Utils;
using MovieSuggestion.Data.Entities;
using MovieSuggestion.Data.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Test
{
    public class MovieServiceUnitTests
    {
        List<Movie> movieList;
        MovieService movieService;
        [SetUp]
        public void Setup()
        {
            //Arrange
            movieList = new List<Movie>()
                                         {
                                             new Movie(){Id = 1,Title = "UnitTest1",Status=EntityStatus.Values.ACTIVE, CreateTime = DateTimeUtils.GetCurrentTicks(),Modifier = "Unittest",Owner = "Unittest",UpdateTime=  DateTimeUtils.GetCurrentTicks(), OriginalTitle = "UnitTest1", Overview = "UnitTest1", SourceId =1},
                                             new Movie(){Id = 2,Title = "UnitTest2",Status=EntityStatus.Values.ACTIVE, CreateTime = DateTimeUtils.GetCurrentTicks(),Modifier = "Unittest",Owner = "Unittest",UpdateTime=  DateTimeUtils.GetCurrentTicks(),  OriginalTitle = "UnitTest2", Overview = "UnitTest2", SourceId =2},
                                             new Movie(){Id = 3,Title = "UnitTest3",Status=EntityStatus.Values.ACTIVE, CreateTime = DateTimeUtils.GetCurrentTicks(),Modifier = "Unittest",Owner = "Unittest",UpdateTime=  DateTimeUtils.GetCurrentTicks(),  OriginalTitle = "UnitTest3", Overview = "UnitTest3", SourceId =3}

                                         };

            Mock<IRepository<Movie>> mock = new Mock<IRepository<Movie>>();
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
      
            unitOfWork.Setup(m => m.Movies).Returns(mock.Object);
            //Here we are going to mock repository GetAll method 
            unitOfWork.Setup(m => m.Movies.GetAllAsync().Result).Returns(movieList);
            unitOfWork.Setup(m => m.Movies.Find(_ => _.Status == EntityStatus.Values.ACTIVE)).Returns(movieList);
            unitOfWork.Setup(m => m.Movies.GetByIdAsync(It.IsAny<ulong>()).Result).Returns((ulong target) =>
            {
                return movieList.Where(_ => _.Id == target).FirstOrDefault();
            });
      
            //Here we are going to mock repository Add method
            unitOfWork.Setup(m => m.Movies.AddAsync(It.IsAny<Movie>()).Result).Returns((Movie target) =>
            {
                var original = movieList.FirstOrDefault(
                    q => q.Id == target.Id);

                if (original != null)
                {
                    return original;
                }

                movieList.Add(target);

                return target;
            });


            unitOfWork.Setup(m => m.Movies.Update(It.IsAny<Movie>()).Result).Returns((Movie target) =>
            {
                var original = movieList.FirstOrDefault(
                    q => q.Id == target.Id);

                original.Title = target.Title;
                return true;
            });

            movieService = new MovieService(unitOfWork.Object);
        }

        [Test]
        public void CreateMovie()
        {

            var movieName = "UnitTest4";
            var movie = movieService.CreateMovie(new Movie
            {
                Id = 4,
                CreateTime = DateTimeUtils.GetCurrentTicks(),
                UpdateTime = DateTimeUtils.GetCurrentTicks(),
                Modifier = "Unittest",
                Owner = "Unittest",
                Title = movieName,
                Status = EntityStatus.Values.ACTIVE,
                Adult = false,
                OriginalTitle = movieName,
                Overview = movieName,
                SourceId = 4
                
            }).Result;
            var result = movieService.GetMovieById(movie.Id).Result;
            Assert.AreEqual(movieName, result.Title);
        }


        [Test]
        public void GetActiveMovielist()
        {
            var result = movieService.GetActiveMovies(0,20).Result.Result;
            Assert.True(result.Count > 0);
        }

        [Test]
        public void UpdateMovie()
        {
            ulong movieId = 2;
            var updateMovieTitle = "UnitTest5";
            var movie = movieService.GetMovieById(movieId).Result;
            movie.Title = updateMovieTitle;
            var result = movieService.UpdateMovie(movie).Result;
            Assert.AreEqual(updateMovieTitle, result.Title);
        }


        [Test]
        public void GetMovielist()
        {
            var result = movieService.GetAllMovies(0,20).Result.Result;
            Assert.True(result.Count > 0);
        }

        [Test]
        public void DeleteMovie()
        {
            ulong movieId = 2;
            var movie = movieService.GetMovieById(movieId).Result;
            var result = movieService.DeleteMovie(movie).Result;
            Assert.AreEqual(EntityStatus.Values.DELETED, result.Status);
        }
    }
}
