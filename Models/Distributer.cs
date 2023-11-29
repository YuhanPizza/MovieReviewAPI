namespace MovieReviewApp.Models
{
	public class Distributer //owner
	{
        public int Id { get; set; }
		public string Company { get; set; }
		public string Address { get; set; }
		public Country Country { get; set; }
		public ICollection<Movie> Movies { get; set; } //1 to many
    }
}
