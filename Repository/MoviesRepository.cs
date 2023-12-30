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

		public bool CreateMovie(int distributerId, int categoryId, Movie movie)
		{
			var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();
		    movie.Distributer = _context.Distributers.Where(d => d.Id ==  distributerId).FirstOrDefault(); //many to one relationship many movies to one distributer.

			var movieCategory = new MovieCategory()
			{
				Category = category,
				Movie = movie,
			};

			_context.Add(movieCategory);

			_context.Add(movie);

			return Save();
		}

		public Movie GetMovie(int id)
		{
			return _context.Movies.Where(m => m.Id == id).FirstOrDefault();
		}

		public Movie GetMovie(string title)
		{
			return _context.Movies.Where(m => m.Title == title).FirstOrDefault();
		}

		public decimal GetMovieRaiting(int movieId)
		{
			var rating =  _context.Reviews.Where(m => m.Movie.Id == movieId);
			if (rating.Count() <= 0)
			{
				return 0;
			}
			return ((decimal)rating.Sum(r => r.Rating) / rating.Count());
		}

		public ICollection<Movie> GetMovies()
		{
			return _context.Movies.OrderBy(p => p.Id).ToList();
		}

		public bool MovieExists(int movieId)
		{
			return _context.Movies.Any(m => m.Id == movieId);
		}

		public bool Save()
		{
			var result = _context.SaveChanges();
			return result > 0 ? true : false;
		}

		public bool UpdateMovie(int distributerId, int categoryId, Movie movie)
		{
			_context.Update(movie);
			return Save();
		}
	}
}
