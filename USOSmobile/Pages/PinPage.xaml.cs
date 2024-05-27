using USOSmobile.Models;
using USOSmobile.SubPages;
using System.Text.RegularExpressions;

namespace USOSmobile
{
    public partial class PinPage : ContentPage
    {
        public PinPage()
        {
            InitializeComponent();
        }

        private async void OnLoginBtnClicked(object sender, EventArgs e)
        {
            
            if (!Regex.IsMatch(PINEntry.Text, @"^[0-9]{8}$"))
            {
                IsLoggedLabel.Text = "Zły kod PIN, spróbuj ponownie";
                return;
            }
            await SecureStorage.SetAsync("oauth_verifier", PINEntry.Text);
            await ModelObjects.apiBrowser.accessToken();
            bool isLogged = await ModelObjects.apiBrowser.getUser();
            if (isLogged)
            {
                await Shell.Current.GoToAsync($"//{nameof(MyUSOSPage)}");
                await ModelObjects.apiBrowser.getCourses();
            }
            else
                IsLoggedLabel.Text = "Zły kod PIN, spróbuj ponownie";
        }

        private async void OnBrowserBtnClicked(object sender, EventArgs e)
        {
            await ModelObjects.apiBrowser.requestToken();
            await ModelObjects.apiBrowser.PINAuthorization();
        }
    }
}
