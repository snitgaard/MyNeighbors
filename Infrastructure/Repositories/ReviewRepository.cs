using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Linq;
using EFCore.BulkExtensions;
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

        public IEnumerable<Review> GetAllReviews()
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

        public Review GetReviewById(int id)
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
            var review = _ctx.Review.FirstOrDefault(r => r.Id == id);
            var removedReview = _ctx.Remove(review).Entity;
            _ctx.SaveChanges();
            return removedReview;
        }

        public IEnumerable<Review> GetReviewsByAddressId(string addressId, double address_x, double address_y)
        {
            var ratioX = 0.002379515501747;
            var ratioY = 0.00137317885235;

            var minX = address_x - ratioX;
            var minY = address_y - ratioY;
            var maxX = address_x + ratioX;
            var maxY = address_y + ratioY;
            return _ctx.Review
                .Where(r => r.Address_x >= minX && r.Address_x <= maxX && r.Address_y >= minY && r.Address_y <= maxY)
                .OrderByDescending(r => r.Date)
                .ToList();
        }

        public IEnumerable<Review> GetReviewsByUserId(int userId)
        {
            return _ctx.Review.Where(r => r.UserId == userId).OrderByDescending(r => r.Date).ToList();
        }

    }
}
