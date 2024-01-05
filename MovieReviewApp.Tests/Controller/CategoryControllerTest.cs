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
			A.CallTo(()=>  _categoryRepository.CategoryExists(categoryId)).Returns(true);
			A.CallTo(()=> _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryId))).Returns(categoryDto);
			var controller = new CategoryController(_categoryRepository, _mapper);

			//Act
			var result = controller.GetCategory(categoryId);

			//Assert
			result.Should().NotBeNull(); //not null
			result.Should().BeOfType(typeof(OkObjectResult));
		}
	}
}
