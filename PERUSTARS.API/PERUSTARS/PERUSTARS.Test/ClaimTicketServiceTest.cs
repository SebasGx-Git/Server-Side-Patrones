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
    class ClaimTicketServiceTest
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public async Task GetByIdAsyncWhenValidClaimTicketReturnsClaimTicket()
        {
            var mockClaimTicketRepository = GetDefaultIClaimTicketRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockHobbyistRepository = GetDefaultHobbyistRepositoryInstance();
            var mockArtistRepository = GetDefaultArtistRepositoryInstance();
            var hobbyistId = 1;
            var artistId = 2;
            var claimTicketId = 1;
            ClaimTicket claimTicket = new ClaimTicket();
            claimTicket.ClaimId = claimTicketId;
            mockClaimTicketRepository.Setup(r => r.FindByIdAndPersonId(claimTicketId, hobbyistId))
                .Returns(Task.FromResult(claimTicket));

            var service = new ClaimTicketService(mockClaimTicketRepository.Object, mockUnitOfWork.Object, mockArtistRepository.Object, mockHobbyistRepository.Object);

            // Act
            ClaimTicketResponse result = await service.GetByIdAndPersonIdAsync(claimTicketId, hobbyistId);
            var claimTicketResult = result.Resource;
            // Assert
            claimTicketResult.Should().Be(null);
        }
        [Test]
        public async Task GetByIdAsyncWhenNoClaimTicketReturnsClaimTicketNotFoundResponse()
        {
            // Arrange
            var mockClaimTicketRepository = GetDefaultIClaimTicketRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockHobbyistRepository = GetDefaultHobbyistRepositoryInstance();
            var mockArtistRepository = GetDefaultArtistRepositoryInstance();
            var hobbyistId = 1;
            var artistId = 2;
            var claimTicketId = 1;

            mockHobbyistRepository.Setup(r => r.FindById(hobbyistId))
                .Returns(Task.FromResult(new Hobbyist()));

            mockClaimTicketRepository.Setup(r => r.FindByIdAndPersonId(claimTicketId, hobbyistId))
                .Returns(Task.FromResult<ClaimTicket>(null));

            var service = new ClaimTicketService(mockClaimTicketRepository.Object, mockUnitOfWork.Object, mockArtistRepository.Object, mockHobbyistRepository.Object);

            // Act
            ClaimTicketResponse result = await service.GetByIdAndPersonIdAsync(claimTicketId, hobbyistId);
            var message = result.Message;
            // Assert
            message.Should().Be($"Claim Ticket not found by Person with Id: {hobbyistId}");
        }
        private Mock<IClaimTicketRepository> GetDefaultIClaimTicketRepositoryInstance()
        {
            return new Mock<IClaimTicketRepository>();
        }
        private Mock<IArtistRepository> GetDefaultArtistRepositoryInstance()
        {
            return new Mock<IArtistRepository>();
        }
        private Mock<IHobbyistRepository> GetDefaultHobbyistRepositoryInstance()
        {
            return new Mock<IHobbyistRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
