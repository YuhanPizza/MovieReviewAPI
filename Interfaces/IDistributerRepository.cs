using MovieReviewApp.Dto;
using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces
{
	public interface IDistributerRepository
	{
		ICollection<Distributer> GetDistributers();
		Distributer GetDistributer(int id);
		Distributer GetDistributerOfAMovie(int moveId);
		public ICollection<Movie> GetMovieByDistributer(int distributerId);
		bool DistributerExists(int distributerId);
		bool CreateDistributer(Distributer distributer);
		bool UpdateDistributer(Distributer distributer);
		Distributer GetDistributersTrimToUpper(DistributerDto distributerDto);
		bool DeleteDistributer(Distributer distributer);
		bool Save();
	}
}
