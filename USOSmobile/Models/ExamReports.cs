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
    internal class Type_description
    {
        public string pl { get; set; }
        public string en { get; set; }
    }
    internal class ExamReports
    {
        string id { get; set; }
        string type_id { get; set; }
        Type_description type_description { get; set; }

        static public bool deserializeExamReportsData(RestResponse data, ref Dictionary<(dynamic, dynamic), ExamReports> dict)
        {
            dynamic courseGradesData = JsonConvert.DeserializeObject<dynamic>(data.Content);

            if (courseGradesData == null)
                return false;

            foreach (var termData in courseGradesData)
            {
                string termId = termData.Name;

                foreach (dynamic course in termData.Value)
                {
                    string courseId = course.Name;
                    foreach (dynamic grade in course.Value.course_grades)
                    {
                        ExamReports cg = new ExamReports();
                        cg.id = grade.id;
                        cg.type_id = grade.type_id;
                        cg.type_description = new Type_description
                        {
                            pl = grade.type_description.pl,
                            en = grade.type_description.en
                        };
                        dict[(termId, courseId)] = cg;
                    }

                }
            }
            return true;
        }
    }
}
