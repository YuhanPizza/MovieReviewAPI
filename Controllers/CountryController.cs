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
	public class CountryController: Controller
	{
		private ICountryRepository _countryRepository;
		private IMapper _mapper;
		private DataContext _context;

		public CountryController(ICountryRepository countryRepository, IMapper mapper, DataContext context)
        {
			_countryRepository = countryRepository;
			_mapper = mapper;
			_context = context;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
		public IActionResult GetCountries()
		{
			var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(countries);
		}
		[HttpGet("{countryId}")]
		[ProducesResponseType(200, Type = typeof(Country))]
		[ProducesResponseType(400)]
		public IActionResult GetCountry(int countryId)
		{
			if (!_countryRepository.CountryExists(countryId))
			{
				return NotFound();
			}
			var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(country);
		}
		[HttpGet("/distributer/{distributerId}")]
		[ProducesResponseType(200, Type = typeof(Country))]
		[ProducesResponseType(400)]
		public IActionResult GetCountryOfAnDistributer(int distributerId)
		{
			var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByDistributer(distributerId));
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(country);
		}
	}
}
