using Microsoft.VisualBasic.FileIO;
using USOSmobile.Models;

namespace USOSmobile.SubPages
{
    
    public partial class SchedulePage : ContentPage
    {
        public SchedulePage()
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
        }
    }

    public class ScheduleViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate {  get; set; }
    }

}