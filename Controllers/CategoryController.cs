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
	public class CategoryController : Controller
	{
        private readonly ICategoryRepository _categoryRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, DataContext context, IMapper mapper)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
		public IActionResult GetCategories()
		{
			var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(categories);
		}
		[HttpGet("{categoryId}")]
		[ProducesResponseType(200, Type = typeof(Category))]
		[ProducesResponseType(400)]
		public IActionResult GetCategory(int categoryId)
		{
			if (!_categoryRepository.CategoryExists(categoryId))
			{
				return NotFound();
			}
			var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryId));
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(category);
		}
		[HttpGet("movie/{categoryId}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
		[ProducesResponseType(400)]
		public IActionResult GetMovieByCategoryId(int categoryId)
		{
			var movies = _mapper.Map<List<MovieDto>>(_categoryRepository.GetMovieByCategory(categoryId));
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(movies);
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
		{
			if(categoryCreate == null)
			{
				return BadRequest(ModelState);
			}
			var category = _categoryRepository.GetCategories()
				.Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper())
				.FirstOrDefault();

			//Error Handling
			if (category != null) 
			{
				ModelState.AddModelError("", "Category Already Exists");
				return StatusCode(422, ModelState);
			}
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var categoryMap = _mapper.Map<Category>(categoryCreate);

			if (!_categoryRepository.CreateCategory(categoryMap))
			{
				ModelState.AddModelError("", "Something went wrong during save");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully Created!");
		}
	}
}
