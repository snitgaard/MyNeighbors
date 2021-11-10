using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNeighbors.Core.DomainServices;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Core.ApplicationServices.Services
{
    public class ReviewService: IReviewService
    {
        private readonly IReviewRepository _reviewRepo;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepo = reviewRepository;
        }
        public List<Review> GetAllReviews()
        {
            return _reviewRepo.ReadAllReviews().ToList();
        }

        public Review UpdateReview(Review updateReview)
        {
            if (updateReview == null)
            {
                throw new ArgumentException("Review is missing");
            }

            return _reviewRepo.UpdateReview(updateReview);
        }

        public Review FindReviewById(int id)
        {
            return _reviewRepo.ReadReviewById(id);
        }

        public Review DeleteReview(int id)
        {
            if (_reviewRepo.ReadReviewById(id) == null)
            {
                throw new InvalidOperationException("Cannot delete a non-existing house");
            }

            return _reviewRepo.DeleteReview(id);
        }

        public Review NewReview(string id, string description, double rating, DateTime date, double noise_rating,
            double shopping_rating, double schools_rating, User user, Address address)
        {
            throw new NotImplementedException();
        }

        public Review CreateReview(Review review)
        {
            return _reviewRepo.CreateReview(review);
        }
    }
}
