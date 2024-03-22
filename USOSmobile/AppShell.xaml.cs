using USOSmobile.SubPages;

namespace USOSmobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(PinPage), typeof(PinPage));

            Routing.RegisterRoute(nameof(MyUSOSPage), typeof(MyUSOSPage));
            Routing.RegisterRoute(nameof(ActivityGroupsPage), typeof(ActivityGroupsPage));
            Routing.RegisterRoute(nameof(GradesPage), typeof(GradesPage));
            Routing.RegisterRoute(nameof(SchedulePage), typeof(SchedulePage));
            Routing.RegisterRoute(nameof(ExamsPage), typeof(ExamsPage)); 
            
        }
    }
}
