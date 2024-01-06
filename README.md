# Welcome to MovieReviewAPI! [Working on Unit Testing]🌟

![MovieReviewAPI-Get-Post-Put-Delete](https://github.com/YuhanPizza/MovieReviewAPI/assets/107896556/96b4f05c-9a22-4165-8a0b-9490b884577d)

Hey there! MovieReviewAPI is your go-to platform for managing movie reviews through a simple and powerful API built with ASP.NET MVC Core 6.0. With this API, you can perform various operations on different entities like Categories, Countries, Distributers, Movies, Reviews, and Reviewers.

## API Endpoints 🚀

### Category

- **GET** `/api/Category`
  - Retrieve all categories.

- **POST** `/api/Category`
  - Create a new category.

- **GET** `/api/Category/{categoryId}`
  - Retrieve details of a specific category.

- **PUT** `/api/Category/{categoryId}`
  - Update a specific category.

- **DELETE** `/api/Category/{categoryId}`
  - Delete a specific category.

- **GET** `/api/Category/movie/{categoryId}`
  - Retrieve movies associated with a specific category.

### Country

- **GET** `/api/Country`
  - Retrieve all countries.

- **POST** `/api/Country`
  - Create a new country.

- **GET** `/api/Country/{countryId}`
  - Retrieve details of a specific country.

- **PUT** `/api/Country/{countryId}`
  - Update a specific country.

- **DELETE** `/api/Country/{countryId}`
  - Delete a specific country.

- **GET** `/distributer/{distributerId}`
  - Retrieve distributors associated with a specific country.

### Distributer

- **GET** `/api/Distributer`
  - Retrieve all distributors.

- **POST** `/api/Distributer`
  - Create a new distributor.

![GetById](https://github.com/YuhanPizza/MovieReviewAPI/assets/107896556/7220e1d8-3335-49fb-8fe1-f5d607ea41ba)
- **GET** `/api/Distributer/{distributerId}`
  - Retrieve details of a specific distributor.

- **PUT** `/api/Distributer/{distributerId}`
  - Update a specific distributor.

- **DELETE** `/api/Distributer/{distributerId}`
  - Delete a specific distributor.

- **GET** `/api/Distributer/{distributerId}/movie`
  - Retrieve movies associated with a specific distributor.

### Movie

- **GET** `/api/Movie`
  - Retrieve all movies.

![Post-Request](https://github.com/YuhanPizza/MovieReviewAPI/assets/107896556/b29eca53-eefc-490a-8f38-a7d889478b35)
- **POST** `/api/Movie`
  - Create a new movie.

- **GET** `/api/Movie/{movieId}`
  - Retrieve details of a specific movie.
    
- **PUT** `/api/Movie/{movieId}`
  - Update a specific movie.

- **DELETE** `/api/Movie/{movieId}`
  - Delete a specific movie.

- **GET** `/api/Movie/{movieId}/rating`
  - Retrieve reviews and ratings for a specific movie.

### Review

- **GET** `/api/Review`
  - Retrieve all reviews.

- **POST** `/api/Review`
  - Create a new review.

- **GET** `/api/Review/{reviewId}`
  - Retrieve details of a specific review.

![UpdateById-Request](https://github.com/YuhanPizza/MovieReviewAPI/assets/107896556/b664a337-258b-4e03-ad51-df7f99954c4c)
- **PUT** `/api/Review/{reviewId}`
  - Update a specific review.

- **DELETE** `/api/Review/{reviewId}`
  - Delete a specific review.

- **GET** `/api/Review/movie/{movieId}`
  - Retrieve reviews for a specific movie.

### Reviewer

- **GET** `/api/Reviewer`
  - Retrieve all reviewers.

- **POST** `/api/Reviewer`
  - Create a new reviewer.

- **GET** `/api/Reviewer/{reviewerId}`
  - Retrieve details of a specific reviewer.

- **PUT** `/api/Reviewer/{reviewerId}`
  - Update a specific reviewer.

![DeleteById](https://github.com/YuhanPizza/MovieReviewAPI/assets/107896556/0306ed7e-807b-48b5-b9d5-8747e00f34a1)
- **DELETE** `/api/Reviewer/{reviewerId}`
  - Delete a specific reviewer.

- **GET** `/api/Reviewer/{reviewerId}/reviews`
  - Retrieve reviews submitted by a specific reviewer.

## Meet the Controllers 🎮

1. **CategoryController.cs**
   - Takes care of everything related to movie categories.

2. **CountryController.cs**
   - Handles operations concerning countries.

3. **DistributorController.cs**
   - The go-to place for distributor-related operations.

4. **MovieController.cs**
   - Your go-to for all things movie-related.

5. **ReviewController.cs**
   - The maestro behind movie review operations.

6. **ReviewerController.cs**
   - Manages operations related to reviewers.

## Behind the Scenes: Data Management 🕹️

1. **DataContext.cs**
   - This is the database maestro, juggling all the data.
   - Houses DbSet properties for categories, countries, distributors, movies, movie categories, reviews, and reviewers.
   - Defines some fancy relationships between entities in the `OnModelCreating` method.

## DTO (Data Transfer Objects) 📦
   - These are like the messengers, ensuring smooth communication between entities and the outside world.

1. **CategoryDto.cs**
2. **CountryDto.cs**
3. **DistributorDto.cs**
4. **MovieDto.cs**
5. **ReviewDto.cs**
6. **ReviewerDto.cs**

## The Little Helper: MappingProfiles.cs 🗺️

- Uses AutoMapper to create maps between entities and DTOs. It's like the translator making sure everyone speaks the same language.

## Interfaces: The Contract Keepers 📜

1. **ICategoryRepository.cs**
2. **ICountryRepository.cs**
3. **IDistributorRepository.cs**
4. **IMoviesRepository.cs**
5. **IReviewRepository.cs**
6. **IReviewerRepository.cs**

   - These are the rulebooks for data access. Everyone follows their lead.

## The Models: The Cool Kids in Town 🕶️

   - These are the main characters, representing what's happening behind the scenes in the database.

1. **Category.cs**
2. **Country.cs**
3. **Distributor.cs**
4. **Movie.cs**
5. **MovieCategory.cs**
6. **Review.cs**
7. **Reviewer.cs**

## The Squad: Repository Classes 🛠️

   - The task force implementing all the data access strategies.

1. **CategoryRepository.cs**
2. **CountryRepository.cs**
3. **DistributorRepository.cs**
4. **MoviesRepository.cs**
5. **ReviewRepository.cs**
6. **ReviewerRepository.cs**


## Getting Things Started: Dependency Injection in Program.cs ⚙️

- Configures all the services like controllers, repositories, AutoMapper, and DbContext.
- Sets up Swagger to make sure everyone knows what's going on.
- Defines where the database lives using Entity Framework.

## Bringing Life to the Party: Seed Data 🌱

Want to kickstart the party with some seed data? Just run the application with the "seeddata" argument, and you're good to go!

## The Cool Middleware 🌐

- Configures Swagger for some snazzy API documentation when you're in development mode.


Feel free to customize and add your own flavor to the API. It's here to make your movie review adventures even more awesome! 🍿🎬🚀

### Unit Testing 
I use xUnit, FakeItEasy and Fluent Assertions.

- **[xUnit](https://xunit.net/):** A testing framework that keeps things simple and snappy. 🚦

- **[FakeItEasy](https://fakeiteasy.github.io/):** Our backstage pass to mocking – perfect for faking it 'til you make it! 🎭

- **[Fluent Assertions](https://fluentassertions.com/introduction):** Making assertions a breeze with a touch of flair. Fluent and fabulous! 💬

## Controllers

**MovieController:** [Passed!]!

- GetMovies() Passed!
- CreateMovies() Passed!
- GetMovie() Passed!
- GetMovieRaiting() Passed!
- UpdateMovie() Passed!
- DeleteMovie() Passed!

**CategoryController:** [Passed!]!

- GetCategories() Passed!
- GetCategory() Passed!
- GetMovieByCategoryId() Passed!
- CreateCategory() Passed!
- UpdateCategory() Passed!
- DeleteCategory() Passed!

**CountryController:** [Passed]!

- GetCountries() Passed!
- GetCountry() Passed!
- GetCountryOfAnDistributer() Passed!
- CreateCountry() Passed!
- UpdateCountry() Passed!
- DeleteCountry() Passed!

**DistributerController:** [TBD]!

- GetDistributers() 
- GetDistributer()
- GetMovieByDistributerId()
- CreateDistributer() 
- UpdateDistributer() 
- DeleteDistributer()

**ReviewController:** [TBD]!

- GetReviews() 
- GetReview()
- GetReviewsForAMovie()
- CreateReview() 
- UpdateReview() 
- DeleteReview()

**ReviewerController:** [TBD]!

- GetReviewers() 
- GetReviewer()
- GetReviewsByAReviewer()
- CreateReviewer() 
- UpdateReviewer() 
- DeleteReviewer()

  
