using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USOSmobile.Models
{
    internal class Course_name
    {
        public string pl { get; set; }
        public string en { get; set; }
    }
    internal class Class_type
    {
        public string pl { get; set; }
        public string en { get; set; }
    }

    internal class Course
    {
        public string course_id { get; set; }
        public Course_name course_name { get; set; }
        public string term_id { get; set; }
        public IList<User_groups> user_groups { get; set; }
    }

    internal class User_groups
    {
        public string course_unit_id { get; set; }
        public int group_number { get; set; }
        public Class_type class_type { get; set; }
        public string class_type_id { get; set; }
        public string group_url { get;set; }
        public string course_id { get; set;}
        public Course_name course_name { get; set; }
        public string course_homepage_url { get; set; }
        public string course_profile_url { get; set; }
        public int course_is_currently_conducted { get; set; }
        public string course_fac_id { get; set; }
        public string course_lang_id { get; set; }
        public string term_id { get; set; }
        public List<User> lecturers { get; set; }
        public List<User> participants { get; set; }
    }

    internal class TermCourses
    {
        public string term_id { get; set; }
        public List<Course> termCourses;
    }

    internal class UserCourses
    {
        public Dictionary<string, TermCourses> courses = new Dictionary<string, TermCourses>();
        public UserCourses deserializeCoursesData(RestResponse data)
        {
            dynamic coursesData = JsonConvert.DeserializeObject<dynamic>(data.Content);

            if (coursesData == null)
                return null;

            foreach (dynamic term in coursesData.course_editions)
            {
                TermCourses termCourses = new TermCourses();
                termCourses.term_id = term.Name;
                termCourses.termCourses = new List<Course>();

                foreach (dynamic course in term.Value)
                {
                    Course newCourse = new Course();
                    newCourse.course_id = course.course_id;
                    newCourse.course_name = new Course_name { pl = course.course_name.pl, en = course.course_name.en };
                    newCourse.term_id = course.term_id;
                    newCourse.user_groups = new List<User_groups>();

                    foreach (dynamic group in course.user_groups)
                    {
                        User_groups newGroup = new User_groups();
                        newGroup.course_unit_id = group.course_unit_id;
                        newGroup.group_number = group.group_number;
                        newGroup.class_type = new Class_type { pl = group.class_type.pl, en = group.class_type.en };
                        newGroup.class_type_id = group.class_type_id;
                        newGroup.group_url = group.group_url;
                        newGroup.course_id = group.course_id;
                        newGroup.course_name = new Course_name { pl = group.course_name.pl, en = group.course_name.en };
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
                courses[termCourses.term_id] = termCourses;
            }
            return this;
        }
    }
}
