using Moq;
using MyNeighbors.Core.ApplicationServices.Services;
using MyNeighbors.Core.DomainServices;
using MyNeighbors.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest
{
    public class SponsorServiceTest
    {
        private Mock<ISponsorRepository<Sponsor>> repoMock;

        public SponsorServiceTest()
        {
            repoMock = new Mock<ISponsorRepository<Sponsor>>();
        }

        [Fact]
        public void CreateSponsorService()
        {
            ISponsorRepository<Sponsor> repo = repoMock.Object;

            SponsorService service = new SponsorService(repo);

            Assert.NotNull(service);
        }

        [Theory]
        [InlineData(1, "Elgiganten", "logo", 8.60435895, 55.72197902, "type")]
        public void AddValidSponsorNotExist(int id,  string name, string image, double x_coordinate, double y_coordinate, string type)
        {
            ISponsorRepository<Sponsor> repo = repoMock.Object;
            SponsorService service = new SponsorService(repo);

            Sponsor s = new Sponsor()
            {
                Id = id,
                Name = name,
                Image = image,
                X_coordinate = x_coordinate,
                Y_coordinate = y_coordinate,
                Type = type
             };

            service.CreateSponsor(s);

            repoMock.Verify(repo => repo.CreateSponsor(It.Is<Sponsor>((sp => sp == s))), Times.Once);
        }

        [Fact]
        public void AddSponsorExistingUserExpectInvalidOperationException()
        {
            Sponsor s = new Sponsor()
            {
                Id = 1,
                Name = "Virksomhed",
                Image = "Billede",
                X_coordinate = 2.0,
                Y_coordinate = 2.0,
                Type = "vand"
            };

            repoMock.Setup(repo => repo.GetSponsorById(It.Is<int>(x => x == s.Id))).Returns(() => s);

            SponsorService service = new SponsorService(repoMock.Object);

            var ex = Assert.Throws<InvalidOperationException>(() => service.CreateSponsor(s));

            Assert.Equal("This Sponsor already exists", ex.Message);
            repoMock.Verify(repo => repo.CreateSponsor(It.Is<Sponsor>(sp => sp == s)), Times.Never);
        }

        [Fact]
        public void AddSponsorIsNullExpectArgumentException()
        {
            SponsorService service = new SponsorService(repoMock.Object);

            var ex = Assert.Throws<ArgumentException>(() => service.CreateSponsor(null));

            Assert.Equal("Sponsor is missing", ex.Message);
            repoMock.Verify(repo => repo.CreateSponsor(It.Is<Sponsor>(s => s == null)), Times.Never);
        }

        [Theory]
        [InlineData(1, "", "logo", 2.0, 2.0, "type")] // test for empty name
        [InlineData(1, "Facebook", "", 2.0, 2.0, "korrupt")] // test for empty image
        [InlineData(1, "Facebook", "logo", 181, 2.0, "type")] // test for x over 180
        [InlineData(1, "Facebook", "logo", 2.0, 91, "type")] // test for y over 180
        [InlineData(1, "Facebook", "logo", -181, 2.0, "type")] // test for x lower than -180
        [InlineData(1, "Facebook", "logo", 2.0, -91, "type")] // test for y lower than -90
        [InlineData(1, "Facebook", "logo", 2.0, 2.0, "")] // test for empty type
        public void AddInvalidSponsorExpectArgumentException(int id, string name, string image, double x_coordinate, double y_coordinate, string type)
        {
            SponsorService service = new SponsorService(repoMock.Object);

            Sponsor s = new Sponsor()
            {
                Id = id,
                Name = name,
                Image = image,
                X_coordinate = x_coordinate,
                Y_coordinate = y_coordinate,
                Type = type
            };

            var ex = Assert.Throws<ArgumentException>(() => service.CreateSponsor(s));

            Assert.Equal("Invalid sponsor property", ex.Message);
            repoMock.Verify(repo => repo.CreateSponsor((It.Is<Sponsor>(sp => sp == s))), Times.Never);
        }

        [Fact]
        public void DeleteExistingSponsor()
        {
            Sponsor s = new Sponsor()
            {
                Id = 1,
                Name = "Virksomhed",
                Image = "Billede",
                X_coordinate = 2.0,
                Y_coordinate = 2.0,
                Type = "vand"
            };

            repoMock.Setup(repo => repo.GetSponsorById(It.Is<int>(x => x == s.Id))).Returns(() => s);

            SponsorService service = new SponsorService(repoMock.Object);

            service.DeleteSponsor(s.Id);

            repoMock.Verify(repo => repo.DeleteSponsor(It.Is<int>(sp => sp == s.Id)), Times.Once);
        }

        [Fact]
        public void DeleteSponsorNotExistExpectInvalidOperationException()
        {
            Sponsor s = new Sponsor()
            {
                Id = 1,
                Name = "Virksomhed",
                Image = "Billede",
                X_coordinate = 2.0,
                Y_coordinate = 2.0,
                Type = "vand"
            };

            repoMock.Setup(repo => repo.GetSponsorById(It.Is<int>(x => x == s.Id))).Returns(() => null);

            SponsorService service = new SponsorService(repoMock.Object);

            var ex = Assert.Throws<InvalidOperationException>(() => service.DeleteSponsor(s.Id));

            Assert.Equal("Cannot remove sponsor that does not exist", ex.Message);
            repoMock.Verify(repo => repo.DeleteSponsor(It.Is<int>(sp => sp == s.Id)), Times.Never);
        }

        [Fact]
        public void GetSponsorByIdExistingUser()
        {
            Sponsor s = new Sponsor()
            {
                Id = 1,
                Name = "Virksomhed",
                Image = "Billede",
                X_coordinate = 2.0,
                Y_coordinate = 2.0,
                Type = "vand"
            };

            repoMock.Setup(repo => repo.GetSponsorById(It.Is<int>(x => x == s.Id))).Returns(() => null);

            SponsorService service = new SponsorService(repoMock.Object);

            var result = service.GetSponsorById(s.Id);

            Assert.Null(result);
            repoMock.Verify(repo => repo.GetSponsorById(It.Is<int>(x => x == s.Id)), Times.Once);
        }

        [Fact]
        public void GetSponsorByIdNonExistingUserReturnsNull()
        {
            int id = 1;

            repoMock.Setup(repo => repo.GetSponsorById(It.Is<int>(s => s == id))).Returns(() => null);

            SponsorService service = new SponsorService(repoMock.Object);

            var result = service.GetSponsorById(id);

            Assert.Null(result);
            repoMock.Verify(repo => repo.GetSponsorById(It.Is<int>(s => s == id)), Times.Once);
        }

        [Theory]        // empty repository, 1 in repository, n in repository
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllSponsors(int sponsorCount)
        {
            // arrange
            List<Sponsor> data = new List<Sponsor>()
            {
                new Sponsor(){ Id = 1},
                new Sponsor() { Id = 2}
            };

            repoMock.Setup(repo => repo.GetAllSponsors()).Returns(() => data.GetRange(0, sponsorCount));

            SponsorService service = new SponsorService(repoMock.Object);

            // act

            var result = service.GetAllSponsors();

            // assert
            Assert.Equal(result.Count(), sponsorCount);
            Assert.Equal(result.ToList(), data.GetRange(0, sponsorCount));
            repoMock.Verify(repo => repo.GetAllSponsors(), Times.Once);
        }

    }
}