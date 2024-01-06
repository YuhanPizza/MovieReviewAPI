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
		private readonly ICountryRepository _countryRepository;
		private readonly IMapper _mapper;

		public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
			_countryRepository = countryRepository;
			_mapper = mapper;
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
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
		{
			if (countryCreate == null) 
			{
				return BadRequest(ModelState);
			}
			var country = _countryRepository.GetCountriesTrimToUpper(countryCreate);

			//Error Handling
			if(country != null)
			{
				ModelState.AddModelError("", "Country Already Exists");
				return StatusCode(422, ModelState);
			}
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var countryMap = _mapper.Map<Country>(countryCreate);

			if (!_countryRepository.CreateCountry(countryMap))
			{
				ModelState.AddModelError("", "Something went wrong during save");
				return StatusCode(500, ModelState);
			}
			
			return Ok("Successfully created!");
		}
		[HttpPut("{countryId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto updatedCountry)
		{
			if (updatedCountry == null)
			{
				return BadRequest(ModelState);
			}

			if (countryId != updatedCountry.Id)
			{
				return BadRequest(ModelState);
			}

			if (!_countryRepository.CountryExists(countryId))
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var countryMap = _mapper.Map<Country>(updatedCountry);

			if (!_countryRepository.UpdateCountry(countryMap))
			{
				ModelState.AddModelError("", "Something went wrong during Update[Country]");
				return StatusCode(500, ModelState);
			}

			return Ok("Country Successfully Updated!");
		}
		[HttpDelete("{countryId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public IActionResult DeleteCountry(int countryId)
		{
			if (!_countryRepository.CountryExists(countryId))
			{
				return NotFound();
			}

			var countryDelete = _countryRepository.GetCountry(countryId);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (!_countryRepository.DeleteCountry(countryDelete))
			{
				ModelState.AddModelError("", "Something went wrong Removing Country");
			}

			return Ok("Country Sucessfully Removed!");
		}
	}
}
