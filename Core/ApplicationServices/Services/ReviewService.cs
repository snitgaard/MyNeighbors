﻿using System;
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
            return _reviewRepo.ReadAllReviews().ToList();
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

            if (_reviewRepo.ReadReviewById(updateReview.Id) == null)
            {
                throw new InvalidOperationException("Review does not exist");
            }
            return _reviewRepo.UpdateReview(updateReview);
        }

        public Review FindReviewById(int id)
        {
            return _reviewRepo.ReadReviewById(id);
        }

        public List<Review> FindReviewsByAddressId(string addressId, double address_x, double address_y)
        {
            return _reviewRepo.FindReviewsByAddressId(addressId, address_x, address_y).ToList();
        }


        public Review DeleteReview(int id)
        {
            if (_reviewRepo.ReadReviewById(id) == null)
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
            return _reviewRepo.CreateReview(review);
        }

        private bool IsValidReview(Review review)
        {
            return (review.Noise_Rating > -1
                    && review.Schools_Rating > -1
                    && review.Shopping_Rating > -1)
                    && review.Rating > -1;
        }

        public Review NewReview(string id, string description, double rating, DateTime date, int noise_rating, int shopping_rating, int schools_rating, User user, Address address)
        {
            throw new NotImplementedException();
        }

        public List<Review> FindReviewsByUserId(int userId)
        {
            return _reviewRepo.FindReviewsByUserId(userId).ToList();
        }
    }
}
