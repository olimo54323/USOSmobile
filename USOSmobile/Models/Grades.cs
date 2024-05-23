using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace USOSmobile.Models
{
    internal class Value_description
    {
        public string pl;
        public string en;
    }

    internal class CourseGrades
    {
        public string value_symbol;
        public string passes;
        public Value_description value_description;
        public int exam_id;
        public int exam_session_number;

        static public bool deserializeGradesData(RestResponse data, ref Dictionary<(string, string, string), CourseGrades> GradesDict)
        {
            dynamic gData = JsonConvert.DeserializeObject<object>(data.Content);

            if (gData == null)
                return false;

            foreach (dynamic termData in gData)
            {
                string termId = termData.Name;

                foreach (dynamic course in termData.Value)
                {
                    string courseId = course.Name;

                    foreach (dynamic grade in course.Value.course_grades)
                    {
                        foreach(dynamic examTerm in grade.Value)
                        {

                        }
                        
                    }
                }
            }
            return true;
        }
    }
}
