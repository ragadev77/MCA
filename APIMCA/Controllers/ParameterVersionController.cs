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
    public class ParameterVersionController : ControllerBase
    {
        private readonly IParameterVersionRepository _repository;

        public ParameterVersionController(IParameterVersionRepository parameterVersionRepository)
        {
            _repository = parameterVersionRepository;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> ListParameterVersions()
        {
            var data = await _repository.List();
            if (data.Any())
            {
                return Ok(data);
            }
            return NotFound();
            //return await _repository.List();
        }


        [HttpGet("Get/{id}")]

        public async Task<ActionResult<Parameter_Version>> GetById(int id)
        {
            //return await _repository.Get(id);
            var data = await _repository.Get(id);
            APIResult retVal = data != null ? APIResult.ResponseAPI(true, APIResult.Level.READ, data) : APIResult.ResponseAPI(false, APIResult.Level.READ_FAILED, data);

            return Ok(retVal);

        }

/* Closed 
        [HttpPost]
        public ActionResult<Parameter_Version> Create([FromBody] Parameter_Version jsonData)
        {
            var data = _repository.Create(jsonData);
            //return CreatedAtAction(nameof(ListParameterVersions), new { d = data.prv_id }, data);
            APIResult retVal = data != null ? APIResult.ResponseAPI(true, APIResult.Level.CREATE, data) : APIResult.ResponseAPI(false, APIResult.Level.CREATE_FAILED, data);
            return Ok(retVal);
        }

        [HttpPut]
        public async Task<ActionResult<Parameter_Version>> Update(int id, [FromBody] Parameter_Version jsonData)
        {
            //if (id != jsonData.prv_id)
            //    return BadRequest();
            //await _repository.Update(jsonData);
            //return NoContent();            
            APIResult retVal;
            if (id != jsonData.prv_id) {
                retVal = APIResult.ResponseAPI(false, APIResult.Level.UPDATE_FAILED, jsonData);
            }
            else {
                await _repository.Update(jsonData);
                retVal = APIResult.ResponseAPI(true, APIResult.Level.UPDATE, jsonData);
            }
            return Ok(retVal);

        }

        [HttpDelete]
        public async Task<ActionResult<Parameter_Version>> Delete(int id)
        {
            var del = await _repository.Get(id);
            APIResult retVal;
            string message = string.Format("data {0} deleted", id);
            if (del != null){
                await _repository.Delete(id);
                retVal = APIResult.ResponseAPI(true, APIResult.Level.DELETE, del, message);
            } else  {
                retVal = APIResult.ResponseAPI(false, APIResult.Level.DELETE_FAILED, del, message);
            }

            return Ok(retVal);
        }

*/


    }
}
