using System.Xml;
using USOSmobile.Models;
using System.IO;

namespace USOSmobile.SubPages
{
    public partial class MyUSOSPage : ContentPage
    {
        public MyUSOSPage()
        {
            InitializeComponent();
            StudentData.Text = $"{"Witaj, " + ModelObjects.user.first_name + " " +  ModelObjects.user.last_name}";

            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
            //data["active_terms_only"] = "false";
            data["term_ids"] = "2022/2023-L";

            //Task.Run(() => Helpers.apiBrowser.getDiagnosticData("services/terms/terms_index"))    //wszystkie roczniki
            //Task.Run(() => ModelObjects.apiBrowser.getDiagnosticData("services/payments/user_payments"))
            //Task.Run(() => ModelObjects.apiBrowser.getDiagnosticData("services/examrep/user", data))
            //Task.Run(() => ModelObjects.apiBrowser.getDiagnosticData("services/courses/user", data))
            Task.Run(() => ModelObjects.apiBrowser.getDiagnosticData("services/grades/terms2", data))
                .ContinueWith(task =>
                {
                    CoursesID.Text = task.Result;
                    Clipboard.SetTextAsync(task.Result);
                }, TaskScheduler.FromCurrentSynchronizationContext());


        }

        private async void DataBtn_Clicked(object sender, EventArgs e)
        {
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
            data["active_terms_only"] = "false";
            bool isCoursesGet = await ModelObjects.apiBrowser.getCourses(data);
            if (isCoursesGet)
            {
                string lines = "";
                foreach (dynamic item in ModelObjects.userCourses.courses)
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
                            bool isTableGet = await ModelObjects.apiBrowser.getTimetables(subsubdata);
                            if (isTableGet)
                            {
                                foreach (dynamic table in ModelObjects.timeTables[(subsubdata["unit_id"], subsubdata["group_number"])].tables)
                                {
                                   lines += "\t\t\t" + table.start_date.ToString("HH:mm") + " " + table.end_date.ToString("HH:mm") + "\n";
                                }
                            }
                            
                        }
                    }
                    lines += "\n";
                }
                CoursesID.Text = lines;
            }
        }
    }
}