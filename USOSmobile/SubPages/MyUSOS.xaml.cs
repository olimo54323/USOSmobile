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
            bool isCoursesGet = await Helpers.apiBrowser.getCourses(data);
            if (isCoursesGet)
            {
                string lines = "";
                foreach (dynamic item in Helpers.userCourses.courses)
                {
                    lines += item.Key + "\n";
                    foreach (dynamic subitem in item.Value.termCourses)
                    {
                        lines += "\t" + subitem.course_name.pl + "\n";
                        foreach (dynamic subsubitem in subitem.user_groups)
                        {
                            lines += "\t\t" + subsubitem.class_type.pl + ", grupa: " + subsubitem.group_number + "\n";
                            Dictionary<string, dynamic> subsubdata = new Dictionary<string, dynamic>();
                            subsubdata["unit_id"] = subsubitem.course_unit_id;
                            subsubdata["group_number"] = subsubitem.group_number;
                            string subLine = (await Helpers.apiBrowser.getDiagnosticData("services/tt/classgroup", subsubdata)) != "[]" ? await Helpers.apiBrowser.getDiagnosticData("services/tt/classgroup", subsubdata) : "";
                            lines += "\t\t" + subLine + "\n";
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