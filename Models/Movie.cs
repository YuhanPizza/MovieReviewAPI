namespace MovieReviewApp.Models
{
	//plain old CLR objects / Models
	//used to represent database
	public class Movie
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime ReleaseDate { get; set; }
		public Distributer Distributer { get; set; } 
		public ICollection<Review> Reviews { get; set; }
		public ICollection<MovieCategory> MovieCategories { get; set; } //many to many
	}
}
