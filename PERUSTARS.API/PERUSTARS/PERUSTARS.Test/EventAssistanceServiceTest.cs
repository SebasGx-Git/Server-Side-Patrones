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
    class EventAssistanceServiceTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task AssignEventAssistanceWhenValidEventAssistanceReturnsEventAssistanceResponse()
        {
            //Arrange
            var mockEventAssistanceRepository = GetDefaultIEventAssistanceRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var eventAssistance = new EventAssistance { EventId = 1, HobbyistId = 1, AttendanceDay = DateTime.Now };
            var hobbyistId = eventAssistance.HobbyistId;
            var eventId = eventAssistance.EventId;
            var attendance = eventAssistance.AttendanceDay;

            mockEventAssistanceRepository.Setup(r => r.AssignEventAssistance(hobbyistId, eventId, attendance))
                .Returns(Task.CompletedTask);
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);
            mockEventAssistanceRepository.Setup(r => r.FindByHobbyistIdAndEventIdAsync(hobbyistId, eventId))
                .Returns(Task.FromResult(eventAssistance));

            var service = new EventAssistanceService(mockEventAssistanceRepository.Object, mockUnitOfWork.Object);

            //Act
            EventAssistanceResponse result = await service.AssignEventAssistanceAsync(hobbyistId, eventId, attendance);
            EventAssistance eventAssistanceResult = result.Resource;

            //Assert
            eventAssistanceResult.Should().Be(eventAssistance);
        }

        [Test]
        public async Task AssignEventAssistanceWhenInvalidHobbyistIdOrEventIdReturnsEventAssistanceExceptionResponse()
        {
            //Arrange
            var mockEventAssistanceRepository = GetDefaultIEventAssistanceRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var hobbyistId = 1;
            var eventId = 1;
            var attendance = DateTime.Now;

            mockEventAssistanceRepository.Setup(r => r.AssignEventAssistance(hobbyistId, eventId, attendance))
                .Returns(Task.FromResult<EventAssistance>(null));
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Throws(new Exception());

            var service = new EventAssistanceService(mockEventAssistanceRepository.Object, mockUnitOfWork.Object);

            //Act
            EventAssistanceResponse result = await service.AssignEventAssistanceAsync(hobbyistId, eventId, attendance);
            var message = result.Message;

            //Assert
            message.Should().Be("An error ocurred while assigning a EventAssistance: Exception of type 'System.Exception' was thrown.");
        }




        [Test]
        public async Task UnassignEventAssistanceWhenValidEventAssistanceReturnsEventAssistanceResponse()
        {
            //Arrange
            var mockEventAssistanceRepository = GetDefaultIEventAssistanceRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var eventAssistance = new EventAssistance { EventId = 1, HobbyistId = 1, AttendanceDay = DateTime.Now };
            var hobbyistId = eventAssistance.HobbyistId;
            var eventId = eventAssistance.EventId;

            mockEventAssistanceRepository.Setup(r => r.UnassignEventAssistance(hobbyistId, eventId))
                .Returns(Task.CompletedTask);
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);
            mockEventAssistanceRepository.Setup(r => r.FindByHobbyistIdAndEventIdAsync(hobbyistId, eventId))
                .Returns(Task.FromResult(eventAssistance));

            var service = new EventAssistanceService(mockEventAssistanceRepository.Object, mockUnitOfWork.Object);

            //Act
            EventAssistanceResponse result = await service.UnassignEventAssistanceAsync(hobbyistId, eventId);
            EventAssistance eventAssistanceResult = result.Resource;

            //Assert
            eventAssistanceResult.Should().Be(eventAssistance);
        }

        [Test]
        public async Task UnassignEventAssistanceWhenInvalidEventAssistanceReturnsEventAssistanceExceptionResponse()
        {
            //Arrange
            var mockEventAssistanceRepository = GetDefaultIEventAssistanceRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var hobbyistId = 1;
            var eventId = 1;

            mockEventAssistanceRepository.Setup(r => r.UnassignEventAssistance(hobbyistId, eventId))
                .Returns(Task.FromResult<EventAssistance>(null));
            mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Throws(new Exception());

            var service = new EventAssistanceService(mockEventAssistanceRepository.Object, mockUnitOfWork.Object);

            //Act
            EventAssistanceResponse result = await service.UnassignEventAssistanceAsync(hobbyistId, eventId);
            var message = result.Message;

            //Assert
            message.Should().Be("An error ocurred while unassigning a EventAssistance: Exception of type 'System.Exception' was thrown.");
        }

        private Mock<IEventAssistanceRepository> GetDefaultIEventAssistanceRepositoryInstance()
        {
            return new Mock<IEventAssistanceRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
