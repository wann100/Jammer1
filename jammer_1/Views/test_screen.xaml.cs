using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jammer_1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Jammer_1.Models;

namespace Jammer_1.Views
{
	
	public partial class test_screen : ContentPage
	{
		public test_screen ()
		{
			InitializeComponent();
            BindingContext viewModel = new UserViewModel();
        }
        

        async void AddUser_Clicked(object sender, EventArgs e)
        {

            ///In here add a user then call add user from azure
            ///
            var newuser = new User();
            newuser.FirstName = "Testme";
            newuser.LastName = "Mwahahha";
            newuser.FacebookId = "some random id";
            newuser.Email = "test123@test.com";
            newuser.GoogleId = "some random id";


            Services.AzureService azureService;

   
            var created_user = await azureService.Add_item_to_table(newuser, "Delaware");
            ViewModel.users.Add(created_user);
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.ShowusersinfoCommandAsync("Delaware").Execute(null);
        }
    }
}