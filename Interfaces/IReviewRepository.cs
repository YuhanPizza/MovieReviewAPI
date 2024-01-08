using MovieReviewApp.Dto;
using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces
{
	public interface IReviewRepository
	{
		ICollection<Review> GetReviews();
		Review GetReview(int id);
		ICollection<Review> GetReviewsOfAMovie(int movieId);
		bool ReviewExists(int reviewId);

		bool CreateReview(int reviewerId, int movieId, Review review);
		Review GetReviewsTrimToUpper(ReviewDto reviewDto);
		bool UpdateReview(Review review);
		bool DeleteReview(Review review);
		bool DeleteReviews(List<Review> reviews);
		bool Save();
	}
}
