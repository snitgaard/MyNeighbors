using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyNeighbors.Core.DomainServices;
using MyNeighbors.Core.Entity;

namespace MyNeighbors.Infrastructure.Repositories
{
    public class SponsorRepository: ISponsorRepository<Sponsor>
    {
        private MyNeighborsContext _ctx;
        public SponsorRepository(MyNeighborsContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Sponsor> GetAllSponsors()
        {
            return _ctx.Sponsor.AsNoTracking();
        }

        public Sponsor CreateSponsor(Sponsor sponsor)
        {
            _ctx.DetachAll();
            _ctx.Attach(sponsor).State = EntityState.Added;
            _ctx.SaveChanges();
            return sponsor;
        }

        public Sponsor GetSponsorById(int id)
        {
            return _ctx.Sponsor.AsNoTracking().FirstOrDefault(s => s.Id == id);
        }

        public Sponsor UpdateSponsor(Sponsor sponsor)
        {
            _ctx.DetachAll();
            _ctx.Attach(sponsor).State = EntityState.Modified;
            _ctx.SaveChanges();
            return sponsor;
        }

        public Sponsor DeleteSponsor(int id)
        {
            var sponsor = _ctx.Sponsor.FirstOrDefault(s => s.Id == id);
            var removedSponsor = _ctx.Remove(sponsor).Entity;
            _ctx.SaveChanges();
            return removedSponsor;
        }
    }
}
