using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Core.ApplicationServices
{
    public interface ISponsorService
    {
        List<Sponsor> GetAllSponsors();
        Sponsor UpdateSponsor(Sponsor updateSponsor);
        Sponsor FindSponsorById(int id);
        Sponsor DeleteSponsor(int id);
        Sponsor CreateSponsor(Sponsor sponsor);
    }
}
