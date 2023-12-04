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
		private readonly IMapper _mapper;
		private readonly DataContext _context;

		public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper,DataContext context)
        {
			_reviewerRepository = reviewerRepository;
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
	}
}
