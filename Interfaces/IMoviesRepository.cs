using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces
{
	public interface IMoviesRepository
	{
		ICollection<Movie> GetMovies();

	}
}
