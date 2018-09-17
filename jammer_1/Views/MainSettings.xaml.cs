using Jammer_1.Models;
using Jammer_1.Services;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Jammer_1.views;

namespace Jammer_1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainSettings : ContentPage
    {
        string uploadedFilename;
        byte[] byteData;

        User currentuser;
        public MainSettings(User currentuser)
        {

            this.currentuser = currentuser;
            showuserprofilepic();
            InitializeComponent();
            

        }

        async void showuserprofilepic()
        {
         
            try {
              
                var profilepic = await ImageManager.GetImage(currentuser.Id + ".png");
                if (profilepic != null)
                {
                    imageToUpload.Source = ImageSource.FromStream(() => new MemoryStream(profilepic));
                }
                if (profilepic == null)
                {
                    profilepic = await ImageManager.GetImage("stock_profile.png");
                    imageToUpload.Source = ImageSource.FromStream(() => new MemoryStream(profilepic));
                }
            }
          
            catch (Exception e)
            {
                await DisplayAlert("Cant get image", "error"+e.Message, "Ok");
            }
        
                // byte[] imageAsBytes = await ImageManager.GetImage(currentuser.FacebookId + ".png");
            
           
        }
        async void OnUpload(object sender, EventArgs e)
        {
            try
            {

                FileData filedata = await CrossFilePicker.Current.PickFile();
                await ImageManager.DeleteFileAsync(currentuser.Id + ".png");
                await ImageManager.UploadImage(new MemoryStream(filedata.DataArray), currentuser.Id + ".png");
                if (filedata != null)
                {
                    imageToUpload.Source = ImageSource.FromStream(() => new MemoryStream(filedata.DataArray));
                }
                // the dataarray of the file will be found in filedata.DataArray 
                // file name will be found in filedata.FileName;
                //etc etc.

            }
            catch (Exception ex)
            {
                await DisplayAlert("Cant get file", "error" + ex.Message, "Ok");
            }
        }
        async void onGoToAbout()
        {

            await Navigation.PushAsync(new About_userSettings(currentuser));
        }
       async void onGoToInstruments()
        {
            await Navigation.PushAsync(new Instruments_page(currentuser));

        }
        async void onGoToMatchSettings()
        {

            await Navigation.PushAsync(new MatchSettings(currentuser));
        }
        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            bool loggedOut = false;

            if (App.Authenticator != null)
            {
                loggedOut = await App.Authenticator.LogoutAsync();
            }
            //Gotta purge the information when you log out!!
            await AzureService.DefaultManager.purgetable("user_table");
            await AzureService.DefaultManager.purgetable("about_user_table");
            await AzureService.DefaultManager.purgetable("user_match_settings_table");
            await AzureService.DefaultManager.purgetable("instrument_table");
            //////////////
            await Navigation.PushAsync(new LoginPage());
        }
    }
}