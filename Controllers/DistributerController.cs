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
	public class DistributerController : Controller
	{
		private readonly IDistributerRepository _distributerRepository;
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public DistributerController(IDistributerRepository distributerRepository, DataContext context, IMapper mapper)
		{
			_distributerRepository = distributerRepository;
			_context = context;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Distributer>))]
		public IActionResult GetDistributers()
		{
			var distributers = _mapper.Map<List<DistributerDto>>(_distributerRepository.GetDistributers());

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(distributers);
		}
		[HttpGet("{distributerId}")]
		[ProducesResponseType(200, Type = typeof(Distributer))]
		[ProducesResponseType(400)]
		public IActionResult GetDistributer(int distributerId)
		{
			if (!_distributerRepository.DistributerExists(distributerId))
			{
				return NotFound();
			}
			var distributer = _mapper.Map<DistributerDto>(_distributerRepository.GetDistributer(distributerId));
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(distributer);
		}
		[HttpGet("{distributerId}/movie")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
		[ProducesResponseType(400)]
		public IActionResult GetMovieByDistributerId(int distributerId)
		{
			if (!_distributerRepository.DistributerExists(distributerId))
			{
				return NotFound();
			}
			var movies = _mapper.Map<List<MovieDto>>(_distributerRepository.GetMovieByDistributer(distributerId));
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(movies);
		}
	}
}
