using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Authenticators.OAuth;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace USOSmobile.Models
{
    class Auth
    {
        readonly string ApiKey = "CgjEJKEVYZJd6kNMguyV";
        readonly string ApiSecret = "dErz5k88FKwK7fwYg5D25qEKyJGt8Se3uxZqZVPn";
        readonly string BaseURL = "https://usosapi.polsl.pl";
        readonly string RequestTokenURL = "https://usosapi.polsl.pl/services/oauth/request_token";
        readonly string AuthorizeURL = "https://usosapi.polsl.pl/services/oauth/authorize";
        readonly string AccessTokenURL = "https://usosapi.polsl.pl/services/oauth/access_token";
        private List<Dictionary<dynamic, dynamic>> Scopes = new List<Dictionary<dynamic, dynamic>>();
        dynamic SignatureMethod = OAuthSignatureMethod.HmacSha1;

        public Auth() { }

        public void requestToken()
        {

            dynamic options = new RestClientOptions(BaseURL)
            {
                Authenticator = OAuth1Authenticator.ForRequestToken(ApiKey, ApiSecret)
            };
            RestRequest request = new RestRequest(RequestTokenURL, Method.Post);
            request.AddParameter("oauth_callback", "oob");

            RestClient client = new RestClient(options);
            RestResponse response = client.Execute(request);

            NameValueCollection pairs = new NameValueCollection();
            string[] keyValuePairs = response.Content.Split('&');
            foreach (string pair in keyValuePairs)
            {
                string[] keyValue = pair.Split('=');
                if (keyValue.Length == 2)
                {
                    pairs.Add(keyValue[0], keyValue[1]);
                }
            }

            string oauthToken = pairs["oauth_token"];
            string oauthTokenSecret = pairs["oauth_token_secret"];
            string oauthCallbackConfirmed = pairs["oauth_callback_confirmed"];

            string urlToOpen = AuthorizeURL + "?oauth_token=" + oauthToken;

        }
    }
}
