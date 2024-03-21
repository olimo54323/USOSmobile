using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USOSmobile.Models
{
    class User
    {
        public string FirstName { get; }
        public string LastName { get; }

        public User(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public static User UserFromJSON(Dictionary<string, dynamic> data)
        {
            string fn = data["first_name"];
            string ln = data["last_name"];
            return new User(fn, ln);
        }
    };
}
