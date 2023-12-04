using MovieReviewApp.Data;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;

namespace MovieReviewApp.Repository
{
	public class ReviewRepository : IReviewRepository
	{
		private readonly DataContext _context;

		public ReviewRepository(DataContext context)
        {
			_context = context;
		}
        public Review GetReview(int id)
		{
			return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
		}

		public ICollection<Review> GetReviews()
		{
			return _context.Reviews.ToList();
		}

		public ICollection<Review> GetReviewsOfAMovie(int movieId)
		{
			return _context.Reviews.Where(r => r.Movie.Id == movieId).ToList();
		}

		public bool ReviewExists(int reviewId)
		{
			return _context.Reviews.Any(r => r.Id == reviewId);
		}
	}
}
