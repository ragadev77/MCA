using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using MCA.Models;

namespace MCA.Repositories
{
    public interface IRuleRepository
    {
        Task<IEnumerable<Rule>> List();
        Task<Rule> Get(int id);
        Rule Create(Rule rule);
        Task Update(Rule rule);
        Task Delete(int id);

        string GetVersion(int id);

    }
}
