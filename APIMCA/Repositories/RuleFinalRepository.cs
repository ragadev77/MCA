using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Newtonsoft.Json;

using MCA.Models;

namespace MCA.Repositories
{
    public class RuleFinalRepository : IRuleFinalRepository
    {
        private readonly AppDbContext _context;

        public RuleFinalRepository(AppDbContext context)
        {
            _context = context;
        }
        public RuleFinal Get(int id)
        {
            return _context.RuleFinals.Find(id);
        }

        public async Task<IEnumerable<RuleFinal>> List()
        {
            return await _context.RuleFinals.ToListAsync();
        }
        public async Task<RuleFinal> Create(RuleFinal ruleFinal)
        {
            _context.RuleFinals.Add(ruleFinal);
            await _context.SaveChangesAsync();
            return ruleFinal; 
        }

        public async Task Delete(int id)
        {
            var delData = await _context.RuleFinals.FindAsync(id);
            _context.RuleFinals.Remove(delData);
            await _context.SaveChangesAsync();
        }

        public async Task Update(RuleFinal prmVersion)
        {
            _context.Entry(prmVersion).State = EntityState.Modified;
            await _context.SaveChangesAsync();            
        }

        public RuleFinal GetByIdOri(int idOri)
        {
            var result = (from b in _context.RuleFinals
                          where b.rul_id_ori == idOri
                          select b).First();
            return result;

        }
    }
}
