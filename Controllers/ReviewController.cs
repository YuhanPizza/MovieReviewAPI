﻿using AutoMapper;
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
	public class ReviewController :Controller
	{
		private IReviewRepository _reviewRepository;
		private IMapper _mapper;
		private DataContext _context;

        public ReviewController(IReviewRepository reviewRepository,IMapper mapper,IMoviesRepository moviesRepository,  DataContext context)
        {
			_reviewRepository = reviewRepository;
			_mapper = mapper;
			_context = context;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
		public IActionResult GetReviews()
		{
			var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(reviews);
		}
		[HttpGet("{reviewId}")]
		[ProducesResponseType(200, Type = typeof(Review))]
		[ProducesResponseType(400)]
		public IActionResult GetReview(int reviewId)
		{
			if (!_reviewRepository.ReviewExists(reviewId))
			{
				return NotFound();
			}
			var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(review);
		}
		[HttpGet("movie/{movieId}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
		[ProducesResponseType(400)]
		public IActionResult GetReviewsForAMovie(int movieId)
		{
			var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfAMovie(movieId));
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(reviews);
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int movieId,[FromBody] ReviewDto reviewCreate)
		{
			if (reviewCreate == null)
			{
				return BadRequest(ModelState);
			}
			var review = _reviewRepository.GetReviews()
				.Where(r => r.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper()) //check for duplicates
				.FirstOrDefault();

			//Error Handling
			if (review != null)
			{
				ModelState.AddModelError("", "Country Already Exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var reviewMap = _mapper.Map<Review>(reviewCreate);

			if (!_reviewRepository.CreateReview(reviewerId, movieId, reviewMap))
			{
				ModelState.AddModelError("", "Something went wrong during save");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully created!");
		}
	}
}
