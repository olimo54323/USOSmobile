using USOSmobile.Models;

namespace USOSmobile.SubPages
{
    
    public partial class GradesPage : ContentPage
    {
        public GradesPage()
        {
            InitializeComponent();
            Task.Run(() => { 
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
                await ModelObjects.apiBrowser.getGrades(parameters);
            }

            foreach (dynamic course in ModelObjects.courseGrades.Values)
            {
                line += course.Show();
            }
            GradesLabel.Text = line;
        }
    }
}