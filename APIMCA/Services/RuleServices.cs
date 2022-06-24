
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using MCA.Models;
using MCA.Repositories;
using MCA.DBClass;

namespace MCA.Services
{
    public class RuleServices : IRuleServices
    {

        DateTime localDateTime, univDateTime;

        public APIResult MakerNew(IRuleRepository _ruleRepository, IParameterVersionRepository _parameterVersionRepository
            , Rule jsonData, string version)
        {
            Parameter_Version prm_version = null;
            Rule rule = null;
            APIResult result = null;
            try
            {
                jsonData.rul_approved_status = Constant.CREATED;
                jsonData.rul_version = Constant.SYNC_VERSION + "." + version;
                rule = _ruleRepository.Create(jsonData);

                /* prepare parameter_version data */
                Parameter_Version parameter_Version = new Parameter_Version()
                {
                    prv_module = Constant.RULE,
                    prv_version = Constant.SYNC_VERSION + "." + version,
                    prv_date = DateTime.Now,
                    prv_status = Constant.CREATED,
                    prv_unique_parameter = rule.rul_id,
                    prv_sync_plan = null,
                    prv_headerid = rule.rul_id
                };
                prm_version = _parameterVersionRepository.Create(parameter_Version);

                var join_data = new
                {
                    rule = rule,
                    parameter_version = prm_version
                };
                result = APIResult.ResponseAPI(true, APIResult.Level.CREATE);
                result.Data = join_data;

            }
            catch (Exception ex)
            {
                Console.WriteLine("SERVICE.RuleMaker : " + ex.Message);
                APIResult.ResponseAPI(true, APIResult.Level.CREATE_FAILED);
            }

            return result;

        }

        public APIResult MakerUpdate(IRuleRepository _repository, IParameterVersionRepository _parameterVersionRepository, Rule jsonData, int rule_id)
        {
            Parameter_Version prm_version = null;
            Rule rule = null;
            APIResult result = null;
            try
            {
                /* update = create new rule and parameter_version */
                string newVersion = _repository.GetVersion(rule_id);

                jsonData.rul_approved_status = Constant.UPDATE;
                jsonData.rul_version = Constant.getNewVersion(newVersion);
                jsonData.rul_modified = DateTime.UtcNow.ToString();
                jsonData.rul_modified_by = jsonData.rul_created_by;
                jsonData.rul_id_ori = rule_id;
                rule = _repository.Create(jsonData);
                if (rule != null)
                {
                    /* prepare parameter_version data */
                    Parameter_Version pv = new Parameter_Version()
                    {
                        prv_module = Constant.RULE,
                        prv_version = rule.rul_version,
                        prv_date = DateTime.Now,
                        prv_status = Constant.UPDATE,
                        prv_unique_parameter = rule.rul_id_ori,
                        prv_sync_plan = null,
                        prv_headerid = rule.rul_id
                    };
                    prm_version = _parameterVersionRepository.Create(pv);
                }

                var join_data = new
                {
                    rule = rule,
                    parameter_version = prm_version
                };
                result = APIResult.ResponseAPI(true, APIResult.Level.CREATE);
                result.Data = join_data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SERVICE.MakerUpdate : " + ex.Message);
                APIResult.ResponseAPI(true, APIResult.Level.CREATE_FAILED);

            }
            return result;
        }
        public async Task<APIResult> CheckerNew(IRuleRepository _ruleRepository, IParameterVersionRepository _parameterVersionRepository
            , int rule_id)
        {
            APIResult result = null;
            Parameter_Version prm_version = null;
            Rule rule = null;
            try
            {
                Parameter_Version previous_prm_version = _parameterVersionRepository.GetByHeaderId(rule_id);
                if (previous_prm_version != null)
                {
                    /* get existing rule */
                    rule = await _ruleRepository.Get(rule_id);
                    prm_version = previous_prm_version;
                    prm_version.prv_status = Constant.CHECKED;
                    rule.rul_approved_status = Constant.CHECKED;
                    rule.rul_approved_by = rule.rul_created_by;
                    /* update rule and parameter_version */
                    await _ruleRepository.Update(rule);
                    await _parameterVersionRepository.Update(prm_version);
                }
                var join_data = new
                {
                    rule = rule,
                    parameter_version = prm_version
                };
                result = APIResult.ResponseAPI(true, APIResult.Level.UPDATE);
                result.Data = join_data;

            }
            catch (Exception ex)
            {
                Console.WriteLine("SERVICE.CheckerNew : " + ex.Message);
                APIResult.ResponseAPI(true, APIResult.Level.UPDATE_FAILED);
            }

            return result;


        }

        public async Task<APIResult> CheckerUpdate(IRuleRepository _ruleRepository, IParameterVersionRepository _parameterVersionRepository
            , int rule_id, bool approved)
        {
            APIResult result = null;
            Rule rule = null;
            /* search by headerId */
            Parameter_Version prm_version = _parameterVersionRepository.GetByHeaderId(rule_id);
            if (prm_version != null)
            {
                /* get existing rule */
                rule = await _ruleRepository.Get(rule_id);
                if (approved == true)
                {
                    prm_version.prv_status = Constant.CHECKED;
                    rule.rul_approved_status = Constant.CHECKED;
                }

                /* update rule and parameter_version */
                await _ruleRepository.Update(rule);
                await _parameterVersionRepository.Update(prm_version);
            }

            var join_data = new
            {
                rule = rule,
                parameter_version = prm_version
            };
            result = APIResult.ResponseAPI(true, APIResult.Level.UPDATE);
            result.Data = join_data;

            return result;

        }

        public async Task<APIResult> ApprovalNew(IRuleRepository _ruleRepository
            , IRuleFinalRepository _ruleFinalRepository
            , IParameterVersionRepository _parameterVersionRepository
            , int rule_id, string sync_date, bool isSynced = false)
        {
            Parameter_Version prm_version = null;
            Rule rule = null;
            RuleFinal rule_final = null;
            APIResult result = null;
            localDateTime = DateTime.Parse(sync_date);
            univDateTime = localDateTime.ToUniversalTime();


            prm_version = _parameterVersionRepository.GetByHeaderId(rule_id);
            rule = await _ruleRepository.Get(rule_id);

            prm_version.prv_status = isSynced == true ? Constant.SYNCED : Constant.APPROVED;
            prm_version.prv_sync_plan = isSynced == true ? prm_version.prv_sync_plan : univDateTime;
            rule.rul_approved_status = isSynced == true ? Constant.SYNCED : Constant.APPROVED;
            rule.rul_approved_by = rule.rul_created_by;
            rule.rul_is_active = true;
            if (isSynced == true)
            {
                rule_final = _ruleFinalRepository.GetByIdOri(rule_id);
                rule_final.rul_approved_status = Constant.SYNCED;
                await _ruleFinalRepository.Update(rule_final);
            }
            else
            {
                rule_final = new RuleFinal()
                {
                    rul_name = rule.rul_name,
                    rul_desc = rule.rul_desc,
                    rul_condition = rule.rul_condition,
                    rul_output = rule.rul_output,
                    rul_is_active = true,
                    rul_created_by = rule.rul_created_by,
                    rul_is_deleted = false,
                    rul_is_used = false,
                    rul_created = DateTime.UtcNow,
                    rul_approved_status = isSynced == true ? Constant.SYNCED : Constant.APPROVED,
                    rul_approved_by = rule.rul_approved_by,
                    rul_id_ori = rule.rul_id,
                    rul_applied = rule.rul_applied,
                    rul_category = rule.rul_category,
                    rul_version = rule.rul_version
                };
                await _ruleFinalRepository.Create(rule_final);
            }
            var join_data = new
            {
                rule = rule,
                rule_final = rule_final,
                parameter_version = prm_version
            };
            result = APIResult.ResponseAPI(true, APIResult.Level.UPDATE);
            result.Data = join_data;

            return result;
        }


    }
}
