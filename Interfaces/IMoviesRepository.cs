using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces
{
	public interface IMoviesRepository
	{
		ICollection<Movie> GetMovies();
		Movie GetMovie(int id);
		Movie GetMovie(string title);
		decimal GetMovieRaiting(int movieId);
		bool MovieExists(int movieId);
	}
}
