
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Jammer_1.Helpers;
using System.Net.Http;
using Jammer_1.Models;
using Newtonsoft.Json;
using Jammer_1.Services;
using Newtonsoft.Json.Linq;
using System.Data;
using Jammer_1.Views;

namespace Jammer_1.views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        bool authenticated = false;
        private UserViewModel viewModel;
        private AzureService azureService;

        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            

           
            InitializeComponent();
            BindingContext = viewModel = new UserViewModel();
        }


        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            try
            {

                if (App.Authenticator != null)
                {
                    authenticated = await App.Authenticator.AuthenticateAsync();
                }

                if (authenticated)
                {
                    ////check if user already exists in our table
                    //*need to check if the user exists already
                    //*if this user does not exist create a new one
                    //*either way go to the new page with the new created user or the old one
                    //
                    // then call next page 
                 var user  = await viewModel.getUser(Settings.FacebookAccessToken);
                    if (user== null)
                    {
                     //  messageLabel.Text = "You user is null";
                        create_new_user();
                        user = await viewModel.getUser(Settings.FacebookAccessToken);
                    }
                    //}
                    // messageLabel.Text = "Email of the user" + mamadou.ElementAt<RootObject>(0).user_claims.ElementAt<UserClaim>(4).val;///customer1.user_claims.ElementAt(0);
                    Navigation.InsertPageBefore(new MainPage(user), this);
                    //await Navigation.PushAsync(new MainSettings(user));
                    await Navigation.PopAsync();
                }
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("Authentication was cancelled"))
                {
           


                   messageLabel.Text = "Authentication cancelled by the user";
                }
            }
            catch (Exception)
            {
                messageLabel.Text = "Authentication failed";
            }
        }
        /// <summary>
        /// Creates a new user using Facebook information!
        /// </summary>
        public async void create_new_user()
        {
            var userId = Settings.FacebookAccessToken;
            var theid = userId.Substring(userId.IndexOf(':') + 1);


            var url = Constants.ApplicationURL + "/.auth/me";
            var clent = new HttpClient();
            clent.DefaultRequestHeaders.Add("X-ZUMO-AUTH", Settings.currentuser.MobileServiceAuthenticationToken);
            var userData = await clent.GetAsync(new Uri(url));
            var response = await userData.Content.ReadAsStringAsync();
            dynamic actualdata = JsonConvert.DeserializeObject(response.ToString());

            List<RootObject> converted_R = JsonConvert.DeserializeObject<List<RootObject>>(response, Converter.Settings);
            string firstname = converted_R.ElementAt<RootObject>(0).user_claims.ElementAt<UserClaim>(4).val;
            string lastname = converted_R.ElementAt<RootObject>(0).user_claims.ElementAt<UserClaim>(5).val;
            string email = converted_R.ElementAt<RootObject>(0).user_claims.ElementAt<UserClaim>(1).val;
            string facebookid = converted_R.ElementAt<RootObject>(0).user_claims.ElementAt<UserClaim>(0).val;
            string b_day = converted_R.ElementAt<RootObject>(0).user_claims.ElementAt<UserClaim>(7).val;
            User newuser = new User
            {
                FirstName = firstname,
                LastName = lastname,
                FacebookId = theid,
                Email = email,
                B_day = b_day,

            };

            await viewModel.ExecuteAddUserCommandAsync(newuser, "");
        }

        /////////////////////////////////////////////
        /// <summary>
        /// //////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>



        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            bool loggedOut = false;

            if (App.Authenticator != null)
            {
                loggedOut = await App.Authenticator.LogoutAsync();
            }
        }

        public class UserClaim
        {
            public string typ { get; set; }
            public string val { get; set; }
        }

        public class RootObject
        {
            public string access_token { get; set; }
            public DateTime expires_on { get; set; }
            public string provider_name { get; set; }
            public List<UserClaim> user_claims { get; set; }
            public string user_id { get; set; }
        }

        public class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
            };
        }

    }


}