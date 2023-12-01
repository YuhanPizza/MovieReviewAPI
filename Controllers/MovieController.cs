using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Data;
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

		public MovieController(IMoviesRepository moviesRepository, DataContext context) 
		{
			_moviesRepository = moviesRepository;
			_context = context;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
		public IActionResult GetPokemons()
		{
			var movies = _moviesRepository.GetMovies();

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(movies);
		}
	}
}
