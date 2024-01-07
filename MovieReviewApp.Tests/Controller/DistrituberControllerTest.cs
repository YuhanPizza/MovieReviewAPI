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
	public class DistrituberControllerTest
	{
		private readonly IDistributerRepository _distributerRepository;
		private readonly ICountryRepository _countryRepository;
		private readonly IMapper _mapper;

        public DistrituberControllerTest()
        {
            _distributerRepository = A.Fake<IDistributerRepository>();
			_countryRepository = A.Fake<ICountryRepository>();
			_mapper = A.Fake<IMapper>();
        }

		//Tests
		[Fact]
		public void DistributerController_GetDistributers_ReturnOk()
		{
			//Arrange
			var distributerCall = A.Fake<ICollection<Distributer>>();
			var distributerDtos = A.Fake<List<DistributerDto>>();
			A.CallTo(() => _distributerRepository.GetDistributers()).Returns(distributerCall);
			A.CallTo(() => _mapper.Map<List<DistributerDto>>(distributerCall)).Returns(distributerDtos);
			var controller = new DistributerController(_distributerRepository, _countryRepository, _mapper);

			//Act
			var result = controller.GetDistributers();

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));

		}

		[Fact]
		public void DistributerController_GetDistributer_ReturnOk()
		{
			//Arrange 
			int distributerId = 1;
			var distributerDto = A.Fake<DistributerDto>();
			//var distributer = A.Fake<Distributer>();
			A.CallTo(() => _distributerRepository.DistributerExists(distributerId)).Returns(true);
			A.CallTo(() => _mapper.Map<DistributerDto>(_distributerRepository.GetDistributer(distributerId))).Returns(distributerDto);
			//or 2 seperate calls
			//A.CallTo(() => _distributerRepository.GetDistributer(distributerId)).Returns(distributer);
			//A.CallTo(() => _mapper.Map<DistributerDto>(distributer)).Returns(distributerDto);
			var controller = new DistributerController(_distributerRepository, _countryRepository, _mapper);

			//Act
			var result = controller.GetDistributer(distributerId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void DistributerController_GetMovieByDistributerId_ReturnOk()
		{
			//Arrange
			int distributerId = 1;
			var movieDto = A.Fake<List<MovieDto>>();
			A.CallTo(() => _distributerRepository.DistributerExists(distributerId)).Returns(true);
			A.CallTo(() => _mapper.Map<List<MovieDto>>(_distributerRepository.GetMovieByDistributer(distributerId))).Returns(movieDto);
			var controller = new DistributerController(_distributerRepository, _countryRepository, _mapper);

			//Act
			var result = controller.GetMovieByDistributerId(distributerId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void DistributerController_CreateDistributer_ReturnOk()
		{
			//Arrange
			int countryId = 1;
			var distributerCreate = A.Fake<DistributerDto>();
			var distributer = A.Fake<Distributer>();
			var country = A.Fake<Country>();
			A.CallTo(() => _distributerRepository.GetDistributersTrimToUpper(distributerCreate)).Returns(null);
			A.CallTo(() => _mapper.Map<Distributer>(distributerCreate)).Returns(distributer);
			A.CallTo(() => _countryRepository.GetCountry(countryId)).Returns(country);
			A.CallTo(() => _distributerRepository.CreateDistributer(distributer)).Returns(true);
			var controller = new DistributerController(_distributerRepository, _countryRepository, _mapper);

			//Act
			var result = controller.CreateDistributer(countryId, distributerCreate);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void DistributerController_UpdateDistributer_ReturnOk()
		{
			//arrange
			int distributerId = 1;
			var distributerUpdate = A.Fake<DistributerDto>();
			var distributer = A.Fake<Distributer>();
			distributerUpdate.Id = distributerId; //so they match 
			A.CallTo(() => _distributerRepository.DistributerExists(distributerId)).Returns(true);
			A.CallTo(() => _mapper.Map<Distributer>(distributerUpdate)).Returns(distributer);
			A.CallTo(() => _distributerRepository.UpdateDistributer(distributer)).Returns(true);
			var controller = new DistributerController(_distributerRepository, _countryRepository, _mapper);

			//act
			var result = controller.UpdateDistributer(distributerId, distributerUpdate);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void DistributerController_DeleteDistributer_ReturnOk()
		{
			//Arrange
			int distributerId = 1;
			var distributerDelete = A.Fake<Distributer>();
			A.CallTo(() => _distributerRepository.DistributerExists(distributerId)).Returns(true);
			A.CallTo(() => _distributerRepository.GetDistributer(distributerId)).Returns(distributerDelete);
			A.CallTo(() => _distributerRepository.DeleteDistributer(distributerDelete)).Returns(true);
			var controller = new DistributerController(_distributerRepository, _countryRepository, _mapper);

			//Act
			var result = controller.DeleteDistributer(distributerId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}
    }
}
