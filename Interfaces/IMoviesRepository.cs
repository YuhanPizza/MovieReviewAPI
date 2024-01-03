using MovieReviewApp.Dto;
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
		bool CreateMovie(int distributerId, int categoryId, Movie movie);
		bool UpdateMovie(int distributerId, int categoryId, Movie movie);
		Movie GetMoviesTrimToUpper(MovieDto movies);
		bool DeleteMovie(Movie movie);
		bool Save();
	}
}
