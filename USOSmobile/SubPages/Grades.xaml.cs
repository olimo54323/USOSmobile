using System.Collections.Generic;
using System.Collections.ObjectModel;
using USOSmobile.Models;

namespace USOSmobile.SubPages
{
    
    public partial class GradesPage : ContentPage
    {
        public ObservableCollection<CourseGradesViewModel> Grades { get; set; }
        public GradesPage()
        {
            InitializeComponent();
            Grades = new ObservableCollection<CourseGradesViewModel>();
            //cv.ItemsSource = Grades;
            Task.Run(() => { 
                GetData();
            });
            LoadData();
        }

        private async void GetData()
        {
            Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();
            foreach (dynamic course in ModelObjects.userCourses.Keys)
            {
                parameters["term_ids"] = course;
                await ModelObjects.apiBrowser.getGrades(parameters);
            }
        }

        private void LoadData()
        {
            Grades.Clear();
            foreach(var item in ModelObjects.courseGrades)
            {
                var keys = item.Key;
                var value = item.Value;

                var termCourses = (List<Course>)ModelObjects.userCourses[keys.Item1];
                var matchingCourse = termCourses.First(c => c.course_id == keys.Item2);
                string CoursePL = matchingCourse.course_name.pl;

                var CourseGradesCollection = new CourseGradesViewModel
                {
                    TermId = keys.Item1,
                    CourseId = keys.Item2,
                    CourseNamePL = CoursePL,
                    CourseGrades = new ObservableCollection<CourseGradeViewModel>()
                };

                foreach (var course in value.courseGrades)
                {
                    var CourseGradeCollection = new CourseGradeViewModel
                    {
                        
                        ValueSymbol = course.value_symbol,
                        ValueDescriptionPL = course.value_description.pl,
                        ExamId = course.exam_id.ToString(),
                        ExamSessionNumber = course.exam_session_number.ToString()
                    };
                    CourseGradesCollection.CourseGrades.Add(CourseGradeCollection);
                }
            }
        }

    }

    public class CourseGradeViewModel
    {

        public string ValueSymbol { get; set; }
        public string Passes { get; set; }
        public string ValueDescriptionPL { get; set; }
        public string ExamId { get; set; }
        public string ExamSessionNumber { get; set; }
    }

    public class CourseGradesViewModel
    {
        public string TermId { get; set; } //klucz podstawowy 1
        public string CourseId { get; set; } //klucz podstawowy 2
        public string CourseNamePL { get; set; } // wartość mapy kursów dla klucza pods. 1 i 2
        public ObservableCollection<CourseGradeViewModel> CourseGrades { get; set; }
    }
}