using MovieReviewApp.Data;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;

namespace MovieReviewApp.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly DataContext _context;
        public CategoryRepository(DataContext context)
        {
			_context = context;   
        }
        public bool CategoryExists(int id)
		{
			return _context.Categories.Any(c => c.Id == id);
		}

		public ICollection<Category> GetCategories()
		{
			return _context.Categories.OrderBy(c => c.Name).ToList();
		}

		public Category GetCategory(int id)
		{
			return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
		}

		public ICollection<Movie> GetMovieByCategory(int id)
		{
			return _context.MovieCategories.Where( mc => mc.CategoryId == id).Select( c => c.Movie).ToList();
		}
	}
}
