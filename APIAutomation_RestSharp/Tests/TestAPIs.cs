using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using APIAutomation_RestSharp.APIHelpers;
using APIAutomation_RestSharp.DTO;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace APIAutomation_RestSharp.Tests
{
    //[TestFixture]
    public class TestAPIs
    {
       

        //[Test]
        public void CreateNewUser()
        {
            var user = new Helpers<CreateUserResponseDTO>();
            string endpoint = "api/users";
            var client=user.SetURL(endpoint);
            string payload = @"{
                                 ""name"": ""morpheus"",
                                 ""job"": ""leader""
                               }";
           var request = user.CreatePostRequest(payload);
            var response = user.ExecuteRequest(client, request);
            CreateUserResponseDTO content = user.GetContent<CreateUserResponseDTO>(response);
            Assert.That(content.Name, Is.EqualTo("morpheus"));
            Assert.That(content.Job, Is.EqualTo("leader"));
            Assert.That(((int)response.StatusCode), Is.EqualTo((201)));

            
        }

        //[Test]
        public  void GetUserLists()
        {

            var user = new Helpers<GetListOfUserResponseDTO>();
            string endpoint = "api/users?page=2";
            var client=user.SetURL(endpoint);
            var request = user.CreateGetRequest();
            var response = user.ExecuteRequest(client,request);
            GetListOfUserResponseDTO content = user.GetContent<GetListOfUserResponseDTO>(response);
            Assert.That(((int)response.StatusCode), Is.EqualTo((200)));
            Assert.That(content.Page, Is.EqualTo(2));
            Assert.That(content.Data[1].Id, Is.EqualTo(8));
           StringAssert.AreEqualIgnoringCase("https://reqres.in/img/faces/11-image.jpg", content.Data[4].Avatar.ToString());
            Assert.That(content.Data.Count, Is.EqualTo(6));
         }

        //[Test]
        public void UpdateSingleUserDetails()
        {
            var user = new Helpers<UpdateUserResponseDTO>();
            string endpoint = "api/users/2";
            var client=user.SetURL(endpoint);
            string payload = @"{
                                 ""name"": ""morpheus"",
                                 ""job"": ""leader""
                               }";
            var request = user.CreateUpdateRequest(payload);
            var response=user.ExecuteRequest(client,request);
            UpdateUserResponseDTO content = user.GetContent<UpdateUserResponseDTO>(response);
            Assert.That(((int)response.StatusCode), Is.EqualTo((200)));
            Assert.That(content.Name, Is.EqualTo("morpheus"));
            Assert.That(content.Job, Is.EqualTo("leader"));
        }
        //[Test]
        public void DeleteUserDetails()
        {
            var user = new Helpers<UpdateUserResponseDTO>();
            string endpoint = "api/users/200000";
            var client = user.SetURL(endpoint);
            var request = user.CreateDeleteRequest();
            var response = user.ExecuteRequest(client, request);
            Assert.That(((int)response.StatusCode), Is.EqualTo((204)));
            

        }
    }
}
