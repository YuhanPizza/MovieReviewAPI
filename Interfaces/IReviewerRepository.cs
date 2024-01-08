using MovieReviewApp.Dto;
using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces
{
	public interface IReviewerRepository
	{
		ICollection<Reviewer> GetReviewers();
		Reviewer GetReviewer(int reviewerId);
		ICollection<Review> GetReviewsByReviewer(int reviewerId);
		bool ReviewerExists(int reviewerId);
		bool CreateReviewer(Reviewer reviewer);
		Reviewer GetReviewersTrimToUpper(ReviewerDto reviewersDto);
		bool UpdateReviewer(Reviewer reviewer);
		bool DeleteReviewer(Reviewer reviewer);
		bool Save();
	}
}
