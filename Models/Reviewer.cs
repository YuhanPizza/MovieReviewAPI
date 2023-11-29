namespace MovieReviewApp.Models
{
	public class Reviewer
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ICollection<Review> Reviews { get; set;} //icollection is like a list but it cant be edited doesnt have as much functionality as a list
	}
}
