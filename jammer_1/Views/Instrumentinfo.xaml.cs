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

namespace Jammer_1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Instrumentinfo : PopupPage
	{/// <summary>
    /// This is the edit instrumentinfopage
    /// </summary>
        private Instrument currentinstrument;
        private UserSettings viewModel;
        private User currentuser;
        public DateTime thedate;
        bool selectedmode;
        public Instrumentinfo (Instrument instrument,User user, bool mode)
		{
            this.currentinstrument = instrument;
            this.currentuser = user;
            this.selectedmode = mode;
            //NavigationPage.SetHasNavigationBar( false);
            InitializeComponent ();
            
            viewModel = new UserSettings();
            setinformation();
            if (!selectedmode)
            {
                monthspicker.IsVisible = false;
                monthslabel.IsVisible = false;
            }

            }
        void setinformation()
        {
            try {
                if (selectedmode) { 
        
              //  DateTime mydate = new DateTime();
              // mydate.
                    monthspicker.Date =currentinstrument.Time_playing_instrument.Date;
                }
                rating.Value = Convert.ToDouble(currentinstrument.Skill_rating);
                InstrumentName.Text = currentinstrument.Instrument_name;
            }
            catch (Exception ex)
            {
                DisplayAlert("eroor", ex.Message + currentinstrument.Instrument_name, "ok");
            }
        }
        async void onupdate()
        {if (selectedmode) {
            currentinstrument.Skill_rating = rating.Value.ToString();
            currentinstrument.Time_playing_instrument = monthspicker.Date;
            await viewModel.azureService.Update_item_in_table(currentinstrument);
            await Navigation.PushAsync(new Instruments_page(currentuser));
            }
            if (!selectedmode)
            {
                var matchsetting = await viewModel.getmatch_setting(currentuser.Id);
                var instruments = matchsetting.Instruments;
                var getmyindex = currentinstrument.Instrument_name + "," + currentinstrument.Skill_rating + ";";
                var newinstruments =  instruments.Replace(currentinstrument.Instrument_name + "," + currentinstrument.Skill_rating + ";", "");
                newinstruments += currentinstrument.Instrument_name+ "," + rating.Value.ToString() + ";";
                matchsetting.Instruments = newinstruments;
                await viewModel.azureService.Update_item_in_table(matchsetting);
                await Navigation.PushAsync(new MatchSettings(currentuser));



            }
        }
        async void ondelete(object sender, EventArgs e)
        {
            if (selectedmode) { 
            await viewModel.azureService.Delete_Item_from_table(currentinstrument);
            await Navigation.PushAsync(new Instruments_page(currentuser));
            }
            if (!selectedmode)
            {
                var matchsetting = await viewModel.getmatch_setting(currentuser.Id);
                var instruments = matchsetting.Instruments;
                var newinstruments = instruments.Replace(currentinstrument.Instrument_name + "," + currentinstrument.Skill_rating + ";","");
                matchsetting.Instruments = newinstruments;
                await viewModel.azureService.Update_item_in_table(matchsetting);
                await Navigation.PushAsync(new MainSettings(currentuser));
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
            ///  
            await PopupNavigation.RemovePageAsync(this);
          //  await Navigation.PushAsync(new Instruments_page(currentuser));

        }

    }
}