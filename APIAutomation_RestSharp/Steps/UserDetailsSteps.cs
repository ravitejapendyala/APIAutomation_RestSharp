using APIAutomation_RestSharp.APIHelpers;
using APIAutomation_RestSharp.DTO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace APIAutomation_RestSharp.Steps
{
    [Binding]
    public class UserDetailsSteps
    {
        private readonly ScenarioContext _scenarioContext;
        public RestResponse response;
        public RestRequest request;
       
        
        
        public UserDetailsSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have new user information with name as (.*) and Job as (.*)")]
        public void IHaveNewUserDetails(string username,string Job)
        {
            _scenarioContext["UserName"]=username;
            _scenarioContext["Job"]=Job;
        }

        [When(@"I create new user")]
        public void ICreateNewUser()
        {

            var user = new Helpers<CreateUserResponseDTO>();
            string endpoint = "api/users";
            var client = user.SetURL(endpoint);
            var name = _scenarioContext["UserName"];
            var job = _scenarioContext["Job"];

            string payload = $@"{{
                     ""name"": ""{name}"",
                     ""job"": ""{job}""
                   }}";
            request = user.CreatePostRequest(payload);
            response = user.ExecuteRequest(client, request);
        }

        [Then(@"I should receive the status code as (.*)")]
        public void VerifyStatusCode(int statusCode)
        {
             Assert.That(((int)response.StatusCode), Is.EqualTo((statusCode)));
        }

        [Then(@"I should see (.*) as (.*) in create user response")]
        public void ThenIShouldSeeInResponse(string fieldName , string value)
        {
            var user = new Helpers<CreateUserResponseDTO>();
            CreateUserResponseDTO content = user.GetContent<CreateUserResponseDTO>(response);
            switch(fieldName.ToUpper().Trim()) 
            {
                case "USERNAME":
                    Assert.That(content.Name, Is.EqualTo(value));
                    break;
                case "JOB":
                    Assert.That(content.Job, Is.EqualTo(value));
                    break;
               
            }
        }

        [Given(@"an user with ID (.*)")]
        public void GivenAnUserWithID(int userID)
        {
            _scenarioContext["UserID"] = userID;
        }
        [When(@"I get user details")]
        public void WhenIGetUserDetails()
        {
            var user = new Helpers<GetListOfUserResponseDTO>();
            string endpoint = "api/users/" + _scenarioContext["UserID"];
            var client = user.SetURL(endpoint);
            var request = user.CreateGetRequest();
            response = user.ExecuteRequest(client, request);
           
        }
        [Then(@"I should see (.*) as (.*) in get user response")]
        public void ThenIShouldSeeDetailsInGetUserResponseForSingleUser(string fieldName, string value)
        {

            var user = new Helpers<GetSingleUserResponseDTO>();
            GetSingleUserResponseDTO content = user.GetContent<GetSingleUserResponseDTO>(response);
            switch (fieldName.ToUpper().Trim())
            {
                case "EMAIL":
                    Assert.That(content.Data.Email,Is.EqualTo(value));
                    break;
                case "Avatar":
                    StringAssert.AreEqualIgnoringCase("https://reqres.in/img/faces/11-image.jpg", content.Data.Avatar.ToString());
                     break;

            }
        }
        [When(@"I Update user details")]
        public void WhenIUpdateUserDetails()
        {
            var user = new Helpers<UpdateUserResponseDTO>();
            string endpoint = "api/users/5";
            var client = user.SetURL(endpoint);
            var name = _scenarioContext["UserName"];
            var job = _scenarioContext["Job"];

            string payload = $@"{{
                     ""name"": ""{name}"",
                     ""job"": ""{job}""
                   }}";
            request = user.CreateUpdateRequest(payload);
            response = user.ExecuteRequest(client, request);
        }

        [When(@"I Delete user details")]
        public void WhenIDeleteUserDetails()
        {
            var user = new Helpers<GetSingleUserResponseDTO>();
            string endpoint = "api/users/" + _scenarioContext["UserID"];
            var client = user.SetURL(endpoint);
            var request = user.CreateDeleteRequest();
            response = user.ExecuteRequest(client, request);

        }


        [Then(@"I should see (.*) as (.*) in Update user response")]
        public void ThenIShouldSeeResponseUpdateUserResponse(string fieldName , string value)
        {

            var user = new Helpers<UpdateUserResponseDTO>();
            UpdateUserResponseDTO content = user.GetContent<UpdateUserResponseDTO>(response);
            switch (fieldName.ToUpper().Trim())
            {
                case "NAME":
                    Assert.That(content.Name, Is.EqualTo(value));
                    break;
                case "JOB":
                    Assert.That(content.Job, Is.EqualTo(value));
                    break;

            }
        }
        [Given(@"I have incomplete user information with name as (.*) and Job as (.*)")]
        public void GivenIHaveIncompleteUserInformation(string userName, string job)
        {
            _scenarioContext["UserName"] = userName;
            _scenarioContext["Job"] = job;
        }
        [When(@"I attempt to create a new user with incomplete information")]
        public void WhenIAttemptToCreateANewUserWithIncompleteInformation()
        {
            try
            {

                var user = new Helpers<CreateUserResponseDTO>();
                string endpoint = "api/users";
                var client = user.SetURL(endpoint);
                var name = _scenarioContext["UserName"];
                var job = _scenarioContext["Job"];

                string payload = $@"{{
                     ""name"": ""{name}"",
                     ""job"": ""{job}""
                   }}";
                request = user.CreatePostRequest(payload);
                response = user.ExecuteRequest(client, request);

            }
            catch (Exception ex) 
            {
                _scenarioContext["Error"] = ex.Message;
                
            }
        }
        [Then(@"I should receive a validation error")]
        public void ThenIShouldReceiveAValidationError()
        {
            var error = _scenarioContext["Error"];

            Assert.That(error.ToString(),Is.EqualTo("User Creating was unsuccessful"));

        }

        [Given(@"a user with ID (.*) does not exist")]
        public void GivenAUserWithIDInvalidDoesNotExist(int userID)
        {
            _scenarioContext["UserID"]=userID;
        }
        [When(@"I attempt to delete the user with Invalid ID")]
        public void WhenIAttemptToDeleteTheUserWithInvalidID()
        {
            try
            {
                var user = new Helpers<GetSingleUserResponseDTO>();
                string endpoint = "api/users/" + _scenarioContext["UserID"];
                var client = user.SetURL(endpoint);
                var request = user.CreateDeleteRequest();
                response = user.ExecuteRequest(client, request);
            }
            catch (Exception ex)
            {
                _scenarioContext["Error"] = ex.Message;

            }
        }

        [Then(@"the system should respond with a user not found error")]
        public void ThenTheSystemShouldRespondWithAUserNotFoundError()
        {
            var error = _scenarioContext["Error"].ToString();

           Assert.That(error.Contains("not found"), "Unexpected error message");
        }







    }
}
