using System.Text.Json;
using System.Diagnostics;
using System.Web;
using RestSharp.Authenticators;
using RestSharp;
using Newtonsoft.Json;



namespace USOSmobile.Models
{
    internal class APIBrowser
    {
        private readonly string BaseURL = "https://usosapi.polsl.pl/";
        private readonly string RequestTokenURL = "https://usosapi.polsl.pl/services/oauth/request_token";
        private readonly string AuthorizeURL = "https://usosapi.polsl.pl/services/oauth/authorize";
        private readonly string AccessTokenURL = "https://usosapi.polsl.pl/services/oauth/access_token";

        private readonly string api_key = "CgjEJKEVYZJd6kNMguyV";
        private readonly string api_secret = "dErz5k88FKwK7fwYg5D25qEKyJGt8Se3uxZqZVPn";

        private readonly List<string> scopes = ["cards", "crstests", "email", "events", "grades", /*"offline_access", "payments",*/ "student_exams", "studies"];

        public RestResponse GetProtectedResource(string URLEndPoint,
                                                                                                        string customerKey,
                                                                                                        string customerSecret,
                                                                                                        string token,
                                                                                                        string tokenSecret,
                                                                                                        Method method,
                                                                                                        Dictionary<string, dynamic> data)
        {
            try
            {
                RestClientOptions options = new RestClientOptions(BaseURL)
                {
                    Authenticator = OAuth1Authenticator.ForProtectedResource(customerKey, customerSecret, token, tokenSecret)
                };
                RestRequest request = new RestRequest(BaseURL + URLEndPoint, method);
                if (data != null)
                    foreach (var item in data)
                    {
                        string value = (item.Value).ToString();
                        request.AddParameter(item.Key, value);
                    }
                RestClient client = new RestClient(options);
                return client.Execute(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("verifyLogin" + ex.ToString());
                return null;
            }
        }


        public async Task requestToken()
        {
            try
            {
                RestClientOptions options = new RestClientOptions(BaseURL)
                {
                    Authenticator = OAuth1Authenticator.ForRequestToken(api_key, api_secret)
                };
                RestRequest request = new RestRequest(RequestTokenURL, Method.Post);
                request.AddParameter("oauth_callback", "oob");
                request.AddParameter("scopes", scopes.Aggregate((current, next) => $"{current}|{next}"));

                RestClient client = new RestClient(options);
                RestResponse response = client.Execute(request);

                dynamic parameters = HttpUtility.ParseQueryString(response.Content);

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
            string urlToOpen = AuthorizeURL + "?oauth_token=" + await SecureStorage.GetAsync("oauth_token");
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
                dynamic options = new RestClientOptions(BaseURL)
                {
                    Authenticator = OAuth1Authenticator.ForAccessToken(api_key,
                                                                                                                api_secret,
                                                                                                                await SecureStorage.GetAsync("oauth_token"),
                                                                                                                await SecureStorage.GetAsync("oauth_token_secret"),
                                                                                                                await SecureStorage.GetAsync("oauth_verifier"))
                };
                RestRequest request = new RestRequest(AccessTokenURL, Method.Post);
                RestClient client = new RestClient(options);
                RestResponse response = client.Execute(request);

                dynamic parameters = HttpUtility.ParseQueryString(response.Content);

                await SecureStorage.SetAsync("oauth_token", parameters["oauth_token"]);
                await SecureStorage.SetAsync("oauth_token_secret", parameters["oauth_token_secret"]);
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("accessToken" + ex.ToString());
            }
         }

        public async Task<bool> getUser(Dictionary<string, dynamic>? parameters = null)
        {
            try
            {
                Dictionary<string, dynamic> localParameters = new Dictionary<string, dynamic>();

                if (parameters == null)
                {
                    localParameters["fields"] = "id|first_name|last_name";
                    localParameters["format"] = "json";
                }
                else
                    localParameters = parameters;

                RestResponse response = GetProtectedResource("services/users/user",
                                                        api_key,
                                                        api_secret,
                                                        await SecureStorage.GetAsync("oauth_token"),
                                                        await SecureStorage.GetAsync("oauth_token_secret"),
                                                        Method.Post,
                                                        localParameters);
                
                if(response.IsSuccessful)
                {
                    Helpers.user = Helpers.user.deserializeUserData(response);
                    return true;
                }
                else
                    return false;


            }
            catch (Exception ex)
            {
                Debug.WriteLine("verifyLogin" + ex.ToString());
                return false;
            }
        }

        public async Task<User> getGrades(Dictionary<string, dynamic>? parameters = null)
        {
            try
            {
                Dictionary<string, dynamic> localParameters = new Dictionary<string, dynamic>();
                if (parameters == null)
                {
                    localParameters["format"] = "json";
                }
                else
                    localParameters = parameters;
                

                RestResponse response = GetProtectedResource("services/users/user",
                                                        api_key,
                                                        api_secret,
                                                        await SecureStorage.GetAsync("oauth_token"),
                                                        await SecureStorage.GetAsync("oauth_token_secret"),
                                                        Method.Post,
                                                        localParameters);

                User user = JsonConvert.DeserializeObject<User>(response.Content);
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("getGrages" + ex.ToString());
                return null;
            }
        }

        public async Task<string> getGroupsIdParticipant(Dictionary<string, dynamic>? parameters = null)
        {
            try
            {
                Dictionary<string, dynamic> localParameters = new Dictionary<string, dynamic>();
                if (parameters == null)
                {
                    localParameters["active_terms"] = "false";
                    localParameters["lang"] = "pl";
                    localParameters["format"] = "json";
                }
                else
                    localParameters = parameters;
               

                RestResponse response = GetProtectedResource("services/groups/user",
                                                        api_key,
                                                        api_secret,
                                                        await SecureStorage.GetAsync("oauth_token"),
                                                        await SecureStorage.GetAsync("oauth_token_secret"),
                                                        Method.Post,
                                                        localParameters);

                return response.Content.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("getGroups" + ex.ToString());
                return null;
            }
        }

        public async Task<bool>getCourses(Dictionary<string, dynamic>? parameters = null)
        {
            try
            {
                Dictionary<string, dynamic> localParameters = new Dictionary<string, dynamic>();
                if (parameters == null)
                {
                    //localParameters["active_terms"] = "false";
                    localParameters["lang"] = "pl";
                    localParameters["format"] = "json";
                }
                else
                    localParameters = parameters;


                RestResponse response = GetProtectedResource("services/courses/user",
                                                        api_key,
                                                        api_secret,
                                                        await SecureStorage.GetAsync("oauth_token"),
                                                        await SecureStorage.GetAsync("oauth_token_secret"),
                                                        Method.Post,
                                                        localParameters);
                if (response.IsSuccessful)
                {
                    Helpers.userCourses = Helpers.userCourses.deserializeCoursesData(response);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("getGroups" + ex.ToString());
                return false;
            }
        }


        public async Task<string> getDiagnosticData(string URLendpoint, Dictionary<string, dynamic>? parameters = null)
        {
            try
            {
                Dictionary<string, dynamic> localParameters = new Dictionary<string, dynamic>();
                if (parameters == null)
                    localParameters["format"] = "json";
                else
                    localParameters = parameters;


                RestResponse response = GetProtectedResource(URLendpoint,
                                                        api_key,
                                                        api_secret,
                                                        await SecureStorage.GetAsync("oauth_token"),
                                                        await SecureStorage.GetAsync("oauth_token_secret"),
                                                        Method.Post,
                                                        localParameters);

                return response.Content.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("getGroups" + ex.ToString());
                return null;
            }
        }
    }
}
