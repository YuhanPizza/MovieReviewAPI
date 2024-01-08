using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Controllers;
using MovieReviewApp.Dto;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieReviewApp.Tests.Controller
{
	public class ReviewControllerTests
	{
		private readonly IReviewRepository _reviewRepository;
		private readonly IMapper _mapper;

        public ReviewControllerTests()
        {
            _reviewRepository = A.Fake<IReviewRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void ReviewController_GetReviews_ReturnOk()
        {
            //Arrange
            var reviewsDto = A.Fake<List<ReviewDto>>();
            A.CallTo(() => _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews())).Returns(reviewsDto);
            //A.CallTo(() => _reviewRepository.GetReviews()).Returns(reviews);
            //A.CallTo(() => _mapper.Map<List<ReviewDto>(reviews)).Return(reviewsDto);
            var controller = new ReviewController(_reviewRepository, _mapper);

            //Act
            var result = controller.GetReviews();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ReviewController_GetReview_ReturnOk()
        {
            //Arrange
            int reviewId = 1;
            var reviewDto = A.Fake<ReviewDto>();
            A.CallTo(() => _reviewRepository.ReviewExists(reviewId)).Returns(true);
            A.CallTo(() => _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId))).Returns(reviewDto);
            var controller = new ReviewController (_reviewRepository, _mapper);

            //Act
            var result = controller.GetReview(reviewId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

        [Fact]
        public void ReviewController_GetReviewsForAMovie_ReturnOk()
        {
            //Arrange
            int movieId = 1;
            var reviewsDto = A.Fake<List<ReviewDto>>();
            A.CallTo(() => _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfAMovie(movieId))).Returns(reviewsDto);
            var controller = new ReviewController(_reviewRepository, _mapper);

            //Act
            var result = controller.GetReviewsForAMovie(movieId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

        [Fact]
        public void ReviewController_CreateReview_ReturnOk()
        {
            //Arrange
            int reviewerId = 1;
            int movieId = 1;
            var reviewCreate = A.Fake<ReviewDto>();
            var review = A.Fake<Review>();
            A.CallTo(() => _reviewRepository.GetReviewsTrimToUpper(reviewCreate)).Returns(null);
            A.CallTo(() => _mapper.Map<Review>(reviewCreate)).Returns(review);
            A.CallTo(() => _reviewRepository.CreateReview(reviewerId, movieId, review)).Returns(true);
            var controller = new ReviewController(_reviewRepository, _mapper);

            //Act
            var result = controller.CreateReview(reviewerId, movieId, reviewCreate);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

        [Fact]
        public void ReviewController_UpdateReview_ReturnOk()
        {
            //Arrange
            int reviewId = 1;
            var reviewUpdate = A.Fake<ReviewDto>();
            reviewUpdate.Id = reviewId;
            var review = A.Fake<Review>();
            A.CallTo(() => _reviewRepository.ReviewExists(reviewId)).Returns(true);
            A.CallTo(() => _mapper.Map<Review>(reviewUpdate)).Returns(review);
            A.CallTo(() => _reviewRepository.UpdateReview(review)).Returns(true);
            var controller = new ReviewController(_reviewRepository, _mapper);

            //Act
            var result = controller.UpdateReview(reviewId, reviewUpdate);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

        [Fact]
        public void ReviewController_DeleteReview_ReturnOk()
        {
            //Arrange
            int reviewId = 1;
            var reviewDelete = A.Fake<Review>();
            A.CallTo(() => _reviewRepository.ReviewExists(reviewId)).Returns(true);
            A.CallTo(() => _reviewRepository.GetReview(reviewId)).Returns(reviewDelete);
            A.CallTo(() => _reviewRepository.DeleteReview(reviewDelete)).Returns(true);
            var controller = new ReviewController(_reviewRepository, _mapper);

            //Act
            var result = controller.DeleteReview(reviewId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
