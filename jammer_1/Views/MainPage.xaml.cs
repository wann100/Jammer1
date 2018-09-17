using Jammer_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BottomBar.XamarinForms;

namespace Jammer_1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : BottomBarPage
    {
		public MainPage (User user)
		{
            //var navigationPage = new NavigationPage(new MainSettings(user));
           // navigationPage.Icon = "schedule.png";
           // navigationPage.Title = "My settings";

          
           Children.Add(new MainSettings(user));
            Children.Add(new Discover(user));
            //NavigationPage.SetHasNavigationBar(this, false);
            //BarBackgroundColor = Color.Transparent;
            //BarTextColor = Color.Black;
            
        }
	}
}