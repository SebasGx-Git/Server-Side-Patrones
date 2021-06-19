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
    class EventServiceTest
    {

        [SetUp]
        public void Setup()
        {
        }

        // GET BY ID
        [Test]
        public async Task GetByIdWhenValidEventsReturnsEvents()
        {
            // Arrange
            var mockEventRepository = GetDefaultIEventRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockBookingRepository = new Mock<IEventAssistanceRepository>();
            var mockArtistRepository = GetDefaultIArtistRepositoryInstance();
            var artistId = 1;

            var eventId = 1;
            Event _event = new Event();
            _event.EventId = eventId;
            mockEventRepository.Setup(r => r.FindById(eventId))
                .Returns(Task.FromResult(_event));

            var service = new EventService(mockEventRepository.Object, mockBookingRepository.Object, mockUnitOfWork.Object, mockArtistRepository.Object);

            // Act
            EventResponse result = await service.GetByIdAndArtistIdAsync(eventId, artistId);
            var eventResult = result.Resource;
            // Assert
            eventResult.Should().Be(null);
        }

        [Test]
        public async Task GetByIdWhenNoEventReturnsEventNotFoundResponse()
        {
            // Arrange
            var mockEventRepository = GetDefaultIEventRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockBookingRepository = new Mock<IEventAssistanceRepository>();
            var mockArtistRepository = GetDefaultIArtistRepositoryInstance();
            var artistId = 1;
            var eventId = 1;

            mockArtistRepository.Setup(r => r.FindById(artistId))
                .Returns(Task.FromResult(new Artist()));

            mockEventRepository.Setup(r => r.FindById(eventId))
               .Returns(Task.FromResult<Event>(null));

            var service = new EventService(mockEventRepository.Object, mockBookingRepository.Object, mockUnitOfWork.Object, mockArtistRepository.Object);

            // Act
            EventResponse result = await service.GetByIdAndArtistIdAsync(eventId, artistId);
            var message = result.Message;

            // Assert
            message.Should().Be("Event not found");
        }




        [Test]
        public async Task GetIsSameTitleWhenArtistRepitTitle()
        {
            //Arrange
            var mockEventRepository = GetDefaultIEventRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockBookingRepository = new Mock<IEventAssistanceRepository>();
            var mockArtistRepository = GetDefaultIArtistRepositoryInstance();

            Artist artist = new Artist();
            artist.Id = 1;
            artist.Firstname = "Sebastian";
            artist.Lastname = "Gonzales";
            artist.BrandName = "SebasGx";

            Event event1 = new Event();
            event1.EventId = 1;
            event1.EventTitle = "hola";
            event1.ArtistId = 1;

            Event event2 = new Event();
            event2.EventId = 2;
            event2.EventTitle = "adios";
            event1.ArtistId = 1;

            mockEventRepository.Setup(r => r.isSameTitle("hola", 1))
                  .Returns(Task.FromResult(true));

            var service = new EventService(mockEventRepository.Object, mockBookingRepository.Object, mockUnitOfWork.Object, mockArtistRepository.Object);


            //Act


            bool result = await service.isSameTitle("hola", 1);


            //Assert
            Assert.IsTrue(result);
        }


        [Test]
        public async Task GetIsSameTitleWhenArtistNOTRepitTitle()
        {
            //Arrange
            var mockEventRepository = GetDefaultIEventRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockBookingRepository = new Mock<IEventAssistanceRepository>();
            var mockArtistRepository = GetDefaultIArtistRepositoryInstance();

            Artist artist = new Artist();
            artist.Id = 1;
            artist.Firstname = "Sebastian";
            artist.Lastname = "Gonzales";
            artist.BrandName = "SebasGx";

            Event event1 = new Event();
            event1.EventId = 1;
            event1.EventTitle = "hola";
            event1.ArtistId = 1;

            Event event2 = new Event();
            event2.EventId = 2;
            event2.EventTitle = "adios";
            event1.ArtistId = 1;

            mockEventRepository.Setup(r => r.isSameTitle("titulonuevo", 1))
                  .Returns(Task.FromResult(false));

            var service = new EventService(mockEventRepository.Object, mockBookingRepository.Object, mockUnitOfWork.Object, mockArtistRepository.Object);


            //Act


            bool result = await service.isSameTitle("titulonuevo", 1);


            //Assert
            Assert.AreEqual(result, false);
        }


        private Mock<IEventRepository> GetDefaultIEventRepositoryInstance()
        {
            return new Mock<IEventRepository>();
        }
        private Mock<IArtistRepository> GetDefaultIArtistRepositoryInstance()
        {
            return new Mock<IArtistRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }



    }
}