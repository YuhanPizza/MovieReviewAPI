﻿using MovieReviewApp.Data;
using MovieReviewApp.Dto;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;

namespace MovieReviewApp.Repository
{
	public class ReviewRepository : IReviewRepository
	{
		private readonly DataContext _context;

		public ReviewRepository(DataContext context)
        {
			_context = context;
		}

		public bool CreateReview(int reviwerId, int movieId, Review review)
		{
			review.Movie = _context.Movies.Where(m => m.Id == movieId).FirstOrDefault(); 
			review.Reviewer = _context.Reviewers.Where(r => r.Id == reviwerId).FirstOrDefault();
			_context.Add(review);
			return Save();
		}

		public bool DeleteReview(Review review)
		{
			_context.Remove(review);
			return Save();
		}

		public bool DeleteReviews(List<Review> reviews)
		{
			_context.RemoveRange(reviews);
			return Save();
		}

		public Review GetReview(int id)
		{
			return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
		}

		public ICollection<Review> GetReviews()
		{
			return _context.Reviews.ToList();
		}

		public ICollection<Review> GetReviewsOfAMovie(int movieId)
		{
			return _context.Reviews.Where(r => r.Movie.Id == movieId).ToList();
		}

		public Review GetReviewsTrimToUpper(ReviewDto reviewDto)
		{
			return GetReviews().Where(r => r.Title.Trim().ToUpper() == reviewDto.Title.TrimEnd().ToUpper()) //check for duplicates
				.FirstOrDefault();
		}

		public bool ReviewExists(int reviewId)
		{
			return _context.Reviews.Any(r => r.Id == reviewId);
		}

		public bool Save()
		{
			var result = _context.SaveChanges();
			return result > 0 ? true : false;
		}

		public bool UpdateReview(Review review)
		{
			_context.Update(review);
			return Save();
		}
	}
}
