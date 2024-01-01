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
		private readonly ICountryRepository _countryRepository;
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public DistributerController(IDistributerRepository distributerRepository,ICountryRepository countryRepository, DataContext context, IMapper mapper)
		{
			_distributerRepository = distributerRepository;
			_countryRepository = countryRepository;
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

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateDistributer([FromQuery] int countryId, [FromBody] DistributerDto distributerCreate)
		{
			if (distributerCreate == null)
			{
				return BadRequest(ModelState);
			}
			var distributer = _distributerRepository.GetDistributers()
				.Where(d => d.Company.Trim().ToUpper() == distributerCreate.Company.TrimEnd().ToUpper())
				.FirstOrDefault();

			//Error Handling
			if (distributer != null)
			{
				ModelState.AddModelError("", "Distributer Already Exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var distributerMap = _mapper.Map<Distributer>(distributerCreate);

			distributerMap.Country = _countryRepository.GetCountry(countryId);

			if (!_distributerRepository.CreateDistributer(distributerMap))
			{
				ModelState.AddModelError("", "Something went wrong during save");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully created!");
		}
		[HttpPut("{distributerId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public IActionResult UpdateDistributer(int distributerId, [FromBody] DistributerDto updatedDistributer)
		{
			if (updatedDistributer == null)
			{
				return BadRequest(ModelState);
			}

			if (distributerId != updatedDistributer.Id)
			{
				return BadRequest(ModelState);
			}

			if (!_distributerRepository.DistributerExists(distributerId))
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var distributerMap = _mapper.Map<Distributer>(updatedDistributer);

			if (!_distributerRepository.UpdateDistributer(distributerMap))
			{
				ModelState.AddModelError("", "Something went wrong during Update[Distributer]");
				return StatusCode(500, ModelState);
			}

			return Ok("Distributer Successfully Updated!");
		}
		[HttpDelete("{distributerId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public IActionResult DeleteDistributer(int distributerId)
		{
			if (!_distributerRepository.DistributerExists(distributerId))
			{
				return NotFound();
			}

			var distributerDelete = _distributerRepository.GetDistributer(distributerId);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (!_distributerRepository.DeleteDistributer(distributerDelete))
			{
				ModelState.AddModelError("", "Something went wrong Removing Distributer");
			}

			return Ok("Distributer Sucessfully Removed!");
		}
	}
}
