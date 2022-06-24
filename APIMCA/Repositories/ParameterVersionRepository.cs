using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json;

using MCA.Models;

namespace MCA.Repositories
{
    public class ParameterVersionRepository : IParameterVersionRepository
    {
        private readonly AppDbContext _context;

        public ParameterVersionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Parameter_Version> Get(int id)
        {
            return await _context.ParameterVersions.FindAsync(id);
        }
        public Parameter_Version GetByHeaderId(int headerId)
        {
            var result = (from b in _context.ParameterVersions
                         where b.prv_headerid == headerId
                         select b).First();
            return result; 
        }
        public async Task<IEnumerable<Parameter_Version>> List()
        {
            return await _context.ParameterVersions.ToListAsync();
        }
        /****
         *  after insert rule 1st time
         * > new parameterVersion
         * 	> prv_module : 'rule'
         *  > prv_version : new
         *  > prv_date : now
         *  > prv_status : created
         *  > prv_unique_parameter : from rul_id
         *  > prv_sync_plan : null
         *  > prv_headerid = from rul_id
        ****/
        public Parameter_Version Create(Parameter_Version prmVersion)
        {
            _context.ParameterVersions.Add(prmVersion);
            string sql = string.Format("select * from ins_parameter_version(" +
                "'{0}', '{1}', '{2}', {3}, {4}, '{5}', '{6}');"
                , prmVersion.prv_module, prmVersion.prv_version, prmVersion.prv_status
                , prmVersion.prv_unique_parameter,prmVersion.prv_headerid
                , prmVersion.prv_date, prmVersion.prv_sync_plan);

            //string sql = "EXEC public.ins_parameter_version @ProductID";
            //List<SqlParameter> parms = new List<SqlParameter>
            //{
            //    // Create parameter(s)    
            //    new SqlParameter { ParameterName = "@ProductID", Value = 706 }
            //};
            var newid = _context.ParameterVersions.FromSqlRaw<Parameter_Version>(sql);            
            _context.SaveChanges();
            return prmVersion; 
        }

        public async Task Delete(int id)
        {
            var delData = await _context.ParameterVersions.FindAsync(id);
            _context.ParameterVersions.Remove(delData);
            await _context.SaveChangesAsync();
            //throw new System.NotImplementedException();
        }

        public async Task Update(Parameter_Version prmVersion)
        {
            _context.Entry(prmVersion).State = EntityState.Modified;
            await _context.SaveChangesAsync();            
        }

    }
}
