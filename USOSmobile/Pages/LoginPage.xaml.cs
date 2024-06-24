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
            if ((await SecureStorage.GetAsync("oauth_token")) != null && (await SecureStorage.GetAsync("oauth_token_secret")) != null) {
                Shell.    
            }
            else
            {
                await ModelObjects.apiBrowser.requestToken();
                await ModelObjects.apiBrowser.PINAuthorization();
                Shell.Current.GoToAsync($"//{nameof(PinPage)}");
            }
        }
    }
}