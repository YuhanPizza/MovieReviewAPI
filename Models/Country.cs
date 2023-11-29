namespace MovieReviewApp.Models
{
	public class Country
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Distributer> Distributers { get; set; }
	}
}
