using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USOSmobile.Models
{
    internal class UserCourseGroup
    {
        public Dictionary<string, string> class_type { get; set; } //keys: pl, en
        public string class_type_id { get; set; }
        public string group_url { get; set; }
        public string course_homepage_url { get; set; }
        public string course_profile_url { get; set; }
        public string course_is_currently_conducted { get; set; }
        public string course_fac_id { get; set; }
        public string course_lang_id { get; set; }
        public List<User> lecturers { get; set; }
        public List<User> participants { get; set; }
    }
}
