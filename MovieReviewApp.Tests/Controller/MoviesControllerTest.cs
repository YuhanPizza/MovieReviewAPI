using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Controllers;
using MovieReviewApp.Data;
using MovieReviewApp.Dto;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieReviewApp.Tests.Controller
{
	public class MoviesControllerTest
	{
		private readonly IMoviesRepository _moviesRepositories;
		private readonly IReviewRepository _reviewRepositories;
		private readonly IMapper _mapper;

		public MoviesControllerTest() //constructor
        {
			//faking all required repo, data, mapper
            _moviesRepositories = A.Fake<IMoviesRepository>();
			_reviewRepositories = A.Fake<IReviewRepository>();
			_mapper = A.Fake<IMapper>();
        }

		//tests
		[Fact]
		public void MovieController_GetMovies_ReturnOk()
		{
			//Arrange
			var movies = A.Fake<ICollection<MovieDto>>();
			var moviesList = A.Fake<List<MovieDto>>();
			A.CallTo(() => _mapper.Map<List<MovieDto>>(movies)).Returns(moviesList);
			var controller = new MovieController(_moviesRepositories,_mapper, _reviewRepositories);

		    //Act
			var result = controller.GetMovies();

			//Assert
			result.Should().NotBeNull(); //not null
			result.Should().BeOfType(typeof(OkObjectResult)); //okresult
		}

		[Fact]
		public void MovieController_GetMovie_ReturnOk()
		{
			//Arrange
			int movieId = 1;
			A.CallTo(()=> _moviesRepositories.MovieExists(movieId)).Returns(true);
			var controller = new MovieController(_moviesRepositories, _mapper, _reviewRepositories);

			//Act
			var result = controller.GetMovie(movieId);


			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void MovieController_CreateMovie_ReturnOk()
		{
			//Arrange
			int distributerId = 1;
			int categoryId = 2;
			var movie = A.Fake<Movie>();
			var movieCreate = A.Fake<MovieDto>();
			var movies = A.Fake<ICollection<MovieDto>>();
			var moviesList = A.Fake<List<MovieDto>>();
			A.CallTo(() => _moviesRepositories.GetMoviesTrimToUpper(movieCreate)).Returns(null);
			A.CallTo(() => _mapper.Map<Movie>(movieCreate)).Returns(movie);
			A.CallTo(() => _moviesRepositories.CreateMovie(distributerId, categoryId, movie)).Returns(true);
			var controller = new MovieController(_moviesRepositories, _mapper, _reviewRepositories);

			//Act
			var result = controller.CreateMovie(distributerId, categoryId, movieCreate);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}
		[Fact]
		public void MovieController_UpdateMovie_ResultOk()
		{
			//Arrange
			int distributerId = 1;
			int categoryId = 2;
			int movieId = 3;
			var movie = A.Fake<Movie>();
			var movieUpdate = A.Fake<MovieDto>();
			movieUpdate.Id = movieId;
			A.CallTo(() => _moviesRepositories.MovieExists(movieId)).Returns(true);
			A.CallTo(()=> _mapper.Map<Movie>(movieUpdate)).Returns(movie);
			A.CallTo(()=> _moviesRepositories.UpdateMovie(distributerId,categoryId, movie)).Returns(true);
			var controller = new MovieController(_moviesRepositories, _mapper, _reviewRepositories);

			//Act
			var result = controller.UpdateMovie(movieId, distributerId, categoryId, movieUpdate);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}
		[Fact]
		public void MovieController_DeleteMovie_ResultOk()
		{
			//Arrange
			int movieId = 3;
			var movie = A.Fake<Movie>();
			var review = A.Fake<Review>();
			var reviewsList = A.Fake<List<Review>>();
			var reviews = A.Fake<ICollection<Review>>();
			A.CallTo(() => _moviesRepositories.MovieExists(movieId)).Returns(true);
			A.CallTo(() => _reviewRepositories.GetReviewsOfAMovie(movieId)).Returns(reviews);
			A.CallTo(() => _moviesRepositories.GetMovie(movieId)).Returns(movie);
			A.CallTo(() => _reviewRepositories.DeleteReviews(reviewsList)).Returns(true);
			A.CallTo(() => _moviesRepositories.DeleteMovie(movie)).Returns(true);
			var controller = new MovieController(_moviesRepositories, _mapper, _reviewRepositories);

			//Act
			var result = controller.DeleteMovie(movieId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}
	}
}
