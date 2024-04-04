using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USOSmobile.Models
{
     static class Helpers
    {
        static public User user = new User();
        static public APIBrowser apiBrowser = new APIBrowser();
        static public Dictionary<string, Dictionary<string, Courses>> courses = new Dictionary<string, Dictionary<string, Courses>>(); //first key - termID, second key - courseID
    }
}
