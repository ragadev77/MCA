using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


using MCA.DBClass;
using MCA.Models;
using MCA.Repositories;
using System.Linq;

namespace MCA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleFinalController : ControllerBase
    {
        private readonly IRuleFinalRepository _repository;
        private readonly IParameterVersionRepository _parameterVersionRepository;

        private const string _MODULE = "rule";
        private readonly string[] _status = { "created", "checked", "approved" };

        public RuleFinalController(IRuleFinalRepository ruleFinalRepository, IParameterVersionRepository parameterVersionRepository)
        {
            _repository = ruleFinalRepository;
            _parameterVersionRepository = parameterVersionRepository;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> ListRules()
        {
            var data = await _repository.List();
            if (data.Any())
                return Ok(data);
            //APIResult retVal = data.Any() ? APIResult.ResponseAPI(true, APIResult.Level.READ, data) : APIResult.ResponseAPI(false, APIResult.Level.READ_FAILED, data);

            return Ok(APIResult.ResponseAPI(false, APIResult.Level.READ_FAILED));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<RuleFinal>> GetById(int id)
        {
            var data =  _repository.Get(id);
            APIResult retVal = data != null ? APIResult.ResponseAPI(true, APIResult.Level.READ, data) : APIResult.ResponseAPI(true, APIResult.Level.READ_FAILED, data);

            return Ok(retVal);

        }

        /****
         *  after insert rule 1st time
         * > new parameterVersion
         * 	> prv_module : 'rule'
         *  > prv_version : from json
         *  > prv_date : now
         *  > prv_status : created
         *  > prv_unique_parameter : from rul_id
         *  > prv_sync_plan : null
         *  > prv_headerid = from rul_id
        ****/


        //[HttpPost]
        //[Route("Create")]
        //public async Task<ActionResult<RuleFinal>> Create(string role, [FromBody] RuleFinal jsonData, string sync_date, int rul_id)
        //{
        //    Parameter_Version prm_version = null;
        //    RuleFinal rule = null;
        //    APIResult result = null;
        //    try
        //    {
        //        if (role != "maker")
        //        {
        //            /* search by headerId */
        //            Parameter_Version previous_prm_version = _parameterVersionRepository.GetByHeaderId(rul_id);
        //            if (previous_prm_version != null)
        //            {
        //                /* get existing rule */
        //                rule = _repository.Get(rul_id);
        //                prm_version = previous_prm_version;
        //                switch (role)
        //                {
        //                    case ("checker"):
        //                        prm_version.prv_status = _status[1];
        //                        rule.rul_approved_status = _status[1];
        //                        rule.rul_approved_by = rule.rul_created_by;
        //                        break;
        //                    case ("approval"):
        //                        DateTime localDateTime, univDateTime;
        //                        localDateTime = DateTime.Parse(sync_date);
        //                        univDateTime = localDateTime.ToUniversalTime();
        //                        prm_version.prv_status = _status[2];
        //                        prm_version.prv_sync_plan = univDateTime;
        //                        rule.rul_approved_status = _status[2];
        //                        rule.rul_approved_by = rule.rul_created_by;
        //                        rule.rul_is_active = true;
        //                        break;
        //                }
        //                /* update rule and parameter_version */
        //                await _repository.Update(rule);
        //                await _parameterVersionRepository.Update(prm_version);
        //            }

        //        }
        //        else {
        //            /* maker logic / first create */
        //            jsonData.rul_approved_status = _status[0];
        //            rule = await _repository.Create(jsonData);
        //            //APIResult retVal = data != null ? APIResult.ResponseAPI(true, APIResult.Level.CREATE, data) : APIResult.ResponseAPI(false, APIResult.Level.CREATE_FAILED, data);
        //            if (rule != null)
        //            {
        //                /* prepare parameter_version data */
        //                Parameter_Version pv = new Parameter_Version()
        //                {
        //                    prv_module = _MODULE,
        //                    prv_version = rule.rul_version,
        //                    prv_date = DateTime.Now,
        //                    prv_status = _status[0],
        //                    prv_unique_parameter = rule.rul_id,
        //                    prv_sync_plan = null,
        //                    prv_headerid = rule.rul_id
        //                };
        //                prm_version = _parameterVersionRepository.Create(pv);
        //            }
        //        }

        //        /* combine rule and paramever_version data */ 
        //        var join_data = new
        //        {
        //            rule = rule,
        //            parameter_version = prm_version
        //        };
        //        //Dictionary<string, object> dictionary_rule = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(rule));
        //        //Dictionary<string, object> dictionary_parameter_version = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(prm_version));

        //        result = APIResult.ResponseAPI(true, APIResult.Level.CREATE);
        //        result.Data = join_data;


        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.WriteLine(ex.InnerException);
        //        return Ok(APIResult.ResponseAPI(false, APIResult.Level.CREATE_FAILED));
        //    }
            
        //}

        //[HttpPut]
        //public async Task<ActionResult<RuleFinal>> Update(int id, [FromBody] RuleFinal jsonData)
        //{
        //    //if (id != jsonData.prv_id)
        //    //    return BadRequest();
        //    //await _repository.Update(jsonData);
        //    //return NoContent();            
        //    APIResult retVal;
        //    if (id != jsonData.rul_id) {
        //        retVal = APIResult.ResponseAPI(true, APIResult.Level.UPDATE_FAILED, jsonData);
        //    }
        //    else {
        //        await _repository.Update(jsonData);
        //        retVal = APIResult.ResponseAPI(true, APIResult.Level.UPDATE, jsonData);
        //    }
        //    return Ok(retVal);

        //}

        //[HttpDelete]
        //public async Task<ActionResult<RuleFinal>> Delete(int id)
        //{
        //    var del = _repository.Get(id);
        //    APIResult retVal;
        //    string message = string.Format("data {0} deleted", id);
        //    if (del != null){
        //        await _repository.Delete(id);
        //        retVal = APIResult.ResponseAPI(true, APIResult.Level.DELETE, del, message);
        //    } else  {
        //        retVal = APIResult.ResponseAPI(true, APIResult.Level.DELETE_FAILED, del, message);
        //    }

        //    return Ok(retVal);
        //}




    }
}
