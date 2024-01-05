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
	public class CategoryControllerTest
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;
		public CategoryControllerTest()
		{
			_categoryRepository = A.Fake<ICategoryRepository>();
			_mapper = A.Fake<IMapper>();
		}

		//Tests
		[Fact]
		public void CategoryController_GetCategories_ReturnOk()
		{
			//Arrange
			var category = A.Fake<ICollection<CategoryDto>>();
			var categoriesList = A.Fake<List<CategoryDto>>();
			A.CallTo(() => _mapper.Map<List<CategoryDto>>(category)).Returns(categoriesList);
			var controller = new CategoryController(_categoryRepository, _mapper);


			//Act
			var result = controller.GetCategories();

			//Assert
			result.Should().NotBeNull(); //not null
			result.Should().BeOfType(typeof(OkObjectResult)); //okresult
		}

		[Fact]
		public void CategoryController_GetCategory_ReturnOk()
		{
			//Arrange
			int categoryId = 1;
			var categoryDto = A.Fake<CategoryDto>();
			A.CallTo(() => _categoryRepository.CategoryExists(categoryId)).Returns(true);
			A.CallTo(() => _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryId))).Returns(categoryDto);
			var controller = new CategoryController(_categoryRepository, _mapper);

			//Act
			var result = controller.GetCategory(categoryId);

			//Assert
			result.Should().NotBeNull(); //not null
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void CategoryController_GetMovieByCategoryId_ReturnOk()
		{
			//Arrange
			int categoryId = 1;
			var movieDtoList = A.Fake<List<MovieDto>>();
			//var movies = A.Fake<ICollection<Movie>>(); //check return type of function mocked
			A.CallTo(() => _mapper.Map<List<MovieDto>>(_categoryRepository.GetMovieByCategory(categoryId))).Returns(movieDtoList);
			//could also be written like this remember to implement<Fake> the variables to be used
			//A.CallTo(()=> _mapper.Map<List<MovieDto>>(movies)).Returns(moviesList);
			//A.CallTo(()=> _categoryRepository.GetMovieByCategory(categoryId)).Returns(movies);
			var controller = new CategoryController(_categoryRepository, _mapper); //implementation of controller

			//Act
			var result = controller.GetMovieByCategoryId(categoryId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}
		
		[Fact]
		public void CategoryController_CreateCategory_ReturnOk()
		{
			//Arrange
			var categoryCreate = A.Fake<CategoryDto>();
			var category = A.Fake<Category>();
			//returns null to say category does not exist in repository yet
			A.CallTo(() => _categoryRepository.GetCategoriesTrimToUpper(categoryCreate)).Returns(null);
			A.CallTo(()=> _mapper.Map<Category>(categoryCreate)).Returns(category);
			A.CallTo(() => _categoryRepository.CreateCategory(category)).Returns(true);
			var controller = new CategoryController(_categoryRepository, _mapper);

			//Act
			var result = controller.CreateCategory(categoryCreate);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void CategoryController_UpdateCategory_ReturnOk()
		{
			//Arrange
			var updatedCategory = A.Fake<CategoryDto>();
			var category = A.Fake<Category>();
			int categoryId = 1;
			updatedCategory.Id = categoryId;
			A.CallTo(()=> _categoryRepository.CategoryExists(categoryId)).Returns(true);
			A.CallTo(()=> _mapper.Map<Category>(updatedCategory)).Returns(category);
			A.CallTo(() => _categoryRepository.UpdateCategory(category)).Returns(true);
			var controller = new CategoryController(_categoryRepository,_mapper);

			//Act
			var result = controller.UpdateCategory(categoryId, updatedCategory);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void CategoryController_DeleteCategory_ReturnOk()
		{
			//Arrange
			int categoryId = 1;
			var categoryDelete = A.Fake<Category>();
			A.CallTo(() => _categoryRepository.CategoryExists(categoryId)).Returns(true);
			A.CallTo(()=> _categoryRepository.GetCategory(categoryId)).Returns(categoryDelete);
			A.CallTo(() => _categoryRepository.DeleteCategory(categoryDelete)).Returns(true);
			var controller = new CategoryController(_categoryRepository, _mapper);

			//Act
			var result = controller.DeleteCategory(categoryId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}
	}
}
