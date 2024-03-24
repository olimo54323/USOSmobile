using USOSmobile.Models;

namespace USOSmobile.SubPages
{
    
    public partial class MyUSOSPage : ContentPage
    {
        public MyUSOSPage()
        {
            InitializeComponent();
            StudentData.Text = $"{"Witaj, " + Helpers.user.first_name + " " +  Helpers.user.last_name}";

            Task.Run(() => Helpers.apiBrowser.getDiagnosticData("services/terms/terms_index"))
                .ContinueWith(task =>
                {
                    Clipboard.SetTextAsync(task.Result);
                }, TaskScheduler.FromCurrentSynchronizationContext());
         }
    }
}