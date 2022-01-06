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
        Review GetReviewById(int id);
        List<Review> GetReviewsByAddressId(string addressId, double address_x, double address_y);
        Review DeleteReview(int id);
        Review CreateReview(Review review);
        List<Review> GetReviewsByUserId(int userId);
    }
}
