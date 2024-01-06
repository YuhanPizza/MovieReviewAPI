using AutoMapper;
using MovieReviewApp.Data;
using MovieReviewApp.Dto;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;

namespace MovieReviewApp.Repository
{
	public class CountryRepository : ICountryRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;
		public CountryRepository(DataContext context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}
        public bool CountryExists(int id) => _context.Countries.Any(c => c.Id == id);

		public bool CreateCountry(Country country)
		{
			_context.Add(country);
			return Save();
		}

		public bool DeleteCountry(Country country)
		{
			_context.Remove(country);
			return Save();
		}

		public ICollection<Country> GetCountries() => _context.Countries.OrderBy(c => c.Name).ToList();

		public Country GetCountriesTrimToUpper(CountryDto countryCreate) => GetCountries().Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper())
				.FirstOrDefault();

		public Country GetCountry(int id) => _context.Countries.Where(c => c.Id == id).FirstOrDefault();

		public Country GetCountryByDistributer(int distributerId) => _context.Distributers.Where(d => d.Id == distributerId).Select(c => c.Country).FirstOrDefault();


		public ICollection<Distributer> GetDistributersFromACountry(int id) => _context.Distributers.Where(c => c.Country.Id == id).ToList();

		public bool Save()
		{
			var result = _context.SaveChanges();
			return result > 0 ? true : false;
		}

		public bool UpdateCountry(Country country)
		{
			_context.Update(country);
			return Save();
		}
	}
}
