using USOSmobile.Models;

namespace USOSmobile.SubPages
{
    
    public partial class ExamsPage : ContentPage
    {
        public ExamsPage()
        {
            InitializeComponent();
            Task.Run(() =>
            {
                LoadData();
            });
        }

        private async void LoadData()
        {
            Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();
            string line = string.Empty;
            foreach (dynamic course in ModelObjects.userCourses.Keys)
            {
                parameters["term_ids"] = course;
                await ModelObjects.apiBrowser.getExams(parameters);
            }

            foreach (dynamic course in ModelObjects.examReports.Values)
            {
                line += course.Show();
            }
            ExamsLabel.Text = line;

        }
    }
}