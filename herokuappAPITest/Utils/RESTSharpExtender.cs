using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace herokuappAPITest
{
    public class RESTSharpExtender
    {
        public RestClient client { get; set; }

        #region initialize client and common methods
        public void InitRestClient(string baseURL)
        {
            this.client = new RestClient(baseURL);
        }
        public static void IsResponseOK(IRestResponse response)
        {
            Assert.IsTrue(response.CheckResponseStatusOk(), "Failed to get valid response code - OK");
        }

        public static void IsResponseCreated(IRestResponse response)
        {
            Assert.IsTrue(response.CheckResponseStatusCreated(), "Failed to get valid response code - Created");
        }
        public static bool IsResponseUpdated(IRestResponse response)
        {
            Assert.IsTrue(response.CheckResponseStatusUpdated(), "Failed to get valid response code - No content / updated");
            return response.CheckResponseStatusUpdated();
        }


        #endregion

        #region GET Or POST methods
        public IRestResponse GetOrPostWithBodyJson(string url, object body, Method method)
        {
            var request = new RestRequest(url, method);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(body);
            var response = client.Execute(request);
            IsResponseCreated(response);
            return response;
        }

        public IRestResponse PUTWithBodyJson(string url, object body)
        {
            var request = new RestRequest(url, Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(body);
            var response = client.Execute(request);
            return response;
        }

       

        public IRestResponse GetResponse(string url)
        {
            var request = new RestRequest(url, Method.GET);
            var response = client.Execute(request);
            IsResponseOK(response);
            return response;
        }

        public IRestResponse GetWithURLSegment(string url, string segmentName, object segmentValue, Method method)
        {
            var request = new RestRequest(url, method);
            request.AddUrlSegment(segmentName, segmentValue);
            var response = client.Execute(request);
            IsResponseOK(response);
            return response;
        }

        public IRestResponse GetOrPostWithParams(string url, Dictionary<string, object> body, Method method)
        {
            var request = new RestRequest(url, method);
            foreach (KeyValuePair<string, object> kvp in body)
            {
                request.AddParameter(kvp.Key, kvp.Value);
            }
            var response = client.Execute(request);
            IsResponseOK(response);
            return response;
        }

        #endregion
    }
}
