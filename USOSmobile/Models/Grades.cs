using Newtonsoft.Json;
using RestSharp;


namespace USOSmobile.Models
{
    internal class CourseGrade
    {
        private string _ValueSymbol;
        private string _Passes;
        private MultiLangItem _ValueDescription;
        private int _ExamId;
        private int _ExamSessionNumber;

        //##########################################################

        public string value_symbol
        {
            get { return _ValueSymbol; }
            set { _ValueSymbol = value; }
        }
        public string passes
        {
            get { return _Passes; }
            set { _Passes = value; }
        }
        public MultiLangItem value_description
        {
            get { return _ValueDescription; }
            set { _ValueDescription = value; }  
        }
        public int exam_id
        {
            get { return _ExamId; }
            set { _ExamId = value; }
        }
        public int exam_session_number
        {
            get { return _ExamSessionNumber; }
            set { _ExamSessionNumber = value; }
        }

        //##########################################################

        public CourseGrade()
        {
            _ValueSymbol = string.Empty;
            _Passes = string.Empty;
            _ValueDescription = new MultiLangItem();
            _ExamId = 0;
            _ExamSessionNumber = 0;
        }

        //##########################################################       

        public string Show()
        {
            return $"Value Symbol: {_ValueSymbol}\nPasses: {_Passes}\nValue Description: {_ValueDescription.pl}\nExam ID: {_ExamId}\nExam Session Number: {_ExamSessionNumber}\n";
        }
    }

    internal class CourseGrades
    {
        private List<CourseGrade> _CourseGrades;

        //##########################################################

        public List<CourseGrade> courseGrades
        {
            get { return _CourseGrades; }
            set { _CourseGrades = value;}
        }

        //##########################################################

        public CourseGrades()
        {
            _CourseGrades = new List<CourseGrade>();
        }

        //##########################################################

        static public bool deserializeGradesData(RestResponse data, ref Dictionary<(dynamic, dynamic), CourseGrades> dict)
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
                        CourseGrades cgs = new CourseGrades();
                        foreach (dynamic examTerm in grade)
                        {
                            if (examTerm.Value == null)
                                continue;
                            string examtermnumber = examTerm.Name;

                            CourseGrade cg = new CourseGrade();
                            cg.value_symbol = examTerm.Value.value_symbol;
                            cg.passes = examTerm.Value.passes;
                            cg.value_description = new MultiLangItem
                            {
                                pl = examTerm.Value.value_description.pl,
                                en = examTerm.Value.value_description.en
                            };
                            cg.exam_id = examTerm.Value.exam_id;
                            cg.exam_session_number = examTerm.Value.exam_session_number;
                            cgs.courseGrades.Add(cg);
                        }
                        dict[(termId, courseId)] = cgs;
                    }
                }
            }
            return true;
        }

        //##########################################################

        public string Show()
        {
            string result = string.Empty;
            foreach (var grade in _CourseGrades)
            {
                result += grade.Show() + "\n\n";
            }
            return result.Trim();
        }
    }
}
