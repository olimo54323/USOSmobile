using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USOSmobile.Models
{
    internal class User
    {
        public int id { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }

        public void deserializeUserData(RestResponse data)
        {
            dynamic userData = JsonConvert.DeserializeObject<dynamic>(data.Content);
            id = userData.id;
            lastName = userData.last_name;
            firstName = userData.first_name;
        }
    };
}
