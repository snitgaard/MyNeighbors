using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Core.DomainServices
{
    public interface ISponsorRepository
    {
        IEnumerable<Sponsor> ReadAllSponsors();
        Sponsor CreateSponsor(Sponsor sponsor);
        Sponsor ReadSponsorById(int id);
        Sponsor UpdateSponsor(Sponsor sponsor);
        Sponsor DeleteSponsor(int id);
    }
}
