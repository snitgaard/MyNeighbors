using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeighbors.Core.ApplicationServices;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Controllers
{
    public class SponsorController : ApiController
    {
        private readonly ISponsorService _sponsorService;

        public SponsorController(ISponsorService sponsorService)
        {
            _sponsorService = sponsorService;
        }

        [HttpGet("all")]
        public ActionResult<List<Sponsor>> Get()
        {
            return _sponsorService.GetAllSponsors();
        }

        [HttpGet("{id}")]
        public ActionResult<Sponsor> Get(int id)
        {
            var sponsor = _sponsorService.GetSponsorById(id);
            if (sponsor == null)
            {
                return StatusCode(404, "Sponsor not found");
            }
            try
            {
                return _sponsorService.GetSponsorById(id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Could not find a sponsor on this id");
            }
        }

        [HttpPost]
        public ActionResult<Sponsor> Post([FromBody] Sponsor sponsor)
        {
            try
            {
                return _sponsorService.CreateSponsor(sponsor);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Sponsor> Put(int id, [FromBody] Sponsor sponsor)
        {
            try
            {
                return Ok(_sponsorService.UpdateSponsor(sponsor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Sponsor> Delete(int id)
        {
            var review = _sponsorService.DeleteSponsor(id);
            if (review == null)
            {
                return StatusCode(404, "Could not delete sponsor if the sponsor doesn't exist");
            }
            try
            {
                return review;
            }
            catch (Exception)
            {
                return StatusCode(500, "Could not find sponsor");
            }
        }

    }
}
