using NUnit.Framework;
using Moq;
using FluentAssertions;
using PERUSTARS.Domain.Models;
using PERUSTARS.Domain.Services.Communications;
using PERUSTARS.Domain.Persistence.Repositories;
using PERUSTARS.Services;
using System.Threading.Tasks;

namespace PERUSTARS.Test
{
    class SpecialtyServiceTest
    {
        [SetUp]
        public void Setup(){}

        [Test]
        public async Task GetByIdAsyncWhenValidSpecialtyReturnsSpecialty()
        {
            // Arrange
            var mockSpecialtyRepository = GetDefaultISpecialtyRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockInterestRepository = GetDefaultInterestRepositoryInstance();
            var specialtyId = 1;
            Specialty specialty = new Specialty();
            specialty.Id = specialtyId;
            mockSpecialtyRepository.Setup(r => r.FindById(specialtyId))
                .Returns(Task.FromResult(specialty));

            var service = new SpecialtyService(mockSpecialtyRepository.Object, mockUnitOfWork.Object, mockInterestRepository.Object);

            // Act
            SpecialtyResponse result = await service.GetByIdAsync(specialtyId);
            var specialtyResult = result.Resource;
            // Assert
            specialtyResult.Should().Be(specialty);
        }
        [Test]
        public async Task GetByIdAsyncWhenNoSpecialtyReturnsSpecialtyNotFoundResponse()
        {
            // Arrange
            var mockSpecialtyRepository = GetDefaultISpecialtyRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockInterestRepository = GetDefaultInterestRepositoryInstance();
            var specialtyId = 1;
            mockSpecialtyRepository.Setup(r => r.FindById(specialtyId))
                .Returns(Task.FromResult<Specialty>(null));

            var service = new SpecialtyService(mockSpecialtyRepository.Object, mockUnitOfWork.Object, mockInterestRepository.Object);

            // Act
            SpecialtyResponse result = await service.GetByIdAsync(specialtyId);
            var message = result.Message;
            // Assert
            message.Should().Be("Specialty not found");
        }
        private Mock<ISpecialtyRepository> GetDefaultISpecialtyRepositoryInstance()
        {
            return new Mock<ISpecialtyRepository>();
        }
        private Mock<IInterestRepository> GetDefaultInterestRepositoryInstance()
        {
            return new Mock<IInterestRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
