using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;

namespace ObtainToken
{
    internal class Program
    {
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string appKey = ConfigurationManager.AppSettings["ida:AppKey"];
        private static string authority = string.Format(CultureInfo.InvariantCulture, aadInstance, tenant);        
        private static string resourceId = ConfigurationManager.AppSettings["ida:ResourceId"];
        private static string baseAddress = ConfigurationManager.AppSettings["ida:BaseAddress"];
        private static string authString = "https://login.windows.net/" + tenant;

        private static void Main(string[] args)
        {
            /*var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("resource", resourceId),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("scope", "openid"),
                new KeyValuePair<string, string>("redirect_uri", "https://idscan.org.uk/TestObtainCredentials"),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", "test@bmsallatiidscanco.onmicrosoft.com"),
                new KeyValuePair<string, string>("password", "Ruha5230"),
            });

            using (var client = new HttpClient())
            {
                var task = client.PostAsync("https://login.microsoftonline.com/" + tenant + "/oauth2/token", formContent);
                task.Wait();
                var result = task.Result;
                result.EnsureSuccessStatusCode();

                Task<string> resultStr = task.Result.Content.ReadAsStringAsync();
                resultStr.Wait();

                dynamic magic = JsonConvert.DeserializeObject(resultStr.Result);

                var acceee = magic["access_token"];

            }*/

            try
            {
                var authContext = new AuthenticationContext(authority);
                var uc = new UserCredential("test1@bmsallatiidscanco.onmicrosoft.com", "W@123123");
                var result = authContext.AcquireToken(resourceId, clientId, uc);
                Console.WriteLine(result.AccessToken);



                var serviceRoot = new Uri("https://graph.windows.net/" + tenant);
                var adClient = new ActiveDirectoryClient(serviceRoot, async () => await GetAppTokenAsync());
                var userLookup = adClient.Users.Where(user => user.ObjectId.Equals("6396b6ad-a861-4791-adfc-1eb95e2ddf67", StringComparison.CurrentCultureIgnoreCase)).ExecuteAsync().Result;
                var userLookupResult = userLookup.CurrentPage.ToList();
                var userGraph = (User)userLookupResult.FirstOrDefault();






                /*string requestUrl = String.Format(CultureInfo.InvariantCulture, "https://graph.windows.net/contoso.com/users/{0}?api-version=2013-04-05", "6396b6ad-a861-4791-adfc-1eb95e2ddf67");
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", GetAppToken());
                HttpResponseMessage response = client.SendAsync(request).Result;*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }

        private static async Task<string> GetAppTokenAsync()
        {
            // Instantiate an AuthenticationContext for my directory (see authString above).
            AuthenticationContext authenticationContext = new AuthenticationContext(authString, false);

            // Create a ClientCredential that will be used for authentication.
            // This is where the Client ID and Key/Secret from the Azure Management Portal is used.
            ClientCredential clientCred = new ClientCredential("e87cf4ac-d66b-4ab0-bc73-e590f1845e26", "RVn67B/QZBvwmAxcPPEPkzWPgttB+NlGN0B7UBt7fP0=");

            // Acquire an access token from Azure AD to access the Azure AD Graph (the resource)
            // using the Client ID and Key/Secret as credentials.
            AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenAsync("https://graph.windows.net", clientCred);

            // Return the access token.
            return authenticationResult.AccessToken;
        }
    }
}

