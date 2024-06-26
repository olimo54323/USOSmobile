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
    internal class ExamReports
    {
        private string _Id;
        private string _TypeId;
        private MultiLangItem _TypeDescription;

        //##########################################################

        public string id 
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string type_id 
        {
            get { return _TypeId; }
            set { _TypeId = value; } 
        }
        public MultiLangItem type_description
        {
            get { return _TypeDescription; }
            set { _TypeDescription = value; } 
        }

        //##########################################################

        public ExamReports() 
        {
            _Id = string.Empty;
            _TypeId = string.Empty;
            _TypeDescription = new MultiLangItem();
        }

        //##########################################################

        static public bool deserializeExamReportsData(RestResponse data, ref Dictionary<(dynamic, dynamic), ExamReports> dict)
        {
            dynamic courseGradesData = JsonConvert.DeserializeObject<dynamic>(data.Content);

            if (courseGradesData == null)
                return false;

            foreach (dynamic termData in courseGradesData)
            {
                string termId = termData.Name;

                foreach (dynamic course in termData.Value)
                {
                    string courseId = course.Name;

                    foreach (dynamic grade in course.Value.course_grades)
                    {
                        ExamReports er = new ExamReports();
                        er.id = grade.id;
                        er.type_id = grade.type_id;
                        er.type_description = new MultiLangItem
                        {
                            pl = grade.type_description.pl,
                            en = grade.type_description.en
                        };
                        dict[(termId, courseId)] = er;
                    }
                }
            }
            return true;
        }

        //##########################################################

        public string Show()
        {
            return $"ID: {_Id}\nType ID: {_TypeId}\nType Description: {_TypeDescription.pl}\n\n";
        }
    }
}
