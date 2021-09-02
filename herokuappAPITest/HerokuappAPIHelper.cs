using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using herokuapp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace herokuappAPITest
{
    public class HerokuappAPIHelper : RESTSharpExtender
    {
        public List<User> GetUsers()
        {
            var content = GetResponse("user");
           List<User> usersList = JsonConvert.DeserializeObject<List<User>>(content.Content);
            return usersList;
        }

        public Dictionary<string, string> SaveUser(User user)
        {
            var content = GetOrPostWithBodyJson("user", user, Method.POST);
            return content.DeserialiseResponse();
        }

        public bool UpdateUser(User user)
        {
            var content = PUTWithBodyJson("user", user);
            return IsResponseUpdated(content);
        }
    }
}
