using USOSmobile.Models;

namespace USOSmobile.SubPages
{
    
    public partial class MyUSOSPage : ContentPage
    {
        public MyUSOSPage()
        {
            InitializeComponent();
            StudentData.Text = $"{"Witaj, " + Helpers.user.first_name + " " +  Helpers.user.last_name}";

            Dictionary<string,dynamic> data = new Dictionary<string,dynamic>();
            Task.Run(() => Helpers.apiBrowser.getGroupsIdParticipant(data))
                .ContinueWith(task =>
                {
                    CoursesID.Text = task.Result;
                }, TaskScheduler.FromCurrentSynchronizationContext());
                }
    }
}