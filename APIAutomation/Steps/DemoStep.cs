using TechTalk.SpecFlow;
using RestSharp;
using FluentAssertions;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;

namespace APIAutomation.Steps
{
    [Binding]
    public class DemoStep
    {

        private RestClient restClient = new RestClient("https://jsonplaceholder.typicode.com/");
        private RestRequest restRequest;
        private RestResponse restResponse;
        private static Random random = new Random();

        [When(@"I send a GET Users request")]
        public void ISendAGETUsersRequest()
        {
            restRequest = new RestRequest("users", Method.Get);
            restResponse = restClient.Execute(restRequest);
        }

        [When(@"I send a GET Users request by Id (\d*)")]
        public void ISendAGETUsersRequestById(int id)
        {
            restRequest = new RestRequest("users/"+id, Method.Get);
            restResponse = restClient.Execute(restRequest);
        }

        [When(@"I send a POST Users request by (\d*)")]
        [Obsolete]
        public void ISendAPostUsersRequest(int userid)
        {
            restRequest = new RestRequest("posts", Method.Post);
            restRequest.AddHeader("Content-type", "application/json; charset=UTF-8");
            string titleString = RandomString(8);
            string bodyString = RandomString(20);
            ScenarioContext.Current.Add("title", titleString);
            ScenarioContext.Current.Add("body", bodyString);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddJsonBody(new { title = titleString, body = bodyString, userId  = userid });
            restResponse = restClient.Execute(restRequest);
        }

        [Then(@"Verify 200 OK message is returned")]
        public void VerfiyOKMessageIsReturned()
        {
           
            HttpStatusCode status = restResponse.StatusCode;
            status.ToString().Should().Be("OK");
            int numericStatusCode = (int)status;
            numericStatusCode.Should().Be(200);
        }

        [Then(@"Verify that there are (.*) users in the results")]
        public void VerifyTheNumberOfUsersInTheResults(int num)
        {
            JArray jObj = (JArray)JsonConvert.DeserializeObject(restResponse.Content);
            jObj.Count.Should().Be(10);
        }

        [Then(@"Verify if user with id (\d*) is (.*)")]
        public void VerifyTheUserNameById(int id, string name)
        {
            JObject jObj = (JObject)JsonConvert.DeserializeObject(restResponse.Content);
            var resultId = (int)jObj["id"];
            var resultName = (string)jObj["name"];
            resultId.Should().Be(id);
            resultName.Should().Be(name);
        }

        [Then(@"Verify 201 Created message is returned")]
        public void VerifyCreatedMessageIsReturned()
        {
            int numericStatusCode = (int)restResponse.StatusCode;
            numericStatusCode.Should().Be(201);
        }

        [Then(@"Verify that the posted data by (\d*) are showing up in the result")]
        [Obsolete]
        public void VerifyThatThePostedDataAreShowingUpInTheResult(int userid)
        {
            JObject jObj = (JObject)JsonConvert.DeserializeObject(restResponse.Content);
            var title = (string)jObj["title"];
            var body = (string)jObj["body"];
            var userId = (int)jObj["userId"];
            title.Should().Be((string)ScenarioContext.Current["title"]);
            body.Should().Be((string)ScenarioContext.Current["body"]);
            userId.Should().Be(userid);
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
