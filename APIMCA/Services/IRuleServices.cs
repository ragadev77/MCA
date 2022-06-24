using System.Threading.Tasks;
using MCA.DBClass;
using MCA.Repositories;
using MCA.Models;

namespace MCA.Services
{
    public interface IRuleServices
    {
        public APIResult MakerNew(IRuleRepository _ruleRepository, IParameterVersionRepository _parameterVersionRepository, Rule jsonData, string version);

        public APIResult MakerUpdate(IRuleRepository _repository, IParameterVersionRepository _parameterVersionRepository, Rule jsonData, int rule_id);

        public Task<APIResult> CheckerNew(IRuleRepository _ruleRepository, IParameterVersionRepository _parameterVersionRepository
            , int rule_id);

        public Task<APIResult> CheckerUpdate(IRuleRepository _ruleRepository, IParameterVersionRepository _parameterVersionRepository
           , int rule_id, bool approved);
        public Task<APIResult> ApprovalNew(IRuleRepository _ruleRepository
            , IRuleFinalRepository _ruleFinalRepository
            , IParameterVersionRepository _parameterVersionRepository
            , int rule_id, string sync_date, bool isSynced = false);
    }
}
