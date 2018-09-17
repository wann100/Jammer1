using System;
using System.Linq;
using Xamarin.Forms;
using Jammer_1.Models;
using Jammer_1.Helpers;
using Plugin.Connectivity;

namespace Jammer_1.Views
{

    public partial class testpage : ContentPage
    {
        UserViewModel viewModel;
        public testpage()
        {
            InitializeComponent();
            BindingContext = viewModel = new UserViewModel();
        }



        async void AddUser_Clicked(object sender, EventArgs e)
        {

            ///In here add a user then call add user from azure
            ///
            User newuser = new User
            {
                FirstName = "Testme",
                LastName = "Mwahahha",
                FacebookId = "some random id",
                Email = "test123@test.com",
                GoogleId = "some random id"
            };
        
            await viewModel.ExecuteAddUserCommandAsync(newuser, "Delaware");

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CrossConnectivity.Current.ConnectivityChanged += ConnecitvityChanged;
            if (viewModel.users.Count() == 0)
            {
                viewModel.LoadUsersCommand.Execute(null);
            }
    
        }

        void ConnecitvityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //OfflineStack.IsVisible = !e.IsConnected;
            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CrossConnectivity.Current.ConnectivityChanged -= ConnecitvityChanged;
        }


    }
}
