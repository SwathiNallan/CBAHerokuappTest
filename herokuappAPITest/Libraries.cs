using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serialization.Json;

namespace herokuappAPITest
{
    public static class Libraries
    {
        public static Dictionary<string, string> DeserialiseResponse(this IRestResponse response)
        {
            var deserialise = new JsonDeserializer();
            var output = deserialise.Deserialize<Dictionary<string, string>>(response);
            return output;
        }

        public static bool CheckResponseStatusOk(this IRestResponse response)
        {
            return response.StatusCode.Equals(HttpStatusCode.OK);
        }

        public static bool CheckResponseStatusCreated(this IRestResponse response)
        {
            return response.StatusCode.Equals(HttpStatusCode.Created);
        }
        public static bool CheckResponseStatusUpdated(this IRestResponse response)
        {
            return response.StatusCode.Equals(HttpStatusCode.NoContent);
        }


    }
}
