using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Core.DomainServices
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetAllReviews();
        Review CreateReview(Review review);
        Review GetReviewById(int id);
        Review UpdateReview(Review updateReview);
        Review DeleteReview(int id);
        IEnumerable<Review> GetReviewsByUserId(int userId);
        IEnumerable<Review> GetReviewsByAddressId(string addressId, double address_x, double address_y);
    }
}
