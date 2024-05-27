using Newtonsoft.Json;
using RestSharp;
using System.Text;

namespace USOSmobile.Models
{

    internal class User_groups
    {
        private string _CourseUnitId;
        private int _GroupNumber;
        private MultiLangItem _ClassType;
        private string _ClassTypeId;
        private string _GroupUrl;
        private string _CourseId;
        private MultiLangItem _CourseName;
        private string _CourseHomepageUrl;
        private string _CourseProfileUrl;
        private int _CourseIsCurrentlyConducted;
        private string _CourseFacId;
        private string _CourseLangId;
        private string _TermId;
        private List<User> _Lecturers;
        private List<User> _Participants;

        //##########################################################

        public string course_unit_id 
        {
            get { return _CourseUnitId; }
            set { _CourseUnitId = value; }
        }
        public int group_number 
        {
            get { return _GroupNumber; }
            set { _GroupNumber = value; }
        }
        public MultiLangItem class_type
        {
            get { return _ClassType; }
            set { _ClassType = value; }
        }
        public string class_type_id
        {
            get { return _ClassTypeId; }
            set { _ClassTypeId = value; }
        }
        public string group_url
        {
            get { return _GroupUrl; }
            set { _GroupUrl = value; }
        }
        public string course_id
        {
            get { return _CourseId; }
            set { _CourseId = value; }
        }
        public MultiLangItem course_name
        {
            get { return _CourseName; }
            set { _CourseName = value; }
        }
        public string course_homepage_url
        {   
            get { return _CourseHomepageUrl; }
            set { _CourseHomepageUrl = value; }
        }
        public string course_profile_url
        {
            get { return _CourseProfileUrl; }
            set { _CourseProfileUrl = value; }
        }
        public int course_is_currently_conducted
        {
            get { return _CourseIsCurrentlyConducted; }
            set { _CourseIsCurrentlyConducted = value; }
        }
        public string course_fac_id
        {
            get { return _CourseFacId; }
            set { _CourseFacId = value; }
        }
        public string course_lang_id
        {
            get { return _CourseLangId; }
            set { _CourseLangId = value; }
        }
        public string term_id
        {
            get { return _TermId; }
            set { _TermId = value; }
        }
        public List<User> lecturers
        {
            get { return _Lecturers; }
            set { _Lecturers = value; }
        }
        public List<User> participants
        {
            get { return _Participants; }
            set { _Participants = value; }
        }

        //##########################################################

        public User_groups()
        {
            _CourseUnitId = string.Empty;
            _GroupNumber = 0;
            _ClassType = new MultiLangItem();
            _ClassTypeId = string.Empty;
            _GroupUrl = string.Empty;
            _CourseId = string.Empty;
            _CourseName = new MultiLangItem();
            _CourseHomepageUrl = string.Empty;
            _CourseProfileUrl = string.Empty;
            _CourseIsCurrentlyConducted = 0;
            _CourseFacId = string.Empty;
            _CourseLangId = string.Empty;
            _TermId = string.Empty;
            _Lecturers = new List<User>();
            _Participants = new List<User>();
        }

        //##########################################################

        public string Show()
        {
            return $"Course Unit ID: {course_unit_id}\nGroup Number: {group_number}\nClass Type PL: {class_type.pl}\nClass Type ID: {class_type_id}\nGroup URL: {group_url}\nCourse ID: {course_id}\nCourse Name PL: {course_name.pl}\nCourse Homepage URL: {course_homepage_url}\nCourse Profile URL: {course_profile_url}\nCourse Is Currently Conducted: {course_is_currently_conducted}\nCourse Faculty ID: {course_fac_id}\nCourse Language ID: {course_lang_id}\nTerm ID: {term_id}";
        }
    }

    internal class Course
    {
        private string _CourseId;
        private MultiLangItem _CourseName;
        private string _TermId;
        private List<User_groups> _UserGroups;

        //##########################################################

        public string course_id
        {
            get { return _CourseId; }
            set { _CourseId = value; }
        }
        public MultiLangItem course_name 
        {
            get { return _CourseName; }
            set { _CourseName = value; }
        }
        public string term_id
        { 
            get { return _TermId; }
            set { _TermId = value; } 
        }
        public List<User_groups> user_groups 
        {
            get { return _UserGroups; }
            set { _UserGroups = value; }
        }

        //##########################################################

        public Course()
        {
            _CourseId = string.Empty;
            _CourseName = new MultiLangItem();
            _TermId = string.Empty;
            _UserGroups = new List<User_groups>();
        }

        //##########################################################

        public string Show()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Course ID: {course_id}");
            sb.AppendLine($"Course Name PL: {course_name.pl}");
            sb.AppendLine($"Term ID: {term_id}");
            sb.AppendLine("User Groups:");
            foreach (var group in user_groups)
            {
                sb.AppendLine(group.Show());
            }

            return sb.ToString();
        }
    }

    internal class UserCourses
    {
        private List<Course> _TermCourses;
        public List<Course> termCourses
        {
            get { return _TermCourses; }
            set { _TermCourses = value; }
        }

        public UserCourses()
        {
            _TermCourses = new List<Course>();
        }

        static public bool deserializeCoursesData(RestResponse data, ref Dictionary<dynamic, UserCourses> dict)
        {
            dynamic? coursesData = JsonConvert.DeserializeObject<dynamic>(data.Content);

            if (coursesData == null)
                return false;

            foreach (dynamic term in coursesData.course_editions)
            {
                string termId = term.Name;
                UserCourses termCourses = new UserCourses();

                foreach (dynamic course in term.Value)
                {
                    Course newCourse = new Course();
                    newCourse.course_id = course.course_id;
                    newCourse.course_name = new MultiLangItem { pl = course.course_name.pl, en = course.course_name.en };
                    newCourse.term_id = course.term_id;
                    newCourse.user_groups = new List<User_groups>();

                    foreach (dynamic group in course.user_groups)
                    {
                        User_groups newGroup = new User_groups();
                        newGroup.course_unit_id = group.course_unit_id;
                        newGroup.group_number = group.group_number;
                        newGroup.class_type = new MultiLangItem { pl = group.class_type.pl, en = group.class_type.en };
                        newGroup.class_type_id = group.class_type_id;
                        newGroup.group_url = group.group_url;
                        newGroup.course_id = group.course_id;
                        newGroup.course_name = new MultiLangItem { pl = group.course_name.pl, en = group.course_name.en };
                        newGroup.course_homepage_url = group.course_homepage_url;
                        newGroup.course_profile_url = group.course_profile_url;
                        newGroup.course_is_currently_conducted = group.course_is_currently_conducted;
                        newGroup.course_fac_id = group.course_fac_id;
                        newGroup.course_lang_id = group.course_lang_id;
                        newGroup.term_id = group.term_id;
                        newGroup.lecturers = new List<User>();
                        newGroup.participants = new List<User>();

                        foreach (dynamic lecturer in group.lecturers)
                            newGroup.lecturers.Add(new User { id = lecturer.id, first_name = lecturer.first_name, last_name = lecturer.last_name });

                        foreach (dynamic participant in group.participants)
                            newGroup.participants.Add(new User { id = participant.id, first_name = participant.first_name, last_name = participant.last_name });

                        newCourse.user_groups.Add(newGroup);
                    }
                    termCourses.termCourses.Add(newCourse);
                }
                dict[termId] = termCourses;
            }
            return true;
        }



        public string Show()
        {
            StringBuilder sb = new StringBuilder();

            foreach (dynamic course in termCourses)
            {
                sb.AppendLine(course.Show());
            }
            return sb.ToString();
        }
    }

    
}
