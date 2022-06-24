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
using MCA.Services;
using System.Linq;

namespace MCA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly IRuleRepository _repository;
        private readonly IRuleFinalRepository _finalRepository;
        private readonly IParameterVersionRepository _parameterVersionRepository;

        private readonly IRuleServices _ruleServices;

        public RuleController(IRuleServices ruleService, IRuleRepository ruleRepository, IParameterVersionRepository parameterVersionRepository, IRuleFinalRepository ruleFinalRepository)
        {
            _ruleServices = ruleService;

            _repository = ruleRepository;
            _finalRepository = ruleFinalRepository;
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


        [HttpGet("Get/{id}")]
        public async Task<ActionResult<Rule>> GetById(int id)
        {
            var data = _repository.Get(id);
            APIResult retVal = data != null ? APIResult.ResponseAPI(true, APIResult.Level.READ, data) : APIResult.ResponseAPI(true, APIResult.Level.READ_FAILED, data);

            return Ok(retVal);

        }

        async Task<bool> syncUpdate(int ruleId)
        {
            bool retVal = false;
            Rule rule = await _repository.Get(ruleId);

            return retVal;
        }

        [HttpPost]
        [Route("Maker/New")]
        public ActionResult<Rule> MakerNew([FromBody] Rule jsonData)
        {
            string ver = "001";
            APIResult result = _ruleServices.MakerNew(_repository, _parameterVersionRepository, jsonData, ver);

            return Ok(result);
        }

        [HttpPost]
        [Route("Maker/Update/{rule_id}")]
        public ActionResult<Rule> MakerUpdate([FromBody] Rule jsonData, int rule_id)
        {

            APIResult result = _ruleServices.MakerUpdate(_repository, _parameterVersionRepository, jsonData, rule_id);

            return Ok(result);
        }

        [HttpPost]
        [Route("Checker/{rule_id}")]
        public async Task<ActionResult<Rule>> CheckerCreate(int rule_id, bool approved = false)
        {
            APIResult result = null;
            if (approved==true) 
                result = await _ruleServices.CheckerNew(_repository, _parameterVersionRepository, rule_id);
            else
                result = APIResult.ResponseAPI(true, APIResult.Level.REJECT);

            return Ok(result);
        }

        [HttpPost]
        [Route("Checker/Update/{rule_id}")]
        public async Task<ActionResult<Rule>> CheckerUpdate(int rule_id, bool approved)
        {
            APIResult result = null;
            if (approved == true)
                result = await _ruleServices.CheckerUpdate(_repository, _parameterVersionRepository, rule_id, approved);
            else
                result = APIResult.ResponseAPI(true, APIResult.Level.REJECT);

            return Ok(result);
        }

        [HttpPost]
        [Route("Approval")]
        public async Task<ActionResult<Rule>> ApprovalCreate(int rule_id, bool approved, string sync_date, bool isSynced)
        {
            APIResult result = null;
            if (approved == true)
                result = await _ruleServices.ApprovalNew(_repository, _finalRepository, _parameterVersionRepository, rule_id, sync_date, isSynced) ;
            else
                result = APIResult.ResponseAPI(true, APIResult.Level.REJECT);
            return Ok(result);
        }

        /**** unused ****/
        /*
        [HttpPost]
        [Route("Approval/Update")]
        public async Task<ActionResult<Rule>> ApprovalUpdate(string role, [FromBody] Rule jsonData, string sync_date = null, int rul_id = 0, bool approved = false, bool isSynced = false, string flag = null)
        {
            APIResult result = null;
            if (approved == true)
                result = await _ruleServices.ApprovalNew(_repository, _finalRepository, _parameterVersionRepository, rule_id, sync_date, isSynced);
            else
                result = APIResult.ResponseAPI(true, APIResult.Level.REJECT);
            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult<Rule>> Update(int id, [FromBody] Rule jsonData)
        {
            //if (id != jsonData.prv_id)
            //    return BadRequest();
            //await _repository.Update(jsonData);
            //return NoContent();            
            APIResult retVal;
            if (id != jsonData.rul_id)
            {
                retVal = APIResult.ResponseAPI(true, APIResult.Level.UPDATE_FAILED, jsonData);
            }
            else
            {
                await _repository.Update(jsonData);
                retVal = APIResult.ResponseAPI(true, APIResult.Level.UPDATE, jsonData);
            }
            return Ok(retVal);

        }

        [HttpDelete]
        [Route("Delete")]

        public async Task<ActionResult<Rule>> Delete(int id)
        {
            var del = _repository.Get(id);
            APIResult retVal;
            string message = string.Format("data {0} deleted", id);
            if (del != null)
            {
                await _repository.Delete(id);
                retVal = APIResult.ResponseAPI(true, APIResult.Level.DELETE, del, message);
            }
            else
            {
                retVal = APIResult.ResponseAPI(true, APIResult.Level.DELETE_FAILED, del, message);
            }

            return Ok(retVal);
        }

        */
    }
}
