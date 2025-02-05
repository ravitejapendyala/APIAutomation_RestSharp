using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace APIAutomation_RestSharp.APIHelpers
{
    public class Helpers<T>
    {
        public RestClient restClient;
        public RestRequest restRequest;
        public RestResponse restResponse;
        public string baseURL = "https://reqres.in/";
        public string url;

        public RestClient SetURL(string endPoint)
        {
             url = Path.Combine(baseURL, endPoint);
            restClient=new RestClient(url);
            return restClient;
        }

        public RestRequest CreatePostRequest(string payload)
        {
            var request = new RestRequest(url,Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", payload, ParameterType.RequestBody);
            return request;
        }
        public RestRequest CreateGetRequest()
        {
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Accept", "application/json");
            return request;

        }
        public RestRequest CreateUpdateRequest(string payload) 
        {

            var request = new RestRequest(url, Method.Put);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", payload, ParameterType.RequestBody);
            return request;
        }

        public RestRequest CreateDeleteRequest()
        {
            var request = new RestRequest(url, Method.Delete);
            request.AddHeader("Accept", "application/json");
            return request;
        }

        public  RestResponse ExecuteRequest(RestClient client , RestRequest request)
        {
            return  client.Execute(request);
            
        }

        /*
        public DTO GetContent<DTO>(RestResponse response)
        {
            if (response.Headers.Any(h => h.Name == "Content-Type" && h.Value.ToString().Contains("application/json")))
            {
                return JsonConvert.DeserializeObject<DTO>(response.Content); ;
            }
            else
            {
                // Handle non-JSON response, log the error, or throw an exception
                throw new InvalidOperationException("Non-JSON response received.");
            }
        }
        */
        public DTO GetContent<DTO>(RestResponse response)
        {
                return JsonConvert.DeserializeObject<DTO>(response.Content); 
           
        }



    }
}
