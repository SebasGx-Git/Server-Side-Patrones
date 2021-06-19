using NUnit.Framework;
using Moq;
using FluentAssertions;
using PERUSTARS.Domain.Models;
using PERUSTARS.Domain.Services.Communications;
using PERUSTARS.Domain.Persistence.Repositories;
using PERUSTARS.Services;
using System.Threading.Tasks;
using System.Collections;

namespace PERUSTARS.Test
{
    public class ArtistServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        // GET BY ID
        [Test]
        public async Task GetByIdAsyncWhenValidArtistReturnsArtist()
        {
            // Arrange
            var mockArtistRepository = GetDefaultIArtistRepositoryInstance();
            var mockFollowerRepository = GetDefaultIFollowerRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var artistId = 1;
            Artist artist = new Artist();
            artist.Id = artistId;
            mockArtistRepository.Setup(r => r.FindById(artistId))
                .Returns(Task.FromResult(artist));

            var service = new ArtistService(mockArtistRepository.Object, mockUnitOfWork.Object, mockFollowerRepository.Object);

            // Act
            ArtistResponse result = await service.GetByIdAsync(artistId);
            var artistResult = result.Resource;
            // Assert
            artistResult.Should().Be(artist);
        }
        [Test]
        public async Task GetByIdAsyncWhenNoArtistFoundReturnsArtistNotFoundResponse()
        {
            // Arrange
            var mockArtistRepository = GetDefaultIArtistRepositoryInstance();
            var mockFollowerRepository = GetDefaultIFollowerRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var artistId = 1;
            mockArtistRepository.Setup(r => r.FindById(artistId))
                .Returns(Task.FromResult<Artist>(null));

            var service = new ArtistService(mockArtistRepository.Object, mockUnitOfWork.Object, mockFollowerRepository.Object);

            // Act
            ArtistResponse result = await service.GetByIdAsync(artistId);
            var message = result.Message;
            // Assert
            message.Should().Be("Artist not found");
        }


        //Artista repite brand name
        [Test]
        public async Task GetIsSameBrandNamewhenArtistRepitBrandName()
        {


            //ARRANGE
            var mockArtistRepository = GetDefaultIArtistRepositoryInstance();
            var mockFollowerRepository = GetDefaultIFollowerRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            Artist artista1 = new Artist();
            Artist artista2 = new Artist();
            Artist artista3 = new Artist();

            artista1.Id = 1;
            artista1.BrandName = "Prueba";

            artista2.Id = 2;
            artista2.BrandName = "Test";

            artista3.Id = 3;
            artista3.BrandName = "Prueba_Test";

            mockArtistRepository.Setup(r => r.isSameBrandingName("Prueba"))
               .Returns(Task.FromResult(true));

            var service = new ArtistService(mockArtistRepository.Object, mockUnitOfWork.Object, mockFollowerRepository.Object);

            //Act

            bool result = await service.isSameBrandingName("Prueba");

            //Asert

            Assert.IsTrue(result);

        }


        [Test]
        public async Task GetIsSameBrandNamewhenArtistNOTRepitBrandName()
        {


            //ARRANGE
            var mockArtistRepository = GetDefaultIArtistRepositoryInstance();
            var mockFollowerRepository = GetDefaultIFollowerRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            Artist artista1 = new Artist();
            Artist artista2 = new Artist();
            Artist artista3 = new Artist();

            artista1.Id = 1;
            artista1.BrandName = "Prueba";

            artista2.Id = 2;
            artista2.BrandName = "Test";

            artista3.Id = 3;
            artista3.BrandName = "Prueba_Test";

            mockArtistRepository.Setup(r => r.isSameBrandingName("nuevoBrandName"))
               .Returns(Task.FromResult(false));

            var service = new ArtistService(mockArtistRepository.Object, mockUnitOfWork.Object, mockFollowerRepository.Object);

            //Act

            bool result = await service.isSameBrandingName("nuevoBrandName");

            //Asert

            Assert.AreEqual(result, false);

        }



        //Artista NO repite brand name
        private Mock<IArtistRepository> GetDefaultIArtistRepositoryInstance()
        {
            return new Mock<IArtistRepository>(); 
        }
        private Mock<IFollowerRepository> GetDefaultIFollowerRepositoryInstance()
        {
            return new Mock<IFollowerRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}