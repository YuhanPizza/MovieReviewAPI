using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Data;
using MovieReviewApp.Models;
using MovieReviewApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieReviewApp.Tests.Repository
{
	public class MoviesRepositoryTest
	{
		private async Task<DataContext> GetDatabaseContext()
		{
			var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			var databaseContext = new DataContext(options);
			databaseContext.Database.EnsureCreated();
			if(await databaseContext.Movies.CountAsync() <= 0)
			{
				for (int i = 0; i < 10; i++)
				{
					databaseContext.Movies.Add(
						new Movie()
						{
							Title = "Sherlock Holmes 3",
							Description = "Sherlock Holmes 3 is the follow up to 2011's Sherlock Holmes: A Game of Shadows, and the continuation of the journey of Robert Downey Jr.'s version of the titular character alongside Jude Law's Dr. John Watson.",
							ReleaseDate = new DateTime(2021, 12, 22),
							MovieCategories = new List<MovieCategory>()
							{
								new MovieCategory { Category = new Category() { Name = "Action"}},
								new MovieCategory { Category = new Category() { Name = "Mystery"}},
								new MovieCategory { Category = new Category() { Name = "Comedy"}}
							},
							Reviews = new List<Review>()
							{
								new Review { Title= "Bonkers good fun.", Text = "I loved it, it is an absolutely bonkers, of the wall thrill ride, and purists of the traditional Holmes stories will probably be appalled, but if you're after two hours of intense fun, and high energy excitement, you will love it.", Rating = 4,
								Reviewer = new Reviewer(){ FirstName = "Richard", LastName = "Novak" } },
								new Review { Title= "A Fine Game it is!",Text = "While we have new ingredients (= actors/characters) such as the girl formerly having a tattoo and a new bad guy, we also still have our beloved Holmes/Watson duo. And by that I mean the same actors in the role. Jude Law and especially Robert Downey Jr. having a lot of fun again and it shows.", Rating = 4,
								Reviewer = new Reviewer(){ FirstName = "Kos", LastName = "Mason" } },
								new Review { Title= "Elementary Holmes", Text = "In Sherlock Holmes: A Game of Shadows, my mind turns two ways: The first half is guns, gunpowder, and gymnastics. Sherlock Holmes (Robert Downey, Jr.) and Dr. Watson (Jude Law) contend with the salvation of civilization mostly through athletics, aided by director Guy Ritchie's considerable skill with the camera and graphics.", Rating = 5,
								Reviewer = new Reviewer(){ FirstName = "John", LastName = "DeSando" } },
							},
							Distributer = new Distributer()
							{
								Company = "Warner Bros.",
								Address = "4000 Warner Boulevard Burbank, CA",
								Country = new Country()
								{
									Name = "USA"
								}
							}
						});
					await databaseContext.SaveChangesAsync();
				}
			}
			return databaseContext;
		}

		[Fact]
		public async void MoviesRepository_GetmovieString_ReturnMovie()
		{
			//arrange
			var title = "Sherlock Holmes 3";
			var dbContext = await GetDatabaseContext(); //creates the database we made before
			var moviesRepository = new MoviesRepository(dbContext); //passes that database as a new database

			//Act
			var result = moviesRepository.GetMovie(title);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(Movie));
		}

		[Fact]
		public async void MoviesRepository_GetmovieInt_ReturnMovie()
		{
			//arrange
			int movieId = 1;
			var dbContext = await GetDatabaseContext(); //creates the database we made before
			var moviesRepository = new MoviesRepository(dbContext); //passes that database as a new database

			//Act
			var result = moviesRepository.GetMovie(movieId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(Movie));
		}

		[Fact]
		public async void MoviesRepository_GetMovieRaiting_ReturnDecimal()
		{
			//Arrange
			int movieId = 1;
			var dbContext = await GetDatabaseContext();
			var moviesRepository = new MoviesRepository(dbContext);

			//Act
			var result = moviesRepository.GetMovieRaiting(movieId);

			//Assert
			result.Should().NotBe(0);
			result.Should().BeInRange(1, 5);
			result.Should().BeOfType(typeof(decimal));
		}

		[Fact]
		public async void MoviesRepository_GetMovies_ReturnICollectionMovies()
		{
			//Arrange
			var dbContext = await GetDatabaseContext();
			var moviesRepository = new MoviesRepository(dbContext);

			//Act
			var result = moviesRepository.GetMovies();

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(List<Movie>));
		}

		[Fact]
		public async void MoviesRepository_MovieExists_ReturnTrue()
		{
			//Arrange
			int movieId = 1;
			var dbContext = await GetDatabaseContext();
			var moviesRepository = new MoviesRepository(dbContext);

			//ACT
			var result = moviesRepository.MovieExists(movieId);

			//Assert
			result.Should().NotBe(false);
			result.Should().Be(true);
		}
    }
}
