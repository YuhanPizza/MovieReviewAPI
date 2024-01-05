using MovieReviewApp.Data;
using MovieReviewApp.Dto;
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

		public bool CreateCategory(Category category)
		{
			// Change Tracker
			// tracks: Adding, Updating, Modifyng
			// state can be connected/ disconnected
			// EntityState.Added

			_context.Add(category);
			//var result = _context.SaveChanges();
			// return result > 0 ? true: false;
			return Save();
		}

		public bool DeleteCategory(Category category)
		{
			_context.Remove(category);
			return Save();
		}

		public ICollection<Category> GetCategories()
		{
			return _context.Categories.OrderBy(c => c.Name).ToList();
		}

		public Category GetCategoriesTrimToUpper(CategoryDto categories) => GetCategories()
				.Where(c => c.Name.Trim().ToUpper() == categories.Name.TrimEnd().ToUpper())
				.FirstOrDefault();


		public Category GetCategory(int id) => _context.Categories.Where(c => c.Id == id).FirstOrDefault();

		public ICollection<Movie> GetMovieByCategory(int id) => _context.MovieCategories.Where(mc => mc.CategoryId == id).Select(c => c.Movie).ToList();
		public bool Save()
		{
			//savechanges: 
			// turns it into sql and 
			// creating it in the database
			var result = _context.SaveChanges();
			return result > 0 ? true: false;
		}

		public bool UpdateCategory(Category category)
		{
			_context.Update(category);
			return Save();
		}
	}
}
