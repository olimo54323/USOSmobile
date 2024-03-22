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
            Auth authObj = new();
            await SecureStorage.SetAsync("oauth_verifier", PINEntry.Text);
            await authObj.accessToken();
            

            //TestLabel.Text = await authObj.verifyLogin();
            User user = await authObj.verifyLogin();
            TestLabel.Text = $"{user.last_name + " " + user.first_name}";
         }

        private async void OnBrowserBtnClicked(object sender, EventArgs e)
        {
            Auth authObj = new();
            await authObj.requestToken();
            await authObj.PINAuthorization();
        }
    }
}
