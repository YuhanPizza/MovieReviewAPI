using MovieReviewApp.Dto;
using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces
{
	public interface ICategoryRepository
	{
		ICollection<Category> GetCategories();
		Category GetCategory(int id);
		ICollection<Movie> GetMovieByCategory(int id);
		bool CategoryExists(int id);
		bool CreateCategory (Category category);
		bool UpdateCategory (Category category);
		Category GetCategoriesTrimToUpper(CategoryDto categories);
		bool DeleteCategory (Category category);
		bool Save();
	}
}
