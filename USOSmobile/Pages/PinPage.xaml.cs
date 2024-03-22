using USOSmobile.Models;
using USOSmobile.SubPages;

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
            await SecureStorage.SetAsync("oauth_verifier", PINEntry.Text != "" ? PINEntry.Text : "");
            await Helpers.apiBrowser.accessToken();
            bool isLogged = await Helpers.apiBrowser.getUser();
            if (isLogged)
                await Shell.Current.GoToAsync($"//{nameof(MyUSOSPage)}");
            else
                IsLoggedLabel.Text = "Zły kod PIN, spróbuj ponownie";
        }

        private async void OnBrowserBtnClicked(object sender, EventArgs e)
        {
            await Helpers.apiBrowser.requestToken();
            await Helpers.apiBrowser.PINAuthorization();
        }
    }
}
