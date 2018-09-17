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
using Syncfusion.ListView.XForms;

namespace Jammer_1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MatchSettings : ContentPage
	{
        public Command LoadMatchsettingsCommand { get; set; }
        public List<string> genres=Instrument.list_of_genre;

        private UserSettings viewModel;
        private User currentuser;
        public MatchSettings (User user)
		{
            this.currentuser = user;

            this.genres = Instrument.list_of_genre;
            LoadMatchsettingsCommand = new Command(async () => await ExecuteLoadInstrumentsCommand());
            viewModel = new UserSettings();
           InitializeComponent();
            genrepicker_1.Title = "Pick your Genre";
         

        }


        public async void onupdate()
        {
            try
            {
                var match_setting = await viewModel.getmatch_setting(currentuser.Id);
                if (match_setting == null)
                {
                    User_match_setttings addmein = new User_match_setttings()
                    {
                        User_id = currentuser.Id,
                        Minimumage = Convert.ToString(RangeSlider.LowerValue),
                        Maximumage = Convert.ToString(RangeSlider.UpperValue),
                        Instruments = "",
                        Genres = "",
                        Use_my_location = Convert.ToString(use_location.IsToggled),

                    };
                    await viewModel.azureService.Add_item_to_table(addmein);
                }
                else
                {
                    match_setting.Minimumage= Convert.ToString(RangeSlider.LowerValue);
                    match_setting.Maximumage = Convert.ToString(RangeSlider.UpperValue);
                    match_setting.Use_my_location = Convert.ToString(use_location.IsToggled);
                    await viewModel.azureService.Update_item_in_table(match_setting);

                }
               Navigation.InsertPageBefore(new MainPage(currentuser), this);
               await Navigation.PopAsync();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Match settings error saving", ex.Message, "ok");
            }
        }
        public async Task ExecuteLoadInstrumentsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                 var match_setting = await viewModel.getmatch_setting(currentuser.Id);
                    RangeSlider.LowerValue = float.Parse(match_setting.Minimumage);
                    RangeSlider.UpperValue = float.Parse(match_setting.Maximumage);
            if(match_setting != null) { 
                if (match_setting.Use_my_location.Equals("False"))
                {
                    use_location.IsToggled = false;
                }
           
                    parsestring parser = new parsestring(match_setting.Instruments);
                    var list = parser.Instrumentlist;
                    var genrelist = parser.parsegenre(match_setting.Genres);
                    updategenrelist(genrelist);
                    genrepicker_1.ItemsSource = genres;
                    genrelistview.ItemsSource = genrelist;
                    InstrumentsListView.ItemsSource = parser.Instrumentlist;
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("match settings", "error" + ex.Message, "ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
        async void onaddinstrument(object sender, EventArgs e)
        {
            try
            {
                var match_setting = await viewModel.getmatch_setting(currentuser.Id);
                if(match_setting.Id == null) { 
                User_match_setttings addmein = new User_match_setttings()
                {
                    User_id = currentuser.Id,
                    Minimumage = Convert.ToString(RangeSlider.LowerValue),
                    Maximumage = Convert.ToString(RangeSlider.UpperValue),
                    Instruments="",
                    Genres="",
                    Use_my_location = Convert.ToString(use_location.IsToggled),
                    
                };
                   await viewModel.azureService.Add_item_to_table(addmein);
                }

                await Navigation.PushAsync(new AddInstrument(currentuser, false));
            }
            catch(Exception ex)
            {

                await DisplayAlert("Match settings error adding", ex.Message, "ok");
            
            }
    
        }
        async void onaddgenre(object sender, EventArgs e)
        {
            try
            {
                var match_setting = await viewModel.getmatch_setting(currentuser.Id);
                if (match_setting != null)
                {
                    match_setting.Genres += genrepicker_1.SelectedItem.ToString() + ",";
                    await viewModel.azureService.Update_item_in_table(match_setting);
                }




                //LoadMatchsettingsCommand.Execute(null);
                await Navigation.PopToRootAsync();
               // await Navigation.PushAsync(new MatchSettings(currentuser));
            }
            catch (Exception ex)
            {

                await DisplayAlert("Match settings error adding", ex.Message, "ok");

            }
           

        }

        async void OnItemSelected(object sender, ItemHoldingEventArgs e)
        {
            var instrument = e.ItemData as Instrument;
            if (instrument == null)
                return;
            await Navigation.PushAsync(new Instrumentinfo(instrument, currentuser,false));

            // Manually deselect item
            InstrumentsListView.SelectedItem = null;
        }

        async void DeleteGenre(object sender, ItemHoldingEventArgs e)
        {
            genrelistview.SelectionBackgroundColor = Color.Red;
            var match_setting = await viewModel.getmatch_setting(currentuser.Id);
            var genre = e.ItemData as Genre;
            if (genre == null)
                return;
            var genres = match_setting.Genres;
            var newgenres = genres.Replace(genre.Name+",", "");
            match_setting.Genres = newgenres;
            await viewModel.azureService.Update_item_in_table(match_setting);
            // Manually deselect item
            genrelistview.SelectedItem = null;
            genrelistview.SelectionBackgroundColor = Color.Blue;
            //await Navigation.PushAsync(new MatchSettings(currentuser));
          
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadMatchsettingsCommand.Execute(null);
        }
        public void updategenrelist(List<Genre> genrelist)
        {
            try
            {
                for (int i = 0; i < genres.Count; i++)
                {
                    for (int j = 0; j < genrelist.Count; j++)
                    {
                        if (genres.ElementAt<string>(i).Equals(genrelist.ElementAt<Genre>(j).Name))
                        {
                            genres.Remove(genres.ElementAt<string>(i));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //  await DisplayAlert("mamadou said", ex.Message, "dayum");
            }

        }
        public void enablegenrelabels(string genre)
        {

        }

    }
}