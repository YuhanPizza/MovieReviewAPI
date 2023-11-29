namespace MovieReviewApp.Models
{
	public class Review
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public int Rating { get; set; }
		public Reviewer Reviewer { get; set; } //1
		public Movie Movie { get; set; } //1
	}
}
