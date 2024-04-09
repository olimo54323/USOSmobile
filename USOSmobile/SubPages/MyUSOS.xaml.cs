using USOSmobile.Models;

namespace USOSmobile.SubPages
{
    public partial class MyUSOSPage : ContentPage
    {
        public MyUSOSPage()
        {
            InitializeComponent();
            StudentData.Text = $"{"Witaj, " + Helpers.user.first_name + " " +  Helpers.user.last_name}";
            ////Task.Run(() => Helpers.apiBrowser.getDiagnosticData("services/terms/terms_index"))    //wszystkie roczniki
            //Task.Run(() => Helpers.apiBrowser.getDiagnosticData("services/courses/user"))
            //    .ContinueWith(task =>
            //    {
            //        CoursesID.Text = task.Result;
            //        Clipboard.SetTextAsync(task.Result);
            //    }, TaskScheduler.FromCurrentSynchronizationContext());

            
        }

        private async void DataBtn_Clicked(object sender, EventArgs e)
        {
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
            data["active_terms_only"] = "false";
            if(await Helpers.apiBrowser.getCourses(data))
            {
                string lines ="";
                foreach (dynamic item in Helpers.userCourses.courses)
                {
                    lines += item.Key + "\n";
                    foreach (dynamic subitem in item.Value.termCourses)
                    {
                        lines += "\t" + subitem.course_name.pl + "\n";
                        foreach(dynamic subsubitem in subitem.user_groups)
                        {
                            lines += "\t\t" + subsubitem.class_type.pl + ", grupa: " + subsubitem.group_number + "\n";
                        }
                    }
                    lines += "\n";
                }
                CoursesID.Text = lines;
            }
            //CoursesID.Text = Helpers.userCourses.showCourseData("2023/2024-L");
        }
    }
}