using System;
using System.Collections.Generic;
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

        [HttpGet]
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
 
        [HttpPost]
        public ActionResult<Review> Post([FromBody] Review review)
        {
            return _reviewService.CreateReview(review);
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
    }
}
