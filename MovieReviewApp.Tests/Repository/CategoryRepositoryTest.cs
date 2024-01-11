using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Data;
using MovieReviewApp.Models;
using MovieReviewApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieReviewApp.Tests.Repository
{
	public class CategoryRepositoryTest
	{
		private async Task<DataContext> GetDatabaseContext()
		{
			var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			var databaseContext = new DataContext(options);
			databaseContext.Database.EnsureCreated();
			if( await databaseContext.Categories.CountAsync() <=0)
			{
				for(int i = 0; i < 10 ; i++)
				{
					databaseContext.Categories.Add(
						new Category()
						{
							Name = "Action"
						});
					await databaseContext.SaveChangesAsync();
				}
			}
			return databaseContext;
		}
		[Fact]
		public async void CategoryRepository_CategoryExists_ReturnTrue()
		{
			//Arrange
			int id = 1;
			var dbContext = await GetDatabaseContext();
			var categoryRepository = new CategoryRepository(dbContext);

			//Act
			var result = categoryRepository.CategoryExists(id);

			//Assert
			result.Should().NotBe(false);
			result.Should().Be(true);
		}
		[Fact]
		public async void CategoryRepository_CreateCategory_ReturnTrue()
		{
			//Arrange
			var categoryAdd = new Category()
			{
				Name = "Thriller"
			};
			var dbContext = await GetDatabaseContext();
			var categoryRepository = new CategoryRepository(dbContext);

			//Act
			var result = categoryRepository.CreateCategory(categoryAdd);

			//Assert
			result.Should().NotBe(false);
			result.Should().Be(true);
		}

		[Fact]
		public async void CategoryRepository_GetCategories_ReturnICollectionCategories()
		{
			//Arrange
			var dbContext = await GetDatabaseContext();
			var categoryRepository = new CategoryRepository(dbContext);

			//Act
			var result = categoryRepository.GetCategories();

			//Assert
			result.Should().NotBeEmpty();
			result.Should().HaveCount(10);
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(List<Category>));
		}

		[Fact]
		public async void CategoryRepository_GetCategory_ReturnCategory()
		{
			//Arrange
			int id = 1;
			var dbContext = await GetDatabaseContext();
			var categoryRepository = new CategoryRepository(dbContext);

			//ACT
			var result = categoryRepository.GetCategory(id);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(Category));
		}
	}
}
