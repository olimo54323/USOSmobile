using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USOSmobile.Models
{
    static class ModelObjects
    {
        static public User user = new User();
        static public APIBrowser apiBrowser = new APIBrowser();
        static public UserCourses userCourses = new UserCourses();
        static public Dictionary<(dynamic, dynamic), TimeTables> timeTables = new Dictionary<(dynamic, dynamic),TimeTables>(); //unit_id, group_number
        static public Dictionary<(dynamic, dynamic), ExamReports> examReports = new Dictionary<(dynamic, dynamic), ExamReports>(); //term_ids, unit_id

    }
}
