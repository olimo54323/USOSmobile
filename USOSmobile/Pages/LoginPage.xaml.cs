using Microsoft.VisualBasic.FileIO;
using USOSmobile.Models;

namespace USOSmobile
{
    
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginBtnClicked(object sender, EventArgs e)
        {
            await Helpers.apiBrowser.PINAuthorization();
            await Helpers.apiBrowser.requestToken();
            Shell.Current.GoToAsync($"//{nameof(PinPage)}");
        }
    }
}