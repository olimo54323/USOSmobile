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
        public string last_name { get; set; }
        public string first_name { get; set; }

        //public User(int userID, string firstName, string lastName)
        //{
        //    UserId = userID;
        //    FirstName = firstName;
        //    LastName = lastName;
        //}
    };
}
