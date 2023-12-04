using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces
{
	public interface IReviewRepository
	{
		ICollection<Review> GetReviews();
		Review GetReview(int id);
		ICollection<Review> GetReviewsOfAMovie(int movieId);
		bool ReviewExists(int reviewId);
	}
}
