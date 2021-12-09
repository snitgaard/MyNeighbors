using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyNeighbors.Core.DomainServices;
using MyNeighbors.Core.Entity;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace MyNeighbors.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private MyNeighborsContext _ctx;

        public ReviewRepository(MyNeighborsContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Review> ReadAllReviews()
        {
            return _ctx.Review.AsNoTracking().OrderByDescending(r => r.Date);
        }

        public Review CreateReview(Review review)
        {
            _ctx.DetachAll();
            _ctx.Attach(review).State = EntityState.Added;
            _ctx.SaveChanges();
            return review;
        }

        public Review ReadReviewById(int id)
        {
            return _ctx.Review.AsNoTracking().FirstOrDefault(r => r.Id == id);
        }

        public Review UpdateReview(Review updateReview)
        {
            _ctx.DetachAll();
            _ctx.Attach(updateReview).State = EntityState.Modified;
            _ctx.SaveChanges();
            return updateReview;
        }

        public Review DeleteReview(int id)
        {
            var review = _ctx.Review.FirstOrDefault();
            var removedReview = _ctx.Remove(review).Entity;
            _ctx.SaveChanges();
            return removedReview;
        }

        public IEnumerable<Review> FindReviewsByAddressId(string addressId)
        {
            return _ctx.Review.Where(r => r.AddressId == addressId).OrderByDescending(r => r.Date).ToList();
        }

        public IEnumerable<Review> FindReviewsByUserId(int userId)
        {
            return _ctx.Review.Where(r => r.UserId == userId).OrderByDescending(r => r.Date).ToList();
        }
    }
    
}
