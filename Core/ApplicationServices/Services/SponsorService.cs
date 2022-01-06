using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using MyNeighbors.Core.DomainServices;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Core.ApplicationServices.Services
{
    public class SponsorService: ISponsorService
    {
        private readonly ISponsorRepository<Sponsor> _sponsorRepo;

        public SponsorService(ISponsorRepository<Sponsor> sponsorRepository)
        {
            _sponsorRepo = sponsorRepository;
        }

        public Sponsor UpdateSponsor(Sponsor updateSponsor)
        {
            if (updateSponsor == null)
            {
                throw new ArgumentException("Sponsor is missing");
            }
            if (!IsValidSponsor(updateSponsor))
            {
                throw new ArgumentException("Invalid sponsor property");
            }

            if (_sponsorRepo.GetSponsorById(updateSponsor.Id) != null)
            {
                throw new InvalidOperationException("This Sponsor already exists");
            }
            if (_sponsorRepo.GetSponsorById(updateSponsor.Id) == null)
            {
                throw new InvalidOperationException("Sponsor does not exist");
            }
            return _sponsorRepo.UpdateSponsor(updateSponsor);
        }

        public Sponsor GetSponsorById(int id)
        {
            return _sponsorRepo.GetSponsorById(id);
        }

        public Sponsor DeleteSponsor(int id)
        {
            if (_sponsorRepo.GetSponsorById(id) == null)
            {
                throw new InvalidOperationException("Cannot remove sponsor that does not exist");
            }
            return _sponsorRepo.DeleteSponsor(id);
        }

        public Sponsor CreateSponsor(Sponsor sponsor)
        {
            if (sponsor == null)
            {
                throw new ArgumentException("Sponsor is missing");
            }
            if (!IsValidSponsor(sponsor))
            {
                throw new ArgumentException("Invalid sponsor property");
            }
            if (_sponsorRepo.GetSponsorById(sponsor.Id) != null)
            {
                throw new InvalidOperationException("This Sponsor already exists");
            }

            return _sponsorRepo.CreateSponsor(sponsor);
        }

        public List<Sponsor> GetAllSponsors()
        {
            return _sponsorRepo.GetAllSponsors().ToList();
        }

        private bool IsValidSponsor(Sponsor sponsor)
        {
            return (!sponsor.Name.IsNullOrEmpty() 
                    && !sponsor.Image.IsNullOrEmpty()
                    && sponsor.X_coordinate <= 180
                    && sponsor.Y_coordinate <= 90
                    && sponsor.X_coordinate >= -180
                    && sponsor.Y_coordinate >= -90
                    && !sponsor.Type.IsNullOrEmpty());
        }
    }                    
}
