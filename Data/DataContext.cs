using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Models;

namespace MovieReviewApp.Data
{
	public class DataContext : DbContext //brought in from entityframeworkcore
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options) //base is going to shove all of the data thats gonna be recieved from an outside class into your db object
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Distributer> Distributers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; } //many to many
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        //Whenever you set up your entity framework and you set up your
        //entity framework context you have to manipulate your tables in some ways
        //on modelcreating is how you do that without going to your tables
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<MovieCategory>()
                .HasKey(mc => new { mc.MovieId, mc.CategoryId }); // how you link the two id's together
            modelBuilder.Entity<MovieCategory>()
                .HasOne(m => m.Movie)
                .WithMany(mc => mc.MovieCategories) //many to many, many movies go with many categories.
                .HasForeignKey(c => c.MovieId);
			modelBuilder.Entity<MovieCategory>()
				.HasOne(m => m.Category)
				.WithMany(mc => mc.MovieCategories) //many to many, many categories go with many movies.
				.HasForeignKey(c => c.CategoryId);
		}
	}
}
