using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Controllers;
using MovieReviewApp.Dto;
using MovieReviewApp.Interfaces;
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
    }
}
