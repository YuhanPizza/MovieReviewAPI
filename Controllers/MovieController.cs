using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Data;
using MovieReviewApp.Dto;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;

namespace MovieReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MovieController : Controller
	{
		private readonly IMoviesRepository _moviesRepository;
		private readonly DataContext _context;
		private readonly IMapper _mapper; //when doing data conversions you need data mapper because you could just convert the movie model to moviedto

		public MovieController(IMoviesRepository moviesRepository, DataContext context, IMapper mapper) 
		{
			_moviesRepository = moviesRepository;
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
	}
}
