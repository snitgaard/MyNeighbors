using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Core.DomainServices
{
    public interface IReviewRepository
    {
        IEnumerable<Review> ReadAllReviews();
        Review CreateReview(Review review);
        Review ReadReviewById(int id);
        Review UpdateReview(Review updateReview);
        Review DeleteReview(int id);
    }
}
