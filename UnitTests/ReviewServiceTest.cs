using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using MyNeighbors.Core.ApplicationServices.Services;
using MyNeighbors.Core.DomainServices;
using MyNeighbors.Core.Entity;
using Xunit;

namespace MyNeighbors.UnitTests
{
    public class ReviewServiceTest
    {
        private Mock<IReviewRepository> reviewMock;

        public ReviewServiceTest()
        {
            reviewMock = new Mock<IReviewRepository>();
        }

        [Fact]
        public void CreateReviewService()
        {
            IReviewRepository repo = reviewMock.Object;
            ReviewService service = new ReviewService(repo);
            Assert.NotNull(service);
        }

        [InlineData(1, "Noisy neighbors", 5, "2021-10-11", 3, 2, 4, 1)]
        [Theory]
        public void CreateValidReviewNotExist(int id, string description, int rating, string date, int noise_rating,
            int shopping_rating, int schools_rating, int userId)
        {
            IReviewRepository repo = reviewMock.Object;
            ReviewService service = new ReviewService(repo);

            Review r = new Review()
            {
                Id = id,
                Description = description,
                Date = DateTime.Parse(date),
                Noise_Rating = noise_rating,
                Shopping_Rating = shopping_rating,
                Schools_Rating = schools_rating,
                UserId = userId
            };
            service.CreateReview(r);
            reviewMock.Verify(repo => repo.CreateReview(It.Is<Review>((re => re == r))), Times.Once);
        }
        [Fact]
        public void CreateReviewExistingReviewExpectInvalidOperationException()
        {
            Review r = new Review()
            {
                Id = 1,
                Description = "Noisy neighbors",
                Date = DateTime.Parse("2021-11-10"),
                Noise_Rating = 2,
                Shopping_Rating = 5,
                Schools_Rating = 3,
                UserId = 1
            };
            reviewMock.Setup(repo => repo.ReadReviewById(It.Is<int>(x => x == r.Id))).Returns(() => r);
            ReviewService service = new ReviewService(reviewMock.Object);

            var reviewEx = Assert.Throws<InvalidOperationException>(() => service.CreateReview(r));
            Assert.Equal("This review already exists", reviewEx.Message);
            reviewMock.Verify(repo => repo.CreateReview(It.Is<Review>(review => review == r)), Times.Never);
        }

        [Fact]
        public void CreateReviewIsNullExpectArgumentException()
        {
            ReviewService service = new ReviewService(reviewMock.Object);
            var ex = Assert.Throws<ArgumentException>(() => service.CreateReview(null));
            Assert.Equal("Review is missing", ex.Message);
            reviewMock.Verify(repo => repo.CreateReview(It.Is<Review>(r => r == null)), Times.Never);
        }

        [InlineData(1, null, 5, "2021-10-11", 3, 2, 4, 1)]
        [InlineData(1, "", 5, "2021-10-11", 3, 2, 4, 1)]
        [InlineData(1, "Noisy neighbors", -1, "2021-10-11", 3, 2, 4, 1)]
        [InlineData(1, "Noisy neighbors", 5, "2021-10-11", -1, 2, 4, 1)]
        [InlineData(1, "Noisy neighbors", 5, "2021-10-11", 3, -1, 4, 1)]
        [InlineData(1, "Noisy neighbors", 5, "2021-10-11", 3, 2, -1, 1)]
        [Theory]
        public void CreateInvalidReviewExpectArgumentException(int id, string description, int rating, string date,
            int noise_rating, int shopping_rating, int schools_rating, int userId)
        {
            ReviewService service = new ReviewService(reviewMock.Object);
            Review r = new Review()
            {
                Id = id,
                Description = description,
                Date = DateTime.Parse(date),
                Noise_Rating = noise_rating,
                Shopping_Rating = shopping_rating,
                Schools_Rating = schools_rating,
                UserId = userId
            };
            var ex = Assert.Throws<ArgumentException>(() => service.UpdateReview(r));
            Assert.Equal("Invalid review property", ex.Message);
            reviewMock.Verify(repo => repo.UpdateReview(It.Is<Review>(re => re == r)), Times.Never);
        }

        [Fact]
        public void DeleteExistingReview()
        {
            Review r = new Review()
            {
                Id = 1,
                Description = "Noisy neighbors",
                Date = DateTime.Parse("2021-11-10"),
                Noise_Rating = 2,
                Shopping_Rating = 5,
                Schools_Rating = 3,
                UserId = 1
            };
            reviewMock.Setup(repo => repo.ReadReviewById(It.Is<int>(x => x == r.Id))).Returns(() => r);
            ReviewService service = new ReviewService(reviewMock.Object);
            service.DeleteReview(r.Id);
            reviewMock.Verify(repo => repo.DeleteReview(It.Is<int>(x => x == r.Id)), Times.Once);
        }

        [Fact]
        public void DeleteReviewNotExistExpectInvalidOperationException()
        {
            Review r = new Review()
            {
                Id = 1,
                Description = "Noisy neighbors",
                Date = DateTime.Parse("2021-11-10"),
                Noise_Rating = 2,
                Shopping_Rating = 5,
                Schools_Rating = 3,
                UserId = 1
            };
            reviewMock.Setup(repo => repo.ReadReviewById(It.Is<int>(x => x == r.Id))).Returns(() => null);
            ReviewService service = new ReviewService(reviewMock.Object);
            var ex = Assert.Throws<InvalidOperationException>(() => service.DeleteReview(r.Id));
            Assert.Equal("Cannot remove review that does not exist", ex.Message);
            reviewMock.Verify(repo => repo.DeleteReview(It.Is<int>(x => x == r.Id)), Times.Never);
        }

        [Fact]
        public void GetReviewByIdExistingReview()
        {
            Review r = new Review()
            {
                Id = 1,
                Description = "Noisy neighbors",
                Date = DateTime.Parse("2021-11-10"),
                Noise_Rating = 2,
                Shopping_Rating = 5,
                Schools_Rating = 3,
                UserId = 1
            };
            reviewMock.Setup(repo => repo.ReadReviewById(It.Is<int>(x => x == r.Id))).Returns(() => r);
            ReviewService service = new ReviewService(reviewMock.Object);
            var result = service.FindReviewById(r.Id);
            Assert.Equal(result, r);
            reviewMock.Verify(repo => repo.ReadReviewById(It.Is<int>(x => x == r.Id)), Times.Once);
        }

        [Fact]
        public void GetReviewByIdNonExistingReviewReturnsNull()
        {
            int id = 1;
            reviewMock.Setup(repo => repo.ReadReviewById(It.Is<int>(x => x == id))).Returns(() => null);
            ReviewService service = new ReviewService(reviewMock.Object);
            var result = service.FindReviewById(id);
            Assert.Null(result);
            reviewMock.Verify(repo => repo.ReadReviewById(It.Is<int>(x => x == id)), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllReviews(int reviewCount)
        {
            List<Review> data = new List<Review>()
            {
                new Review(){Id = 1},
                new Review(){Id = 2}
            };
            reviewMock.Setup(repo => repo.ReadAllReviews()).Returns(() => data.GetRange(0, reviewCount));
            ReviewService service = new ReviewService(reviewMock.Object);
            var result = service.GetAllReviews();
            Assert.Equal(result.Count(), reviewCount);
            Assert.Equal(result.ToList(), data.GetRange(0, reviewCount));
            reviewMock.Verify(repo => repo.ReadAllReviews(), Times.Once);

        }

    }
}
