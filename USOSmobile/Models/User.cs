using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USOSmobile.Models
{
    internal class User
    {
        public int id { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }

        //public User(int userID, string firstName, string lastName)
        //{
        //    UserId = userID;
        //    FirstName = firstName;
        //    LastName = lastName;
        //}

        //public static User UserFromJSON(Dictionary<string, dynamic> data)
        //{
        //    string fn = data["first_name"];
        //    string ln = data["last_name"];
        //    return new User(fn, ln);
        //}
    };
}
