using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSqlAuto.BL
{
    public class GogsServer : IGogs
    {

        public void AddUser(string name, string password, string url)
        {
            var client = new RestClient($"{url}/user/sign_up");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("user_name", name);
            request.AddParameter("password", password);
            request.AddParameter("email", $"{name}@{name}.postman");
            request.AddParameter("retype", password);
            IRestResponse response = client.Execute(request);
        }
    }
}
