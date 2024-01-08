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
	public class ReviewerControllerTests
	{
		private readonly IReviewerRepository _reviewerRepository;
		private readonly IReviewRepository _reviewRepository;
		private readonly IMapper _mapper;

        public ReviewerControllerTests()
        {
            _reviewerRepository = A.Fake<IReviewerRepository>();
			_reviewRepository = A.Fake<IReviewRepository>();
			_mapper = A.Fake<IMapper>();
        }

		//tests
		[Fact]
		public void ReviewerController_GetReviewers_ReturnOk()
		{
			//Arrange
			var reviewers = A.Fake<List<ReviewerDto>>();
			A.CallTo(() => _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers())).Returns(reviewers);
			var controller = new ReviewerController(_reviewerRepository, _mapper, _reviewRepository);

			//Act
			var result = controller.GetReviewers();

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void ReviewerController_GetReviewer_ReturnOk()
		{
			//Arrange
			int reviewerId = 1;
			var reviewerDto = A.Fake<ReviewerDto>();
			A.CallTo(() => _reviewerRepository.ReviewerExists(reviewerId)).Returns(true);
			A.CallTo(() => _mapper.Map<ReviewerDto>(_reviewRepository.GetReview(reviewerId))).Returns(reviewerDto);
			var controller = new ReviewerController(_reviewerRepository, _mapper, _reviewRepository);

			//Act
			var result = controller.GetReviewer(reviewerId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void ReviewerController_GetReviewsByAReviewer_ReturnOk()
		{
			//Arrange
			int reviewerId = 1;
			var reviewDto = A.Fake<List<ReviewDto>>();
			A.CallTo(() => _reviewerRepository.ReviewerExists(reviewerId)).Returns(true);
			A.CallTo(() => _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewsByReviewer(reviewerId))).Returns(reviewDto);
			var controller = new ReviewerController(_reviewerRepository, _mapper, _reviewRepository);

			//Act
			var result = controller.GetReviewsByAReviewer(reviewerId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void ReviewerController_CreateReviewer_ReturnOk()
		{
			//Arrange
			var reviewerCreate = A.Fake<ReviewerDto>();
			var reviewer = A.Fake<Reviewer>();
			A.CallTo(() => _reviewerRepository.GetReviewersTrimToUpper(reviewerCreate)).Returns(null);
			A.CallTo(() => _mapper.Map<Reviewer>(reviewerCreate)).Returns(reviewer);
			A.CallTo(() => _reviewerRepository.CreateReviewer(reviewer)).Returns(true);
			var controller = new ReviewerController(_reviewerRepository, _mapper, _reviewRepository);

			//Act
			var result = controller.CreateReviewer(reviewerCreate);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void ReviewerController_UpdateReviewer_ReturnOk()
		{
			//Arrange
			int reviewerId = 1;
			var reviewerUpdate = A.Fake<ReviewerDto>();
			reviewerUpdate.Id = reviewerId;
			var reviewer = A.Fake<Reviewer>();
			A.CallTo(() => _reviewerRepository.ReviewerExists(reviewerId)).Returns(true);
			A.CallTo(() => _mapper.Map<Reviewer>(reviewerUpdate)).Returns(reviewer);
			A.CallTo(() => _reviewerRepository.UpdateReviewer(reviewer)).Returns(true);
			var controller = new ReviewerController(_reviewerRepository, _mapper, _reviewRepository);

			//Act
			var result = controller.UpdateReviewer(reviewerId, reviewerUpdate);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void ReviewerController_DeleteReviewer_ReturnOk()
		{
			//Arrange
			int reviewerId = 1;
			var reviewsDelete = A.Fake<List<Review>>();
			var reviewerDelete = A.Fake<Reviewer>();
			A.CallTo(() => _reviewerRepository.ReviewerExists(reviewerId)).Returns(true);
			A.CallTo(() => _reviewerRepository.GetReviewsByReviewer(reviewerId)).Returns(reviewsDelete);
			A.CallTo(() => _reviewerRepository.GetReviewer(reviewerId)).Returns(reviewerDelete);
			A.CallTo(() => _reviewRepository.DeleteReviews(reviewsDelete.ToList())).Returns(true);
			A.CallTo(() => _reviewerRepository.DeleteReviewer(reviewerDelete)).Returns(true);
			var controller = new ReviewerController(_reviewerRepository, _mapper, _reviewRepository);

			//Act
			var result = controller.DeleteReviewer(reviewerId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}
    }
}
