using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using MCA.Models;

namespace MCA.Repositories
{
    public interface IParameterVersionRepository
    {
        Task<IEnumerable<Parameter_Version>> List();
        Task<Parameter_Version> Get(int id);
        Parameter_Version GetByHeaderId(int id);
        Parameter_Version Create(Parameter_Version prmVersion);
        Task Update(Parameter_Version prmVersion);
        Task Delete(int id);
    }
}
