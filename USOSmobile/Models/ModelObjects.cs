using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USOSmobile.Models
{
    static class ModelObjects
    {
        static public APIBrowser apiBrowser = new APIBrowser();
        static public User user = new User();
        static public Dictionary<dynamic, UserCourses> userCourses = new Dictionary<dynamic, UserCourses>(); //term_id
        static public Dictionary<(dynamic, dynamic), TimeTables> timeTables = new Dictionary<(dynamic, dynamic),TimeTables>(); //unit_id, group_number 
        static public Dictionary<(dynamic, dynamic), ExamReports> examReports = new Dictionary<(dynamic, dynamic), ExamReports>(); //term_id, unit_id
        static public Dictionary<(dynamic, dynamic), CourseGrades> courseGrades = new Dictionary<(dynamic, dynamic), CourseGrades>(); //term_id, unit_id
    }
}
