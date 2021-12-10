using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyNeighbors.Core.ApplicationServices;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Controllers
{
    public class ReviewController : ApiController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("all")]
        public ActionResult<List<Review>> Get()
        {
            return _reviewService.GetAllReviews();
        }

        [HttpGet("{id}")]
        public ActionResult<Review> Get(int id)
        {
            var review = _reviewService.FindReviewById(id);
            if (review == null)
            {
                return StatusCode(404, "Review not found");
            }
            try
            {
                return _reviewService.FindReviewById(id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Could not find a review on this id");
            }
        }

        [HttpGet("{addressId}&x={address_x}&y={address_y}")]
        public ActionResult<List<Review>> GetByAddressId(string addressId, double address_x, double address_y)
        {
            var review = _reviewService.FindReviewsByAddressId(addressId, address_x, address_y);
            if (review == null)
            {
                return StatusCode(404, "Review not found");
            }
            try
            {
                return _reviewService.FindReviewsByAddressId(addressId, address_x, address_y);
            }
            catch (Exception)
            {
                return StatusCode(500, "Could not find a review on this id");
            }
        }

        [HttpPost]
        public ActionResult<Review> Post([FromBody] Review review)
        {
            try
            {
                return _reviewService.CreateReview(review);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPut("{id}")]
        public ActionResult<Review> Put(int id, [FromBody] Review review)
        {
            try
            {
                return Ok(_reviewService.UpdateReview(review));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Review> Delete(int id)
        {
            var review = _reviewService.DeleteReview(id);
            if (review == null)
            {
                return StatusCode(404, "Could not delete review if the house doesn't exist");
            }
            try
            {
                return review;
            }
            catch (Exception)
            {
                return StatusCode(500, "Could not find review");
            }
        }

        [HttpGet("userid/{userId}")]
        public ActionResult<List<Review>> GetByUserId(int userId) 
        {
            var review = _reviewService.FindReviewsByUserId(userId);
            if (review == null)
            {
                return StatusCode(404, "Review not found");
            }

            try
            {
                return _reviewService.FindReviewsByUserId(userId);
            }
            catch (Exception)
            {
                return StatusCode(500, "Could not find a review on this id");
            }
        }
    }
}
