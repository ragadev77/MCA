using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MCA.DBClass
{
    public class APIResult
    {
        public enum Level
        {
            CREATE,
            READ,
            UPDATE,
            DELETE,
            CREATE_FAILED,
            READ_FAILED,
            UPDATE_FAILED,
            DELETE_FAILED,
            REJECT
        }

        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static APIResult ResponseAPI(bool success, APIResult.Level apiType, Object objData = null, string customMessage = "custom message")
        {
            var json = JsonConvert.SerializeObject(objData);
            JObject settings = JObject.Parse(File.ReadAllText(@"apimessage.json"));
            
            var dictionary = objData != null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(json) : null;
            string resultStatus = success == true ? settings.GetValue("api_output_ok").ToString() : settings.GetValue("api_output_not_ok").ToString();

            string resultMessage = "default";
            switch(apiType)
            {
                case APIResult.Level.CREATE:
                    resultMessage = settings.GetValue("save_success").ToString();
                    break;
                case APIResult.Level.READ:
                    resultMessage = settings.GetValue("data_found").ToString();
                    break;
                case APIResult.Level.UPDATE:
                    resultMessage = settings.GetValue("update_success").ToString();
                    break;
                case APIResult.Level.DELETE:
                    resultMessage = settings.GetValue("delete_success").ToString();
                    break;
                case APIResult.Level.CREATE_FAILED:
                    resultMessage = settings.GetValue("save_not_success").ToString();
                    break;
                case APIResult.Level.READ_FAILED:
                    resultMessage = settings.GetValue("data_not_found").ToString();
                    break;
                case APIResult.Level.UPDATE_FAILED:
                    resultMessage = settings.GetValue("update_not_success").ToString();
                    break;
                case APIResult.Level.DELETE_FAILED:
                    resultMessage = settings.GetValue("delete_not_success").ToString();
                    break;
                case APIResult.Level.REJECT:
                    resultMessage = settings.GetValue("workflow_reject").ToString();
                    break;
                default:
                    resultMessage = "no value given";
                    break;
            }

            APIResult result = new APIResult()
            {
                Status = resultStatus,
                Message = resultMessage,
                Data = dictionary
            };

            return result;
        }

        //private void GetJsonMessage()
        //{
        //    JObject o1 = JObject.Parse(File.ReadAllText(@"apimessage.json"));

        //    // read JSON directly from a file
        //    using (StreamReader file = File.OpenText(@"apimessage.json"))
        //    using (JsonTextReader reader = new JsonTextReader(file))
        //    {
        //        JObject o2 = (JObject)JToken.ReadFrom(reader);                
        //    }
        //}
    }
}
