using System.Collections.ObjectModel;
using USOSmobile.Models;

namespace USOSmobile.SubPages
{
    
    public partial class ExamsPage : ContentPage
    {
        public ObservableCollection<ExamReportsViewModel> Exams {  get; set; }
        public ExamsPage()
        {
            InitializeComponent();
            Task.Run(() =>
            {
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
                await ModelObjects.apiBrowser.getExams(parameters);
            }
        }

        private void LoadData()
        {
            Exams.Clear();
            foreach (var item in ModelObjects.examReports)
            {
                var keys = item.Key;
                var value = item.Value;

                var termCourses = (List<Course>)ModelObjects.userCourses[keys.Item1];
                var matchingCourse = termCourses.First(c => c.course_id == keys.Item2);
                string CoursePL = matchingCourse.course_name.pl;

                var ExamReportsCollection = new ExamReportsViewModel
                {
                    TermId = keys.Item1,
                    CourseId = keys.Item2,
                    CourseNamePL = CoursePL,
                    Id = value.id,
                    TypeId = value.type_id,
                    TypeDescriptionPL = value.type_description.pl
                };
                Exams.Add(ExamReportsCollection);
            }
        }
    }


    public class ExamReportsViewModel
    {
        public string TermId { get; set; } //klucz podstawowy 1
        public string CourseId { get; set; } //klucz podstawowy 2
        public string CourseNamePL { get; set; } // wartość mapy kursów dla klucza pods. 1 i 2
        public string Id {  get; set; }
        public string TypeId {  get; set; }
        public string TypeDescriptionPL {  get; set; }
    }

}