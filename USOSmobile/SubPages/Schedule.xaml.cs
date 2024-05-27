using Microsoft.VisualBasic.FileIO;
using USOSmobile.Models;

namespace USOSmobile.SubPages
{
    
    public partial class SchedulePage : ContentPage
    {
        public SchedulePage()
        {
            InitializeComponent();
        }
        private async void DataButttonClicked(object sender, EventArgs e)
        {
            Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();
            string line = string.Empty;
            foreach (dynamic userCourses in ModelObjects.userCourses.Values)
            {
                foreach(dynamic termCourses in userCourses.termCourses)
                {
                    foreach(dynamic userGroup in termCourses.user_groups)
                    {
                        parameters["unit_id"] = userGroup.course_unit_id;
                        parameters["group_number"] = userGroup.group_number;
                        await ModelObjects.apiBrowser.getTimetables(parameters);
                    }
                }
            }

            foreach (dynamic course in ModelObjects.timeTables.Values)
            {
                line += course.Show();
            }
            ScheduleLabel.Text = line;
        }
    }
}