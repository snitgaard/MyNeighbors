using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Core.ApplicationServices
{
    public interface IReviewService
    {
        List<Review> GetAllReviews();
        Review UpdateReview(Review updateReview);
        Review FindReviewById(int id);
        List<Review> FindReviewsByAddressId(string addressId);
        Review DeleteReview(int id);

        Review NewReview(string id, string description, double rating, DateTime date, int noise_rating,
            int shopping_rating, int schools_rating, User user, Address address);

        Review CreateReview(Review review);
        List<Review> FindReviewsByUserId(int userId);
    }
}
