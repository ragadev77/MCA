using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using MCA.Models;

namespace MCA.Repositories
{
    public class RuleRepository : IRuleRepository
    {
        private readonly AppDbContext _context;

        public RuleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Rule> Get(int id)
        {
            return await _context.Rules.FindAsync(id);           
        }
        public async Task<IEnumerable<Rule>> List()
        {
            return await _context.Rules.ToListAsync();
        }
        public Rule Create(Rule rule)
        {
            _context.Rules.Add(rule);
            _context.SaveChanges();
            return rule; 
        }

        public async Task Delete(int id)
        {
            var delData = await _context.Rules.FindAsync(id);
            _context.Rules.Remove(delData);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Rule prmVersion)
        {
            _context.Entry(prmVersion).State = EntityState.Modified;
            await _context.SaveChangesAsync();            
        }
        public string GetVersion(int id)
        {
            return _context.Rules.Find(id).rul_version;
        }



    }
}
