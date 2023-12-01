using MovieReviewApp.Data;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;

namespace MovieReviewApp.Repository
{
	public class MoviesRepository : IMoviesRepository
	{
		private readonly DataContext _context;

		public MoviesRepository(DataContext context)
        {
			_context = context;
		}

		public ICollection<Movie> GetMovies()
		{
			return _context.Movies.OrderBy(p => p.Id).ToList();
		}
    }
}
