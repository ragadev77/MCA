using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using MCA.Models;

namespace MCA.Repositories
{
    public interface IRuleFinalRepository
    {
        Task<IEnumerable<RuleFinal>> List();
        RuleFinal Get(int id);
        RuleFinal GetByIdOri(int idOri);
        Task<RuleFinal> Create(RuleFinal rule);
        Task Update(RuleFinal rule);
        Task Delete(int id);
    }
}
