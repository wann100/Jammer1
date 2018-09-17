using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Jammer_1.ViewModels;
using Jammer_1.Models;
using Jammer_1.Services;
using Plugin.Geolocator;
using Jammer_1.Helpers;

namespace Jammer_1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class About_userSettings : ContentPage
	{
        private User currentuser;
        
        private UserSettings viewModel;
        private LocationHelper location;
       /// private AzureService azureService;
        public About_userSettings (User currentuser)
		{
            this.currentuser = currentuser;
           // 
            InitializeComponent ();
            location = new LocationHelper();
            BindingContext = viewModel = new UserSettings();
            showcurrentinfo();
        }

        async void OnUpdateinfo()
        {
            try { 
            var about_user = await viewModel.getabout_user_item(currentuser.Id);
            if (about_user != null)
            {
                about_user.About_me = about_me.Text;
                about_user.Prefered_genre = prefered_genre.Text;
                about_user.Video_link_1 = video_link_1.Text;
                about_user.Sample_link_1 = sample_link_1.Text;
                about_user.Sample_link_2 = sample_link_2.Text;
                about_user.Influences = influences.Text;
                //  await DisplayAlert("updating", about_user.About_me, "Ok");
                 await viewModel.azureService.Update_item_in_table(about_user);
            }
            if (about_user== null) { 

                await create_about_user();
                    await DisplayAlert("creating", about_user.About_me, "Ok");
                }
            }
        catch (Exception ex)
            {
                await DisplayAlert("About info ", "error" + ex.Message, "Ok");
            }
        //    if (ifadded) {
        Navigation.InsertPageBefore(new MainSettings(currentuser), this);
            //await Navigation.PushAsync(new MainSettings(user));
            await Navigation.PopAsync();
        //    }
        }

        async void showcurrentinfo()
        {
            try
            {
                var about_user = await viewModel.getabout_user_item(currentuser.Id);
                if (about_user != null)
                {
                    influences.Text = about_user.Influences;
                    about_me.Text = about_user.About_me;
                    prefered_genre.Text = about_user.Prefered_genre;
                    sample_link_1.Text = about_user.Sample_link_1;
                    sample_link_2.Text = about_user.Sample_link_2;
                    video_link_1.Text = about_user.Video_link_1;

                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("show current info", "error" + ex.Message, "Ok");
            }
         
       
        }
       async Task create_about_user()
        {

            About_user newabout = new About_user
            {
                User_id = currentuser.Id,
                About_me = about_me.Text,
                Prefered_genre = prefered_genre.Text,
                Video_link_1 = video_link_1.Text,
                Sample_link_1 = sample_link_1.Text,
                Sample_link_2 = sample_link_2.Text,
                Influences = influences.Text,
                Location = location.Myposition.Longitude + "," + location.Myposition.Latitude,

            };

            await viewModel.ExecuteAddusersettingCommandAsync(newabout,"about_user_table");


        }
        async void onCancel(object sender, EventArgs e)
        {
            //}
            // messageLabel.Text = "Email of the user" + mamadou.ElementAt<RootObject>(0).user_claims.ElementAt<UserClaim>(4).val;///customer1.user_claims.ElementAt(0);
            Navigation.InsertPageBefore(new MainSettings(currentuser), this);
            //await Navigation.PushAsync(new MainSettings(user));
            await Navigation.PopAsync();
        }

        async void update_location(object sender, EventArgs e)
        {
            try
            {
                var about_user = await viewModel.getabout_user_item(currentuser.Id);
                if (about_user != null)
                {
                    about_user.About_me = about_me.Text;
                    about_user.Prefered_genre = prefered_genre.Text;
                    about_user.Video_link_1 = video_link_1.Text;
                    about_user.Sample_link_1 = sample_link_1.Text;
                    about_user.Sample_link_2 = sample_link_2.Text;
                    about_user.Influences = influences.Text;
                    about_user.Location = location.Myposition.Longitude + "," + location.Myposition.Latitude;
                    //  await DisplayAlert("updating", about_user.About_me, "Ok");
                    await viewModel.azureService.Update_item_in_table(about_user);

                }
                if(about_user == null)
                {
                    await create_about_user();
                }
                Navigation.InsertPageBefore(new MainSettings(currentuser), this);
                //await Navigation.PushAsync(new MainSettings(user));
                await Navigation.PopAsync();
            }
            catch(Exception ex)
            {
                await DisplayAlert("update location", "error" + ex.Message, "Ok");
            }
            }
    }
}