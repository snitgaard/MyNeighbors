using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Moq.Language.Flow;
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
            return _reviewRepo.GetAllReviews().ToList();
        }

        public Review UpdateReview(Review updateReview)
        {
            if (updateReview == null)
            {
                throw new ArgumentException("Review is missing");
            }

            if (!IsValidReview(updateReview))
            {
                throw new ArgumentException("Invalid review property");
            }

            if (_reviewRepo.GetReviewById(updateReview.Id) == null)
            {
                throw new InvalidOperationException("Review does not exist");
            }
            return _reviewRepo.UpdateReview(updateReview);
        }

        public Review GetReviewById(int id)
        {
            return _reviewRepo.GetReviewById(id);
        }

        public List<Review> GetReviewsByAddressId(string addressId, double address_x, double address_y)
        {
            return _reviewRepo.GetReviewsByAddressId(addressId, address_x, address_y).ToList();
        }

        public Review DeleteReview(int id)
        {
            if (_reviewRepo.GetReviewById(id) == null)
            {
                throw new InvalidOperationException("Cannot remove review that does not exist");
            }

            return _reviewRepo.DeleteReview(id);
        }

        public Review CreateReview(Review review)
        {
            if (review == null)
            {
                throw new ArgumentException("Review is missing");
            }

            if (!IsValidReview(review))
            {
                throw new ArgumentException("Invalid review property");
            }

            if (_reviewRepo.GetReviewById(review.Id) != null)
            {
                throw new InvalidOperationException("This review already exists");
            }
            return _reviewRepo.CreateReview(review);
        }

        private bool IsValidReview(Review review)
        {
            return (review.Noise_Rating > -1
                    && review.Schools_Rating > -1
                    && review.Shopping_Rating > -1)
                    && review.Rating > -1
                    && review.Schools_Rating <= 5
                    && review.Shopping_Rating <= 5
                    && review.Noise_Rating <= 5
                    && review.Rating <= 5
                    && review.Address_x <= 180
                    && review.Address_y <= 90
                    && review.Address_x >= -180
                    && review.Address_y >= -90;
        }

        public List<Review> GetReviewsByUserId(int userId)
        {
            return _reviewRepo.GetReviewsByUserId(userId).ToList();
        }
    }
}
