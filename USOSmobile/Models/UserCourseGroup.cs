using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USOSmobile.Models
{
    internal class UserCourseGroup
    {
        public Course course {  get; set; }
        public string groupNumber { get; set; }
        public Dictionary<string, string> classType { get; set; } //keys: pl, en
        public string classTypeID { get; set; }
        public string groupURL { get; set; }
        public string courseID { get; set; }
        public string courseHomepageURL { get; set; }
        public string courseProfileURL { get; set; }
        public string courseIsCurrentlyConducted { get; set; }
        public string courseFacultyID { get; set; }
        public string courseLanguageID { get; set; }
        public List<User> lecturers { get; set; }
        public List<User> participants { get; set; }
    }
}
