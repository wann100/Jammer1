using Jammer_1.Helpers;
using Jammer_1.Services;
using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using MvvmHelpers;
using Jammer_1.Models;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;

using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Jammer_1.ViewModels
{
    class UserSettings: BaseViewModel
    {
        public AzureService azureService { get; }

       

        public UserSettings()
        {
            azureService = new AzureService();

        }

    
        string loadingMessage;
        public string LoadingMessage
        {
            get { return loadingMessage; }
            set { SetProperty(ref loadingMessage, value); }
        }


        public async Task ExecuteAddusersettingCommandAsync(Object item,string name_of_table)
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                LoadingMessage = "Adding..."+name_of_table;
                IsBusy = true;
                    await azureService.Initialize(name_of_table);
                var created_user = await azureService.Add_item_to_table(item);

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

        public async Task<About_user> getabout_user_item(string user_id)
        {
            About_user returnthis = null;
            
             await azureService.Initialize("about_user_table");
            var aboutuser_table = await azureService.GetTable("about_user_table");
            
                var about_users = aboutuser_table.Cast<About_user>(); 

            for (int i = 0; i < about_users.Count(); i++)
            {
                if (about_users.ElementAt<About_user>(i).User_id.Equals(user_id))
                {
                  returnthis =about_users.ElementAt<About_user>(i);
                }
            }
            return returnthis;
        }
        public async Task<User_match_setttings> getmatch_setting(string user_id)
        {
            User_match_setttings returnthis = new User_match_setttings();

            await azureService.Initialize("user_match_settings_table");
            var match_settings_table = await azureService.GetTable("user_match_settings_table");

            var match_settings = match_settings_table.Cast<User_match_setttings>();

            for (int i = 0; i < match_settings.Count(); i++)
            {
                if (match_settings.ElementAt<User_match_setttings>(i).User_id.Equals(user_id))
                {
                   return match_settings.ElementAt<User_match_setttings>(i);
                }
            }
            return returnthis;
        }
        public async Task<List <Instrument>> Get_Instruments(string user_id, string table_name)
        {
            List<Instrument> returnthis = new List<Instrument>();
            var instruments_table = await azureService.GetTable(table_name);
            var instruments = instruments_table.Cast<Instrument>();
            for (int i = 0; i < instruments.Count(); i++)
            {
                if (instruments.ElementAt<Instrument>(i).User_id.Equals(user_id))
                {
                    returnthis.Add(instruments.ElementAt<Instrument>(i));
                }
            }
            return returnthis;
        }

    }
}
