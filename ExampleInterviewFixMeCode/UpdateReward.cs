using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using ExampleInterviewFixMeCode.Models;
using ExampleInterviewFixMeCode.Data;
using System.Net;

namespace ExampleInterviewFixMeCode
{
    public static class UpdateReward
    {
        //Instanciated Reward Data Adapter
        public static RewardDataAdapter _RewardDataAdapter = new RewardDataAdapter();

        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> UpdateRewardDetails(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            HttpResponseMessage response = null;

            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            //Get Reward Post Body
            Reward data = (Reward)JsonConvert.DeserializeObject<Reward>(requestBody);

            //Validate Fields
            string responseMessage = data.isValid ? null : "The Post Body had some bad values!";

            //If Model Valid Lets Save the Reward Details
            if (responseMessage == null)
                _RewardDataAdapter.SaveReward(data);
            else //Otherwise lets setup error response
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent(responseMessage);
                return response;
            }

            //Looks like we made it through our validation and saving
            response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent("Reward Details were Saved!");
            return response;
        }
    }
}
