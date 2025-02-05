using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Xml;

namespace APIAutomation_RestSharp.APIHelpers
{
    public class Helpers<T>
    {
        public RestClient restClient;
        public RestRequest restRequest;
        public RestResponse restResponse;
        public static string baseURL;
        public static string clientId;
        public static string secret;
        public string url;

        static Helpers()
        {
            baseURL = GetTestRunParameter("baseURL");
            clientId = GetTestRunParameter("clientId");
            secret = GetTestRunParameter("secret");
        }

        private static string GetTestRunParameter(string parameterName)
        {
            var configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "TestConfig.xml");
            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException($"The configuration file '{configFilePath}' was not found.");
            }

            var doc = new XmlDocument();
            doc.Load(configFilePath);
            var node = doc.SelectSingleNode($"//Parameter[@name='{parameterName}']");
            return node?.Attributes["value"]?.Value;
        }

        public RestClient SetURL(string endPoint)
        {
            url = Path.Combine(baseURL, endPoint);
            restClient = new RestClient(url);
            return restClient;
        }

        public RestRequest CreatePostRequest(string payload)
        {
            var request = new RestRequest(url, Method.Post);
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

        public RestResponse ExecuteRequest(RestClient client, RestRequest request)
        {
            return client.Execute(request);
        }

        public DTO GetContent<DTO>(RestResponse response)
        {
            return JsonConvert.DeserializeObject<DTO>(response.Content);
        }
    }
}
