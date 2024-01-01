using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Data;
using MovieReviewApp.Dto;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Repository;

namespace MovieReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewerController: Controller
	{
		private readonly IReviewerRepository _reviewerRepository;
		private readonly IReviewRepository _reviewRepository;
		private readonly IMapper _mapper;
		private readonly DataContext _context;

		public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper,DataContext context, IReviewRepository reviewRepository)
        {
			_reviewerRepository = reviewerRepository;
			_reviewRepository = reviewRepository;
			_mapper = mapper;
			_context = context;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
		public IActionResult GetReviewers()
		{
			var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(reviewers);
		}
		[HttpGet("{reviewerId}")]
		[ProducesResponseType(200, Type = typeof(Reviewer))]
		[ProducesResponseType(400)]
		public IActionResult GetReviewer(int reviewerId)
		{
			if (!_reviewerRepository.ReviewerExists(reviewerId))
			{
				return NotFound();
			}
			var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(reviewerId));
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(reviewer);
		}
		[HttpGet("{reviewerId}/reviews")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
		[ProducesResponseType(400)]
		public IActionResult GetReviewsByAReviewer(int reviewerId)
		{
			if (!_reviewerRepository.ReviewerExists(reviewerId))
			{
				return NotFound();
			}
			var reviews = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewsByReviewer(reviewerId));
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(reviews);
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerCreate)
		{
			if (reviewerCreate == null)
			{
				return BadRequest(ModelState);
			}
			var reviewer = _reviewerRepository.GetReviewers()
				.Where(c => c.FirstName.Trim().ToUpper() == reviewerCreate.FirstName.TrimEnd().ToUpper())
				.FirstOrDefault();

			//Error Handling
			if (reviewer != null)
			{
				ModelState.AddModelError("", "Movie Already Exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);

			if (!_reviewerRepository.CreateReviewer(reviewerMap))
			{
				ModelState.AddModelError("", "Something went wrong during save");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully created!");
		}
		[HttpPut("{reviewerId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public IActionResult UpdateReviewer(int reviewerId, [FromBody] ReviewerDto updatedReviewer)
		{
			if (updatedReviewer == null)
			{
				return BadRequest(ModelState);
			}

			if (reviewerId != updatedReviewer.Id)
			{
				return BadRequest(ModelState);
			}

			if (!_reviewerRepository.ReviewerExists(reviewerId))
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var reviewerMap = _mapper.Map<Reviewer>(updatedReviewer);

			if (!_reviewerRepository.UpdateReviewer(reviewerMap))
			{
				ModelState.AddModelError("", "Something went wrong during Update[Reviewer]");
				return StatusCode(500, ModelState);
			}

			return Ok("Reviewer Successfully Updated!");
		}
		[HttpDelete("{reviewerId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public IActionResult DeleteReviewer(int reviewerId)
		{
			if (!_reviewerRepository.ReviewerExists(reviewerId))
			{
				return NotFound();
			}

			var reviewsDelete = _reviewerRepository.GetReviewsByReviewer(reviewerId);
			var reviewerDelete = _reviewerRepository.GetReviewer(reviewerId);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (!_reviewRepository.DeleteReviews(reviewsDelete.ToList()))//delete range method
			{
				ModelState.AddModelError("", "Error removing Reviews!");
			}

			if (!_reviewerRepository.DeleteReviewer(reviewerDelete))
			{
				ModelState.AddModelError("", "Something went wrong Removing Reviewer");
			}

			return Ok("Reviewer Sucessfully Removed!");
		}
	}
}
