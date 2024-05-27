using USOSmobile.Models;

namespace USOSmobile.SubPages
{
    public partial class ActivityGroupsPage : ContentPage
    {
        public ActivityGroupsPage()
        {
            InitializeComponent();
        }
        private async void DataButttonClicked(object sender, EventArgs e)
        {
            string line = string.Empty;
            foreach (var item in ModelObjects.userCourses.Values)
            {
                line += item.Show();
            }
            ActivityGroupsLabel.Text = line;
        }
    }
}