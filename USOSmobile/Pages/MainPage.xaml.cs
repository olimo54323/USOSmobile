using Microsoft.VisualBasic.FileIO;
using USOSmobile.Models;

namespace USOSmobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginBtnClicked(object sender, EventArgs e)
        {
            Auth authObj = new();
            await authObj.requestToken();
            await authObj.PINAuthorization();
            App.Current.MainPage = new NavigationPage(new PinPage());
        }
    }
}