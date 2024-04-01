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
            if (PINEntry.Text == null || PINEntry.Text.Length !=8)
            {
                IsLoggedLabel.Text = "Brak kodu PIN, spróbuj ponownie";
                return;
            }
            await SecureStorage.SetAsync("oauth_verifier", PINEntry.Text);
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
