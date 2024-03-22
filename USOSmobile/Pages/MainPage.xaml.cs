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
            APIBrowser APIObj = new();
            await APIObj.requestToken();
            await APIObj.PINAuthorization();
            App.Current.MainPage = new NavigationPage(new PinPage());
        }
    }
}