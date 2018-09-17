using Jammer_1.Models;
using Jammer_1.Services;
using System;
using System.Collections.Generic;
using MvvmHelpers;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Jammer_1.Helpers;

namespace Jammer_1.ViewModels
{
    class DiscoveryViewModel:BaseViewModel
    {
        User currentuser;
        AzureService azureService;
        UserSettings settingsviewmodel;
        public string message ="Empty";
        User_match_setttings my_settings;
        public DiscoveryViewModel(User user)
        {
            this.currentuser = user;
            settingsviewmodel = new UserSettings();
            azureService = DependencyService.Get<AzureService>();

        }
        async void initialize_settings()
        {
           
            my_settings = await settingsviewmodel.getmatch_setting(currentuser.Id);
     

        }
        public List <superuser> get_users_by_genres(List <superuser> list_of_users)
        {
            initialize_settings();
            for (int i = 0; i < list_of_users.Count(); i++)
            {
                if(!my_settings.Genres.Contains(list_of_users.ElementAt<superuser>(i).About_me.Prefered_genre))
                {
                    list_of_users.Remove(list_of_users.ElementAt<superuser>(i));
                }
            }
           
            


            return list_of_users;
        }

        public async Task <List<superuser>> get_users_near(string mylocation)
        {
            initialize_settings();
            List<superuser> user_list = new List<superuser>();
            await azureService.Initialize("user_table");
            var users_table = await azureService.GetTable("user_table");
            List<string> currentuserlocation = new List<string>();
            /////////////////////////////////////////////////////////////////
            var users = users_table.Cast<User>();
            azureService = new AzureService();
            await azureService.Initialize("about_user_table");
            var aboutuser_table = await azureService.GetTable("about_user_table");
            var about_users = aboutuser_table.Cast<About_user>();
            ///////////////////////////////////////////////////////////////////
            if (my_settings.Use_my_location.Equals("True"))
            {
                 currentuserlocation = parsestring.parselocation(mylocation);
            }
            if (my_settings.Use_my_location.Equals("False"))
            {
                currentuserlocation = parsestring.parselocation(currentuser.Location);
            }
            ////////////////////////////////////////////////////////////////////
            for (int i =0;i<users.Count(); i++)
            {
                for (int j = 0; j < about_users.Count(); j++)
                {
                    if (!users.ElementAt<User>(i).Id.Equals(currentuser.Id)) {
                        if (users.ElementAt<User>(i).Id.Equals(about_users.ElementAt<About_user>(j).User_id))
                        {
                            List<string> location = parsestring.parselocation(about_users.ElementAt<About_user>(j).Location);
                            if (distance(Convert.ToDouble(location.ElementAt<string>(1)), Convert.ToDouble(location.ElementAt<string>(0)), Convert.ToDouble(currentuserlocation.ElementAt<string>(1)), Convert.ToDouble(currentuserlocation.ElementAt<string>(0)),'M') <= 30.00){
                                superuser newsuper = new superuser();
                                message = "trying to add user to list" + users.ElementAt<User>(i).FirstName;
                                newsuper.About_me = about_users.ElementAt<About_user>(j);
                                newsuper.Current_user = users.ElementAt<User>(i);
                                user_list.Add(newsuper);
                            }
                         
                        }

                    }
                }
            }
            /////////////////////////////////////////////////////////////////////////
            return user_list;
        }




        public double distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }


    }
}
