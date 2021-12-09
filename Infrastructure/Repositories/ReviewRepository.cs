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

        public IEnumerable<Review> FindReviewsByAddressId(string addressId, double address_x, double address_y)
        {
            var ratioX = 0.004661679103927;
            var ratioY = 0.00169972628037;

            var minX = address_x - ratioX;
            var minY = address_y - ratioY;
            var maxX = address_x + ratioX;
            var maxY = address_y + ratioY;
            Console.WriteLine(minX + ", " + minY + "& " + maxX + ", " + maxY);
            return _ctx.Review
                .Where(r => r.AddressId == addressId).OrderByDescending(r => r.Date)
                .Where(r=> r.Address_x >= minX && r.Address_x <= maxX && r.Address_y >= minY && r.Address_y <= maxY)
                .ToList();
        }

        public IEnumerable<Review> FindReviewsByUserId(int userId)
        {
            return _ctx.Review.Where(r => r.UserId == userId).OrderByDescending(r => r.Date).ToList();
        }

    }
}
