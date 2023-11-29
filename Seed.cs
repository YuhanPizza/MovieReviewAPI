using MovieReviewApp.Data;
using MovieReviewApp.Models;

namespace PokemonReviewApp
{
	public class Seed
	{
		private readonly DataContext dataContext;
		public Seed(DataContext context)
		{
			this.dataContext = context;
		}
		public void SeedDataContext()
		{
			if (!dataContext.Movies.Any())
			{
				var movie = new List<Movie>()
				{
					new Movie()
					{
						Title = "Barbie",
						Description ="The Barbies preside over Barbieland in a matriarchal system and work high-position jobs while the Kens spend time as futile subordinates living " +
						"in the Barbies' shadow. Beach Ken (Ryan Gosling) has feelings for Stereotypical Barbie (Margot Robbie) and constantly vies for her attention, but she doesn't recognize.",
						ReleaseDate = new DateTime(2023,7,9), //YY MM DD
						MovieCategories = new List<MovieCategory>()
						{
							new MovieCategory { Category = new Category() { Name = "Comedy"}},
							new MovieCategory { Category = new Category() { Name = "Action"}}
						},
						Reviews = new List<Review>()
						{
							new Review { Title="An Unforgettable Delight!",Text = "Oh, where do I even begin? Barbie 2023 is a tour de force that has left me utterly captivated, enchanted, and spellbound. ", Rating = 5,
							Reviewer = new Reviewer(){ FirstName = "Sanda", LastName = "Abunwar" } },
							new Review { Title="Barbie Movie", Text = "A Whimsical and Empowering Adventure for All Ages", Rating = 4,
							Reviewer = new Reviewer(){ FirstName = "Ajit", LastName = "Iyer" } },
							new Review { Title="Barbie",Text = "A Mesmerizing and Modern Masterpiece: The 2023 Barbie Movie Soars with Margot Robbie and Ryan Gosling!", Rating = 5,
							Reviewer = new Reviewer(){ FirstName = "Pozza", LastName = "Fc" } },
						},
						Distributer = new Distributer()
						{
							Company = "Warner Bros.",
							Address = "4000 Warner Boulevard Burbank, CA",
							Country = new Country()
							{
								Name = "USA"
							}
						}
					},
					new Movie()
					{
							Title = "Sherlock Holmes 3",
							Description ="Sherlock Holmes 3 is the follow up to 2011's Sherlock Holmes: A Game of Shadows, and the continuation of the journey of Robert Downey Jr.'s version of the titular character alongside Jude Law's Dr. John Watson.",
							ReleaseDate = new DateTime(2021,12,22),
							MovieCategories = new List<MovieCategory>()
							{
								new MovieCategory { Category = new Category() { Name = "Action"}},
								new MovieCategory { Category = new Category() { Name = "Mystery"}},
								new MovieCategory { Category = new Category() { Name = "Comedy"}}
							},
							Reviews = new List<Review>()
							{
								new Review { Title= "Bonkers good fun.", Text = "I loved it, it is an absolutely bonkers, of the wall thrill ride, and purists of the traditional Holmes stories will probably be appalled, but if you're after two hours of intense fun, and high energy excitement, you will love it.", Rating = 4,
								Reviewer = new Reviewer(){ FirstName = "Richard", LastName = "Novak" } },
								new Review { Title= "A Fine Game it is!",Text = "While we have new ingredients (= actors/characters) such as the girl formerly having a tattoo and a new bad guy, we also still have our beloved Holmes/Watson duo. And by that I mean the same actors in the role. Jude Law and especially Robert Downey Jr. having a lot of fun again and it shows.", Rating = 4,
								Reviewer = new Reviewer(){ FirstName = "Kos", LastName = "Mason" } },
								new Review { Title= "Elementary Holmes", Text = "In Sherlock Holmes: A Game of Shadows, my mind turns two ways: The first half is guns, gunpowder, and gymnastics. Sherlock Holmes (Robert Downey, Jr.) and Dr. Watson (Jude Law) contend with the salvation of civilization mostly through athletics, aided by director Guy Ritchie's considerable skill with the camera and graphics.", Rating = 5,
								Reviewer = new Reviewer(){ FirstName = "John", LastName = "DeSando" } },
							},
						Distributer = new Distributer()
						{
							Company = "Warner Bros.",
							Address = "4000 Warner Boulevard Burbank, CA",
							Country = new Country()
							{
								Name = "USA"
							}
						}
					},
					new Movie()
					{
							Title = "Avengers: Endgame",
							Description ="AVENGERS: ENDGAME is set after Thanos' catastrophic use of the Infinity Stones randomly wiped out half of Earth's population in Avengers: Infinity War. Those left behind are desperate to do something -- anything -- to bring back their lost loved ones.",
							ReleaseDate = new DateTime(2019,4,26),
							MovieCategories = new List<MovieCategory>()
							{
								new MovieCategory { Category = new Category() { Name = "Action"}},
								new MovieCategory { Category = new Category() { Name = "Science Fiction"}},
								new MovieCategory { Category = new Category() { Name = "Drama"}},
								new MovieCategory { Category = new Category() { Name = "Fantasy"}}
							},
							Reviews = new List<Review>()
							{
								new Review { Title="Perfect",Text = "Phenomenally entertaining and a masterpiece!!! The Russo brothers nailed it again!!! I'm so grateful they're directing these movies. It makes them so much better and everything is so well thought out. ", Rating = 5,
								Reviewer = new Reviewer(){ FirstName = "Sheikh", LastName = "Akbar" } },
								new Review { Title="Avengers Fanboy",Text = "Thank you, MCU, Kevin Feige and all the directors for giving us such a great time (mostly with all the movies). Thank you Russo Brothers ( The \"Real Heroes\") in directing the Civil War, Infinity War and finally giving their best (appropriately) in Endgame and putting up or even surpassing the expectations. It's the best time I had in a Cinema Hall after a  long time! ", Rating = 5,
								Reviewer = new Reviewer(){ FirstName = "Bhaskar", LastName = "Khurana" } },
								new Review { Title="Epic!",Text = "Where to begin, where to begin! You know a movie is outstanding when the end credits alone are more epic than the majority of films released in the last 20 years! This film is the pure definition of an emotional roller coaster and throughout its run time brings about fascination, humor, sadness, incredible excitement, and sheer finality.", Rating = 5,
								Reviewer = new Reviewer(){ FirstName = "Lorenzo", LastName = "Roman" } },
							},
						Distributer = new Distributer()
						{
							Company = "Walt Disney Studios Motion Pictures",
							Address = "500 South Buena Vista Street, Burbank",
							Country = new Country()
							{
								Name = "USA"
							}
						}
					}
				};
				dataContext.Movies.AddRange(movie);
				dataContext.SaveChanges();
			}
		}
	}
}