using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Data;
using MovieReviewApp.Dto;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Repository;

namespace MovieReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MovieController : Controller
	{
		private readonly IMoviesRepository _moviesRepository;
		private readonly DataContext _context;
		private readonly IMapper _mapper; //when doing data conversions you need data mapper because you could just convert the movie model to moviedto
		private readonly IReviewRepository _reviewRepository;

		public MovieController(IMoviesRepository moviesRepository, DataContext context, IMapper mapper, IReviewRepository reviewRepository) 
		{
			_moviesRepository = moviesRepository;
			_reviewRepository = reviewRepository;
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
		public IActionResult GetMovies()
		{
			var movies = _mapper.Map<List<MovieDto>>(_moviesRepository.GetMovies());
			//without automapper 
			//var movie = _moviesRepository.GetMovie(movieId);
			//var recievedMovie = new MovieDto
			//{
			//	Id = movie.Id,
			//	Title = movie.Title,
			//	Description = movie.Description,
			//	ReleaseDate = movie.ReleaseDate,
			//}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(movies);
		}

		[HttpGet("{movieId}")]
		[ProducesResponseType(200,Type = typeof(Movie))]
		[ProducesResponseType(400)]
		public IActionResult GetMovie(int movieId) 
		{
			if (!_moviesRepository.MovieExists(movieId))
			{
				return NotFound();
			}

			var movie = _mapper.Map<MovieDto>(_moviesRepository.GetMovie(movieId)); //does validation and maps it for us
			//without automapper 
			//var movie = _moviesRepository.GetMovie(movieId);
			//var recievedMovie = new MovieDto
			//{
			//	Id = movie.Id,
			//	Title = movie.Title,
			//	Description = movie.Description,
			//	ReleaseDate = movie.ReleaseDate,
			//}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(movie);
		}
		[HttpGet("{movieId}/rating")]
		[ProducesResponseType(200, Type = typeof(decimal))]
		[ProducesResponseType(400)]
		public IActionResult GetMovieRating(int movieId)
		{
			if (!_moviesRepository.MovieExists(movieId))
			{
				return NotFound();
			}
			var rating = _moviesRepository.GetMovieRaiting(movieId);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(rating);
		}

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateMovie([FromQuery] int distributerId,[FromQuery] int categoryId,[FromBody] MovieDto movieCreate)
		{
			if (movieCreate == null)
			{
				return BadRequest(ModelState);
			}
			var movie = _moviesRepository.GetMovies()
				.Where(c => c.Title.Trim().ToUpper() == movieCreate.Title.TrimEnd().ToUpper())
				.FirstOrDefault();

			//Error Handling
			if (movie != null)
			{
				ModelState.AddModelError("", "Movie Already Exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var movieMap = _mapper.Map<Movie>(movieCreate);

			if (!_moviesRepository.CreateMovie(distributerId,categoryId, movieMap))
			{
				ModelState.AddModelError("", "Something went wrong during save");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully created!");
		}

		[HttpPut("{movieId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public IActionResult UpdateMovie(int movieId,[FromQuery] int distributerId, [FromQuery] int categoryId, [FromBody] MovieDto updatedMovie)
		{
			if (updatedMovie == null)
			{
				return BadRequest(ModelState);
			}

			if (movieId != updatedMovie.Id)
			{
				return BadRequest(ModelState);
			}

			if (!_moviesRepository.MovieExists(movieId))
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var movieMap = _mapper.Map<Movie>(updatedMovie);

			if (!_moviesRepository.UpdateMovie(distributerId,categoryId,movieMap))
			{
				ModelState.AddModelError("", "Something went wrong during Update[Movie]");
				return StatusCode(500, ModelState);
			}

			return Ok("Movie Successfully Updated!");
		}
		[HttpDelete("{movieId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public IActionResult DeleteMovie(int movieId)
		{
			if (!_moviesRepository.MovieExists(movieId))
			{
				return NotFound();
			}

			var reviewsDelete = _reviewRepository.GetReviewsOfAMovie(movieId);

			var movieDelete = _moviesRepository.GetMovie(movieId);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (!_reviewRepository.DeleteReviews(reviewsDelete.ToList()))//delete range method
			{
				ModelState.AddModelError("", "Error removing Reviews!");
			}

			if (!_moviesRepository.DeleteMovie(movieDelete))
			{
				ModelState.AddModelError("", "Something went wrong Removing Movie");
			}

			return Ok("Movie Sucessfully Removed!");
		}
	}
}
