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
    }
}
