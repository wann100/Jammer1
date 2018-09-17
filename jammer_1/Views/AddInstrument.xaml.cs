using Jammer_1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Jammer_1.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Jammer_1.Helpers;

namespace Jammer_1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
   /// private Instrument instrument;
   ///////////////////////////////////////////////////////////////////
    public partial class AddInstrument :PopupPage
    {
        private UserSettings settingsviewModel;
        private User_match_setttings match { get; set; }
        private User currentuser;
        public List<string> instruments = new List<string>();


        public bool settings_mode;
        public List<string> genres = new List<string>();


        public AddInstrument (User user,bool mode)
		{
            this.currentuser = user;
            this.settings_mode = mode;

            this.instruments = Instrument.list_of_instruments;
         
            this.genres = Instrument.list_of_genre;

            settingsviewModel = new UserSettings();

            InitializeComponent();

            instrumentpicker.ItemsSource = this.instruments;
            updateinstrumentlist(this.instruments);
            instrumentpicker.Title = "Pick your Instrument";
            ratinglabel.FontSize = 18;
           // genrepicker.ItemsSource = this.genres;
          //  genrepicker.Title = "Pick your Genre";
            genrepicker.IsVisible = false;
            if (settings_mode == false)
            {

                ratinglabel.Text = "Skill I am looking for";
                monthslabel.IsVisible = false;
                monthspicker.IsVisible = false;
               
            }
            if(settings_mode == true)
            {
                genrepicker.ItemsSource = Instrument.list_of_genre;
            }



        }
        public async void updateinstrumentlist(List <string> instrumentnames )
        {
            // List<Instrument> instruments = new List<Instrument>();
            if (settings_mode) { 
            var instruments= await settingsviewModel.Get_Instruments(currentuser.Id, "instrument_table");
            try {
            for(int i =0; i< instrumentnames.Count; i++)
            {
                for (int j = 0; j < instruments.Count; j++)
                {
                    if (instrumentnames.ElementAt<string>(i).Equals(instruments.ElementAt<Instrument>(j).Instrument_name))
                    {
                        instrumentnames.Remove(instrumentnames.ElementAt<string>(i));
                    }
                }
            }
            }
            catch(Exception ex)
            {
              //  await DisplayAlert("mamadou said", ex.Message, "dayum");
            }
            }
            if (!settings_mode)
            {
                var match_settings = await settingsviewModel.getmatch_setting(currentuser.Id);
                parsestring parser = new parsestring(match_settings.Instruments);
                var instrumentlist = parser.Instrumentlist;
                for (int i = 0; i < instrumentnames.Count; i++)
                {
                    for (int j = 0; j < instrumentlist.Count; j++)
                    {
                        if (instrumentnames.ElementAt<string>(i).Equals(instrumentlist.ElementAt<Instrument>(j).Instrument_name))
                        {
                            instrumentnames.Remove(instrumentnames.ElementAt<string>(i));
                        }
                    }
                }
            }

        
        }
        async void OnAddInstrument(object sender, EventArgs e)
        {
        

                if (settings_mode)
            {
                if (instrumentpicker.SelectedIndex >= 0 )
                {
                    Instrument newinstrument = new Instrument()
                    {
                        User_id = currentuser.Id,
                        Instrument_name = instrumentpicker.SelectedItem.ToString(),
                       // Genre_name = genrepicker.SelectedItem.ToString(),
                        Skill_rating = rating.Value.ToString(),
                        Time_playing_instrument = monthspicker.Date
                    };
                    await settingsviewModel.azureService.Add_item_to_table(newinstrument);
                    await Navigation.PushAsync(new Instruments_page(currentuser));
                }
                else
                {
                    Error.Text = "Please fill everything out";


                }
            }

            if (settings_mode == false)
            {
                if (instrumentpicker.SelectedIndex>=0)
                {
                    try
                    {
                        settingsviewModel = new UserSettings();
                        var match_setting = await settingsviewModel.getmatch_setting(currentuser.Id);
                       string updatethis = instrumentpicker.SelectedItem.ToString() + "," +rating.Value.ToString() + ";";
                        match_setting.Instruments += updatethis;
                        await settingsviewModel.azureService.Update_item_in_table(match_setting);
                        Navigation.InsertPageBefore(new MatchSettings(currentuser), this);
                        await Navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Add instrument for settings exception", ex.Message, "ok");

                    }
                }
                else
                {
                    Error.Text = "Please fill everything out";


                }
            }
            
       
        
        }
        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                instlabel.Text = (string)picker.ItemsSource[selectedIndex];
            }
        }
        protected override Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.5);
        }

        protected override Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1);
        }
        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
          ///  await Navigation.PopAllPopupAsync();
            await Navigation.PushAsync(new Instruments_page(currentuser));
           
        }
    

}
}