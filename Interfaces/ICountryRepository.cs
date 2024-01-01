using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces
{
	public interface ICountryRepository
	{
		ICollection<Country> GetCountries();
		Country GetCountry(int id);
		Country GetCountryByDistributer(int distributerId);
		ICollection <Distributer> GetDistributersFromACountry(int id);
		bool CountryExists(int id);
		bool CreateCountry(Country country);
		bool UpdateCountry(Country country);
		bool DeleteCountry(Country country);
		bool Save();
	}
}
