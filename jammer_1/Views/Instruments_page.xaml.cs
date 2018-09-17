
using Jammer_1.Models;

using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Jammer_1.ViewModels;

namespace Jammer_1.Views
{
    using MvvmHelpers;
    using System.Collections.Generic;
    using System.Diagnostics;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Instruments_page : ContentPage
    {
        
        public Command LoadInstrumentsCommand { get; set; }
        private UserSettings viewModel;
        public List<Instrument> instruments { get; set; } = new List<Instrument>();
        private User currentuser;

        public Instruments_page(User user)
        {
            this.currentuser = user;
            LoadInstrumentsCommand = new Command(async () => await ExecuteLoadInstrumentsCommand());
           // LoadInstrumentsCommand.Execute(null);
            BindingContext = viewModel = new UserSettings();
            InitializeComponent();
            
        }
        async Task ExecuteLoadInstrumentsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
               // instruments.Clear();
                var items = await viewModel.Get_Instruments(currentuser.Id,"instrument_table");
                InstrumentsListView.ItemsSource= items;
               /// await DisplayAlert("hi mamadou", items.Count.ToString() , "ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
               
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var instrument = args.SelectedItem as Instrument;
            if (instrument == null)
                return;
            await Navigation.PushAsync(new Instrumentinfo(instrument,currentuser,true));

            // Manually deselect item
            InstrumentsListView.SelectedItem = null;
        }
        async void onGoToAddInstruments()
        {
            try {
                await Navigation.PushAsync(new AddInstrument(currentuser, true));
            }
            catch(Exception ex)
            {
                await DisplayAlert("cannot reach next page", ex.Message, "Ok");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadInstrumentsCommand.Execute(null);
        }

        
    }
}