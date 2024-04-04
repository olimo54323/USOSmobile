using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USOSmobile.Models
{
    internal class Courses
    {
        public string course_id { get; set; }
        public Dictionary<string, string> course_name { get; set; } //keys: pl, en
        public string term_id { get; set; }
        public string course_unit_id { get; set; }
        public string group_number { get; set; }
        public List<UserCourseGroup> user_groups { get; set; } 

        public Courses deserializeCourseData(RestResponse data)
        {
             return JsonConvert.DeserializeObject<Courses>(data.Content);
        }

        public Dictionary<string, Dictionary<string, Courses>> setCourses(Courses )
    }
}
