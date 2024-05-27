using USOSmobile.Models;

namespace USOSmobile.SubPages
{
    
    public partial class GradesPage : ContentPage
    {
        public GradesPage()
        {
            InitializeComponent();
        }
        private async void DataButttonClicked(object sender, EventArgs e)
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