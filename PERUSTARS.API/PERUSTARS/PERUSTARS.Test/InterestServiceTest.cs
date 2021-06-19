using FluentAssertions;
using Moq;
using NUnit.Framework;
using PERUSTARS.Domain.Models;
using PERUSTARS.Domain.Persistence.Repositories;
using PERUSTARS.Domain.Services.Communications;
using PERUSTARS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PERUSTARS.Test
{
    class InterestServiceTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task AssignInterestWhenValidInterestReturnsInterestResponse()
        {
            //Arrange
            var mockInterestRepository = GetDefaultIInterestRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var interest = new Interest { HobbyistId = 1, SpecialtyId = 1 };
            var hobbyistId = interest.HobbyistId;
            var specialtyId = interest.SpecialtyId;

            mockInterestRepository.Setup(r => r.AssignInterest(hobbyistId, specialtyId))
                .Returns(Task.CompletedTask);
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);
            mockInterestRepository.Setup(r => r.FindByHobbyistIdAndSpecialtyId(hobbyistId, specialtyId))
                .Returns(Task.FromResult(interest));

            var service = new InterestService(mockInterestRepository.Object, mockUnitOfWork.Object);

            //Act
            InterestResponse result = await service.AssingInterestAsync(hobbyistId, specialtyId);
            Interest interestResult = result.Resource;

            //Assert
            interestResult.Should().Be(null);
        }

        [Test]
        public async Task AssignInterestWhenInvalidHobbyistIdOrSpecialtyIdReturnsInterestExceptionResponse()
        {
            //Arrange
            var mockInterestRepository = GetDefaultIInterestRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var hobbyistId = 1;
            var specialtyId = 1;

            mockInterestRepository.Setup(r => r.AssignInterest(hobbyistId, specialtyId))
                .Returns(Task.FromResult<Interest>(null));
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Throws(new Exception());

            var service = new InterestService(mockInterestRepository.Object, mockUnitOfWork.Object);

            //Act
            InterestResponse result = await service.AssingInterestAsync(hobbyistId, specialtyId);
            var message = result.Message;

            //Assert
            message.Should().Be("An error ocurred while assignig Interest: Exception of type 'System.Exception' was thrown.");
        }




        [Test]
        public async Task UnassignInterestWhenValidInterestReturnsInterestResponse()
        {
            //Arrange
            var mockInterestRepository = GetDefaultIInterestRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var interest = new Interest { HobbyistId = 1, SpecialtyId = 1 };
            var hobbyistId = interest.HobbyistId;
            var specialtyId = interest.SpecialtyId;

            mockInterestRepository.Setup(r => r.UnassignInterest(hobbyistId, specialtyId))
                .Returns(Task.CompletedTask);
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);
            mockInterestRepository.Setup(r => r.FindByHobbyistIdAndSpecialtyId(hobbyistId, specialtyId))
                .Returns(Task.FromResult(interest));

            var service = new InterestService(mockInterestRepository.Object, mockUnitOfWork.Object);

            //Act
            InterestResponse result = await service.UnassignInterestAsync(hobbyistId, specialtyId);
            Interest interestResult = result.Resource;

            //Assert
            interestResult.Should().Be(interest);
        }

        [Test]
        public async Task UnassignInterestWhenInvalidHobbyistIdOrSpecialtyIdReturnsHobbyistHasNoInterestResponse()
        {
            //Arrange
            var mockInterestRepository = GetDefaultIInterestRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var hobbyistId = 1;
            var specialtyId = 1;



            mockInterestRepository.Setup(r => r.UnassignInterest(hobbyistId, specialtyId))
                .Returns(Task.FromResult<Interest>(null));
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Throws(new Exception());

            var service = new InterestService(mockInterestRepository.Object, mockUnitOfWork.Object);

            //Act
            InterestResponse result = await service.UnassignInterestAsync(hobbyistId, specialtyId);
            var message = result.Message;

            //Assert
            message.Should().Be("Hobbyist has no interest in Specialty with id: 1");
        }

        private Mock<IInterestRepository> GetDefaultIInterestRepositoryInstance()
        {
            return new Mock<IInterestRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
