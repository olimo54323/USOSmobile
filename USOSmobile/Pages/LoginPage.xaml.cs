using Microsoft.VisualBasic.FileIO;
using USOSmobile.Models;
using USOSmobile.SubPages;

namespace USOSmobile
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Task.Run(() =>
            {
                Login();
            });
        }


        private async void Login()
        {
            if (((await SecureStorage.GetAsync("oauth_token")) == null || (await SecureStorage.GetAsync("oauth_token_secret")) == null) || !(await ModelObjects.apiBrowser.getUser()))
            {
                await ModelObjects.apiBrowser.requestToken();
                await ModelObjects.apiBrowser.PINAuthorization();
                Shell.Current.GoToAsync($"//{nameof(PinPage)}");
            }
            else
            {
                Shell.Current.GoToAsync($"//{nameof(MyUSOSPage)}");
                await ModelObjects.apiBrowser.getCourses();
            }

        }
    }
}