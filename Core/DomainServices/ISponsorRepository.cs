using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Core.DomainServices
{
    public interface ISponsorRepository<T>
    {
        IEnumerable<Sponsor> GetAllSponsors();
        Sponsor CreateSponsor(Sponsor sponsor);
        Sponsor GetSponsorById(int id);
        Sponsor UpdateSponsor(Sponsor sponsor);
        Sponsor DeleteSponsor(int id);
    }
}
