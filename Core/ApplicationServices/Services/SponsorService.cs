using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return _sponsorRepo.UpdateSponsor(updateSponsor);
        }

        public Sponsor FindSponsorById(int id)
        {
            return _sponsorRepo.ReadSponsorById(id);
        }

        public Sponsor DeleteSponsor(int id)
        {
            return _sponsorRepo.DeleteSponsor(id);
        }

        public Sponsor CreateSponsor(Sponsor sponsor)
        {
            return _sponsorRepo.CreateSponsor(sponsor);
        }

        public List<Sponsor> GetAllSponsors()
        {
            return _sponsorRepo.ReadAllSponsors().ToList();
        }
    }
}
