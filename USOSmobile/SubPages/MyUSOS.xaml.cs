using USOSmobile.Models;

namespace USOSmobile.SubPages
{
    
    public partial class MyUSOSPage : ContentPage
    {
        public MyUSOSPage()
        {
            InitializeComponent();
            StudentData.Text = $"{"Witaj, " + Helpers.user.firstName + " " +  Helpers.user.lastName}";

            //Task.Run(() => Helpers.apiBrowser.getDiagnosticData("services/terms/terms_index"))    //wszystkie roczniki
            Task.Run(() => Helpers.apiBrowser.getDiagnosticData("services/courses/user"))
                .ContinueWith(task =>
                {
                    CoursesID.Text = task.Result;
                    Clipboard.SetTextAsync(task.Result);
                }, TaskScheduler.FromCurrentSynchronizationContext());
         }
    }
}