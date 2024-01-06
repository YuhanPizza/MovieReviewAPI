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
	public class CountryControllerTests
	{
		private readonly ICountryRepository _countryRepository;
		private readonly IMapper _mapper;

        //setup controller constructor
        public CountryControllerTests()
        {
            _countryRepository = A.Fake<ICountryRepository>();
            _mapper = A.Fake<IMapper>();
        }

        //tests
        [Fact]
        public void CountryController_GetCountries_ReturnOk()
        {
            //Arrange
            var countriesDto = A.Fake<List<CountryDto>>();
            var countries = A.Fake <ICollection<Country>>();
            A.CallTo(() => _countryRepository.GetCountries()).Returns(countries);
            A.CallTo(() => _mapper.Map<List<CountryDto>>(countries)).Returns(countriesDto);
            //or 
            //A.CallTo(() => _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries()).Returns(countriesDto); 
            var controller = new CountryController(_countryRepository,_mapper);

            //Act
            var result = controller.GetCountries();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void CountryController_GetCountry_ReturnOk()
        {
            //Arrange
            int countryId = 1; 
            var countryDto = A.Fake<CountryDto>();
            //var country = A.Fake<Country>();
            A.CallTo(()=> _countryRepository.CountryExists(countryId)).Returns(true);
            A.CallTo(()=> _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId))).Returns(countryDto);
            //or
            //A.CallTo(() => _countryRepository.GetCountry(countryId)).Returns(country);
            //A.CallTo(() => _mapper.Map<CountryDto>(country)).Returns(countryDto);
            var controller = new CountryController(_countryRepository, _mapper);

            //Act
            var result = controller.GetCountry(countryId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void CountryController_GetCountryOfAnDistributer_ReturnOk()
        {
            //Arrange
            int distributerId = 1;
            //var country = A.Fake<Country>();
            var countryDto = A.Fake<CountryDto>();
            //A.CallTo(() => _countryRepository.GetCountryByDistributer(distributerId)).Returns(country);
            A.CallTo(() => _mapper.Map<CountryDto>(_countryRepository.GetCountryByDistributer(distributerId))).Returns(countryDto);
            var controller = new CountryController(_countryRepository, _mapper);

            //Act
            var result = controller.GetCountryOfAnDistributer(distributerId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

        [Fact]
        public void CountryController_CreateCountry_ReturnOk()
        {
            //Arrange
            var countryCreate = A.Fake<CountryDto>();
            var country = A.Fake<Country>();
            A.CallTo(() => _countryRepository.GetCountriesTrimToUpper(countryCreate)).Returns(null);
            A.CallTo(() => _mapper.Map<Country>(countryCreate)).Returns(country);
            A.CallTo(() => _countryRepository.CreateCountry(country)).Returns(true);
            var controller = new CountryController(_countryRepository, _mapper);

            //Act
            var result = controller.CreateCountry(countryCreate);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

        [Fact]
        public void CountryController_UpdateCountry_ReturnOk()
        {
            //Arrange
            int countryId = 1;
            var countryUpdate = A.Fake<CountryDto>();
            var country = A.Fake<Country>();
            countryUpdate.Id = countryId;
            A.CallTo(() => _countryRepository.CountryExists(countryId)).Returns(true);
            A.CallTo(() => _mapper.Map<Country>(countryUpdate)).Returns(country);
            A.CallTo(() => _countryRepository.UpdateCountry(country)).Returns(true);
            var controller = new CountryController (_countryRepository, _mapper);

            //Act
            var result = controller.UpdateCountry(countryId, countryUpdate);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

        [Fact]
        public void CountryController_DeleteCountry_ReturnOk()
        {
            //Arrange
            int countryId = 1;
            var country = A.Fake<Country>();
            A.CallTo(() => _countryRepository.CountryExists(countryId)).Returns(true);
            A.CallTo(() => _countryRepository.GetCountry(countryId)).Returns(country);
            A.CallTo(() => _countryRepository.DeleteCountry(country)).Returns(true);
            var controller = new CountryController(_countryRepository, _mapper);

            //Act
            var result = controller.DeleteCountry(countryId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}
    }
}
