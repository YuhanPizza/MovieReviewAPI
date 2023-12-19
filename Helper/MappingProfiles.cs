using AutoMapper;
using MovieReviewApp.Dto;
using MovieReviewApp.Models;

namespace MovieReviewApp.Helper
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Movie, MovieDto>();
			CreateMap<MovieDto, Movie>();
			CreateMap<Category, CategoryDto>();
			CreateMap<CategoryDto, Category>();
			CreateMap<Country, CountryDto>();
			CreateMap<CountryDto, Country>();
			CreateMap<Distributer,DistributerDto>();
			CreateMap<DistributerDto, Distributer>();
			CreateMap<Review, ReviewDto>();
			CreateMap<ReviewDto, Review>();
			CreateMap<Reviewer, ReviewerDto>();
			CreateMap<ReviewerDto, Reviewer>();
		}
    }
}
