using MovieReviewApp.Data;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;

namespace MovieReviewApp.Repository
{
	public class DistributerRepository : IDistributerRepository
	{
		private readonly DataContext _context;
        public DistributerRepository(DataContext context)
        {
            _context = context;
        }
        public bool DistributerExists(int distributerId)
		{
			return _context.Distributers.Any(d => d.Id == distributerId);
		}

		public Distributer GetDistributer(int id)
		{
			return _context.Distributers.Where(d => d.Id == id).FirstOrDefault();
		}

		public Distributer GetDistributerOfAMovie(int moveId)
		{
			return _context.Movies.Where(m => m.Id ==  moveId).Select(d => d.Distributer).FirstOrDefault();
		}

		public ICollection<Distributer> GetDistributers()
		{
			return _context.Distributers.ToList();
		}

		public ICollection<Movie> GetMovieByDistributer(int distributerId) 
		{
			return _context.Movies.Where(m => m.Distributer.Id == distributerId).ToList();
		}
	}
}
