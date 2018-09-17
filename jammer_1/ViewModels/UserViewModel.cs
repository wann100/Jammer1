using Jammer_1.Helpers;
using Jammer_1.Services;
using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using MvvmHelpers;
using Jammer_1.Models;
using Xamarin.Forms;

namespace Jammer_1

{
    using Microsoft.WindowsAzure.MobileServices;
    using MvvmHelpers;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;

    class UserViewModel : BaseViewModel
    {
        AzureService azureService;
        MobileServiceUser logedin_info;



        public UserViewModel()
        {
            azureService = DependencyService.Get<AzureService>();
            LoadUsersCommand = new Command(async () => await ShowusersinfoCommandAsync("Delaware"));
            ///  AddUsersCommand = new Command(async () => await ExecuteAddCoffeeCommandAsync("Delaware"));

        }

        public ObservableRangeCollection<User> users { get; } = new ObservableRangeCollection<User>();


        public ObservableRangeCollection<Grouping<string, User>> Usersgrouped { get; } = new ObservableRangeCollection<Grouping<string, User>>();
        public Command LoadUsersCommand { get; set; }
        public Command AddUserCommand { get; set; }
        string loadingMessage;
        public string LoadingMessage
        {
            get { return loadingMessage; }
            set { SetProperty(ref loadingMessage, value); }
        }
        async Task ShowusersinfoCommandAsync(string state)
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                LoadingMessage = "Loading Users...";
                IsBusy = true;
                var users_table = await azureService.GetTable("user_table");
                users.ReplaceRange(users_table.Cast<User>());


            }
            catch (Exception ex)
            {
                Debug.WriteLine("OH NO!" + ex);

                await Application.Current.MainPage.DisplayAlert("Sync Error", "Unable to sync users, you may be offline", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }


        public async Task ExecuteAddUserCommandAsync(User user, string state)
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                LoadingMessage = "Adding User...";
                IsBusy = true;

                await azureService.Initialize("user_table");
                var token = new JObject();
                // logedin_info = await AzureService.LoginAsync(MobileServiceAuthenticationProvider.Facebook,);
                // user.FacebookId = logedin_info.UserId;

                if (Settings.AuthToken != null)
                {
                    user.FacebookId = Settings.FacebookAccessToken;

                }
                if (Settings.AuthToken == null)
                {
                    user.FacebookId = "Damn still empty";
                }
                var created_user = await azureService.Add_item_to_table(user);

                //Location = string.Empty;
                // AtHome = false;
                users.Add(user);
                Debug.WriteLine("Added" + created_user.ToString());
                //SortCoffees();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OH NO!" + ex);
            }
            finally
            {
                LoadingMessage = string.Empty;
                IsBusy = false;
            }

        }
        /// <summary>
        /// Call this on Login because it check if the facebookauth user is the user
        /// </summary>
        /// <param name="facebook_id">facebook auth</param>
        /// <returns></returns>
        public async Task<User> getUser(string facebook_id)
        {
            User returnthis = null;
            var users_table = await azureService.GetTable("user_table");
            var users = users_table.Cast<User>();
            for (int i = 0; i < users.Count(); i++)
            {
                if (users.ElementAt<User>(i).FacebookId.Equals(facebook_id))
                {
                    returnthis = users.ElementAt<User>(i);
                }
            }
            return returnthis;
        }

    }
}