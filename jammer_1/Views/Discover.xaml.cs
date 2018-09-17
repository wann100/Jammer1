using Jammer_1.Helpers;
using Jammer_1.Models;
using Jammer_1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jammer_1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Discover : ContentPage
	{
        DiscoveryViewModel viewModel;
        public Command LoadUsersCommand { get; set; }
        UserSettings mysettingsVM;
        List<superuser> user_list = new List<superuser>();
       // parsestring parser = new parsestring("");
        User current_user;
        
		public Discover (User user)
		{
			
            this.current_user = user;
            viewModel = new DiscoveryViewModel(user);
            mysettingsVM = new UserSettings();
            LoadUsersCommand = new Command(async () => await ExecuteLoaUsersCommand());
            BindingContext = LoadUsersCommand;
            
            
           
            InitializeComponent();

        }

        public async Task ExecuteLoaUsersCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
             
                var about_me = await mysettingsVM.getabout_user_item(current_user.Id);
                //if (about_me != null) {
                user_list = await viewModel.get_users_near(about_me.Location);
                    user_list = viewModel.get_users_by_genres(user_list);
                UsersListView.ItemsSource = user_list;
               // }


            }
            catch (Exception ex)
            {
               // await DisplayAlert("Discovery settings", "error" + ex.Message, "ok");
            }
            finally
            {
                IsBusy = false;
            }
        }


        public async Task testing()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {


                await mysettingsVM.azureService.Initialize("about_user_table");
                var about_me = await mysettingsVM.getabout_user_item(current_user.Id);
                user_list = await viewModel.get_users_near(about_me.Location);
                user_list = viewModel.get_users_by_genres(user_list);
                UsersListView.ItemsSource = user_list;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Discovery settings", "error" + ex.Message, "ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadUsersCommand.Execute(null);
        }
    }
}