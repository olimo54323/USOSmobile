using System.Text.Json;
using System.Diagnostics;
using System.Web;
using RestSharp.Authenticators;
using RestSharp;
using Newtonsoft.Json;



namespace USOSmobile.Models
{
    internal class Auth
    {
        private readonly string ApiKey = "CgjEJKEVYZJd6kNMguyV";
        private readonly string ApiSecret = "dErz5k88FKwK7fwYg5D25qEKyJGt8Se3uxZqZVPn";

        private readonly string BaseURL = "https://usosapi.polsl.pl";
        private readonly string RequestTokenURL = "https://usosapi.polsl.pl/services/oauth/request_token";
        private readonly string AuthorizeURL = "https://usosapi.polsl.pl/services/oauth/authorize";
        private readonly string AccessTokenURL = "https://usosapi.polsl.pl/services/oauth/access_token";

        //static private string? OAuthToken;
        //static private string? OAuthTokenSecret;
        //static private string? OAuthCallbackConfirmed;
        //static public string? OAuthVerifier; //ZMIANA  



        //nieużywane - TODO
        //private List<Dictionary<dynamic, dynamic>> Scopes = new List<Dictionary<dynamic, dynamic>>();

        public async Task requestToken()
        {
            try
            {
                RestClientOptions options = new RestClientOptions(BaseURL)
                {
                    Authenticator = OAuth1Authenticator.ForRequestToken(ApiKey, ApiSecret)
                    //dodać scopy
                };
                RestRequest request = new RestRequest(RequestTokenURL, Method.Post);
                request.AddParameter("oauth_callback", "oob");

                RestClient client = new RestClient(options);
                RestResponse response = client.Execute(request);

                dynamic parameters = HttpUtility.ParseQueryString(response.Content);

                //OAuthToken = parameters["oauth_token"];
                //OAuthTokenSecret = parameters["oauth_token_secret"];
                //OAuthCallbackConfirmed = parameters["oauth_callback_confirmed"];

                await SecureStorage.SetAsync("oauth_token", parameters["oauth_token"]);
                await SecureStorage.SetAsync("oauth_token_secret", parameters["oauth_token_secret"]);
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("requestToken" + ex.ToString());
            }
        }

        public async Task PINAuthorization()
        {
            string oauthToken = await SecureStorage.GetAsync("oauth_token");

            //string oauthToken = OAuthToken;

            string urlToOpen = AuthorizeURL + "?oauth_token=" + oauthToken;
            try
            {
                Uri uri = new Uri(urlToOpen);
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("PinAuthorization" +ex.ToString());
            }
        }

        public async Task accessToken()
        {
            try
            {
                string oauthToken = await SecureStorage.GetAsync("oauth_token");
                string oauthTokenSecret = await SecureStorage.GetAsync("oauth_token_secret");
                string oauthVerifier = await SecureStorage.GetAsync("oauth_verifier");

                //string oauthToken = OAuthToken;
                //string oauthTokenSecret = OAuthTokenSecret;
                //string oauthVerifier = OAuthVerifier;

                dynamic options = new RestClientOptions(BaseURL)
                {
                    Authenticator = OAuth1Authenticator.ForAccessToken(ApiKey, ApiSecret, oauthToken, oauthTokenSecret, oauthVerifier)
                };
                RestRequest request = new RestRequest(AccessTokenURL, Method.Post);
                RestClient client = new RestClient(options);
                RestResponse response = client.Execute(request);

                dynamic parameters = HttpUtility.ParseQueryString(response.Content);

                //OAuthToken = parameters["oauth_token"];
                //OAuthTokenSecret = parameters["oauth_token_secret"];

                //await SecureStorage.SetAsync("oauth_token", parameters["oauth_token"]);
                //await SecureStorage.SetAsync("oauth_token_secret", parameters["oauth_token_secret"]);

                await SecureStorage.SetAsync("access_token", parameters["oauth_token"]);
                await SecureStorage.SetAsync("access_token_secret", parameters["oauth_token_secret"]);
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("accessToken" + ex.ToString());
            }
         }

        public async Task<User> verifyLogin()
        {
            try
            {
                string oauthToken = await SecureStorage.GetAsync("access_token");
                string oauthTokenSecret = await SecureStorage.GetAsync("access_token_secret");

                //string oauthToken = OAuthToken;
                //string oauthTokenSecret = OAuthTokenSecret;

                dynamic options = new RestClientOptions(BaseURL)
                {
                    Authenticator = OAuth1Authenticator.ForProtectedResource(ApiKey, ApiSecret, oauthToken, oauthTokenSecret)
                };

                RestRequest request = new RestRequest(BaseURL + "/services/users/user");
                request.AddParameter("format", "json");
                
                RestClient client = new RestClient(options);
                RestResponse response = client.Execute(request);
                User user = JsonConvert.DeserializeObject<User>(response.Content);
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("verifyLogin" + ex.ToString());
                return new User();
            }
        }
    }
}
