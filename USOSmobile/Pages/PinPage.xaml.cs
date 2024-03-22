using USOSmobile.Models;

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
            APIBrowser APIObj = new();
            await SecureStorage.SetAsync("oauth_verifier", PINEntry.Text);
            await APIObj.accessToken();
            

            //TestLabel.Text = await APIObj.verifyLogin();
            User user = await APIObj.getUser();
            TestLabel.Text = $"{user.last_name + " " + user.first_name}";
         }

        private async void OnBrowserBtnClicked(object sender, EventArgs e)
        {
            APIBrowser APIObj = new();
            await APIObj.requestToken();
            await APIObj.PINAuthorization();
        }
    }
}
