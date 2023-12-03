using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces
{
	public interface ICategoryRepository
	{
		ICollection<Category> GetCategories();
		Category GetCategory(int id);
		ICollection<Movie> GetMovieByCategory(int id);
		bool CategoryExists(int id);
	}
}
